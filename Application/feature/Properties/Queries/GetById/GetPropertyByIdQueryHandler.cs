using Application.Models.Responds;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Queries.GetById
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyResponse?>
    {
        private readonly IPropertyService _propertyService;

        public GetPropertyByIdQueryHandler(
            IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<PropertyResponse?> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            //find the property by ID
        var propertyInDb = await _propertyService.GetByIdAsync(request.id);

        if(propertyInDb == null)
            {
                return null;
            } 

            //convert to DTO
            var propertyRes = propertyInDb.Adapt<PropertyResponse>();

            return propertyRes;

        }
    }


}
