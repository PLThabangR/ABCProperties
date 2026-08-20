using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Command.DeleteProperty
{
    public record DeletePropertyCommand(int Id) : IRequest<bool>;
    
    
}
