using Application.feature.Agents;
using Application.Models.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Validators.CreatePropertyCommandValida
{
    public class CreatePropertyRequestValidator:AbstractValidator<CreatePropertyRequest>
    {   
        //Validate if Agent exist by enjcting the IAgentService into our constructor
        public CreatePropertyRequestValidator(IAgentService agentService)
        {   //Short description must not be empty
            RuleFor(request => request.ShortDescription)
                .NotEmpty();

            //Price must be greater than zero
            RuleFor(request => request.Price)
                .GreaterThan(0.0m);

            //Domain level validation
            //Validate the agent if exist using custom validations mustAsync 
            //We are checking if Agent exist in the DB
            RuleFor(request => request.AgentId)
                .Cascade(CascadeMode.Stop) //When one fails the DB operation should stop not to continue to othe validations
                .NotEmpty()
                .MustAsync(async (AgentId, cancellationtoken) => await agentService.DoesExistAsync(AgentId))
                .WithMessage("Agent does not exist");

        }
    }
}
