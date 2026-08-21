using Application.feature.Agents;
using Application.feature.Properties.Command.CreateProperty;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Validators.CreatePropertyCommandValida
{
    //We are inheriting from AbstractValidators and passing the CreatePropertyCommand
    //We are validating the createProperty object from CreatePropertyCommand
    public class CreatePropertyCommandValidator:AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator(IAgentService agentService) {
            //We are invoking the CreatePropertyValidation throung the parent command
            RuleFor(command => command.Request).SetValidator(new CreatePropertyRequestValidator(agentService));
        }
    }
}
