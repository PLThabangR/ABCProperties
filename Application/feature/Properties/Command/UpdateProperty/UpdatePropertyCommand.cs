using Application.Models.Requests;
using Application.Models.Responds;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Command.UpdateProperty
{
    public record UpdatePropertyCommand(UpdatePropertyRequest Request) : IRequest<PropertyResponse?>;
    
    
}
