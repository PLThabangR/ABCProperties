using Application.Models.Responds;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Queries.GetAllProperties
{
    public record GetAllPropertiesQuery
        : IRequest<List<PropertyResponse>>;
}
