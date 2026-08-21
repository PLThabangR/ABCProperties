using Application.feature.Agents;
using Application.Models.Requests;
using FluentValidation;


namespace Application.feature.Properties.Validators.CreatePropertyCommandValida
{
    public class CreatePropertyRequestValidator:AbstractValidator<CreatePropertyRequest>
    {   
        //Validate if Agent exist by enjcting the IAgentService into our constructor
        public CreatePropertyRequestValidator(IAgentService agentService)
        {   //Short description must not be empty
            RuleFor(request => request.ShortDescription)
                .NotEmpty().WithMessage("Short description is required")
                .MaximumLength(100).WithMessage("Short description must not exceed 100 characters");


            // Long description must not be empty
            RuleFor(request => request.LongDescription)
                .NotEmpty().WithMessage("Long description is required")
                .MaximumLength(1000).WithMessage("Long description must not exceed 1000 characters");

            //Price must be greater than zero
            RuleFor(request => request.Price)
                .GreaterThan(0)
                .WithMessage("Price Must be greater Than Zero");

            // Listing date must be in the future or today
            RuleFor(request => request.ListingDate)
                .NotEmpty().WithMessage("Listing date is required")
                .Must(date => date.Date >= DateTime.Today)
                .WithMessage("Listing date cannot be in the past");

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
