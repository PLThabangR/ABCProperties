using Application.Models.Responds;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Queries.GetById
{
    public record GetPropertyByIdQuery(int id) : IRequest<PropertyResponse>;
    
    
}
