using Application.Models.Requests;
using Application.Models.Responds;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Command.CreateProperty
{
    public record CreatePropertyCommand(CreatePropertyRequest Request ) : IRequest<PropertyResponse?>;
    
    
}
