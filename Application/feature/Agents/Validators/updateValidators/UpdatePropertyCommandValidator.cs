using Application.feature.Agents.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Agents.Validators.updateValidators
{// <summary>
    /// Validator for updating an existing property
    /// Ensures all property data is valid and the agent exists
    /// </summary>
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdateAgentCommand>
    {
        private readonly IAgentService _agentService;  // Service to check if agent exists

        public UpdatePropertyCommandValidator()
        {
            // First, check that the command has data
            RuleFor(x => x.UpdateAgent)
                .NotNull()
                .WithMessage("Update data is required");

            // If the data exists, use the request validator
            RuleFor(x => x.UpdateAgent).SetValidator(new UpdateAgentRequestValidator(_agentService));

        }
    }
}
