using Application.feature.Agents.Command;
using Application.Models.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.feature.Agents.Validators.createValidators
{
   public class CreateAgentCommandValidator : AbstractValidator<CreateAgentCommand>
    {

        public CreateAgentCommandValidator()
        {
            /// First, check that the command itself has data
            RuleFor(x => x.CreateAgent)
                .NotNull()  // Cannot be null
                .WithMessage("Agent data is required");


            RuleFor(command => command.CreateAgent).SetValidator(new CreateAgentRequestValidator());
        }

        
    }
}
