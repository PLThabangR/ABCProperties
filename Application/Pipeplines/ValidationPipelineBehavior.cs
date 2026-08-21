using Application.Wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pipeplines
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> //Only MediatR request are going to be Validated
    { //We need the IValidator interface and the generic one we gonna attach it to incoming requests
        //We only 
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {   
            //Check if theis any validatior created
            //We gonna scan thought the assembly
            //Execute if any validators are found
            if (validators.Any()) {
                var context = new ValidationContext<TRequest>(request);
                //This code willl execute validation
                var validationResults = await Task .WhenAll(validators.Select(vr =>vr.ValidateAsync(context,cancellationToken)));

                //we check what results do we get
                //if is not valid sget errors in a list 
                if (!validationResults.Any(vr => vr.IsValid))
                {
                    //create variable to store errors messages
                    var errorMessages = new List<string>();

                    //Get errors from failures where failure is not null 
                    //this is how we obtain our errors
                    var failures = validationResults.SelectMany(vr=>vr.Errors)
                            .Where(f=> f !=null).ToList();

                    //We loop through failure to obtain the error messages
                    foreach(var failure in failures)
                    {   //Add each error message
                        errorMessages.Add(failure.ErrorMessage);
                    }
                //We return response of type TResponse we cast our Repper to TResponse 
                    return (TResponse)ResponseWrapper.Fail(messages : errorMessages);
                }
            }

            //if there no errors let the request travel tho the next pipeline
            return await next();
        }
    }
}
