using Application.Models.Responds;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, List<PropertyResponse>
      {
        //Enject the interfaces we need
        private readonly IPropertyService _propertyService;

        public GetAllPropertiesQueryHandler(
            IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }
        public async Task<List<PropertyResponse>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            // Get properties from database
            var propertiesInDb = await _propertyService.GetAllAsync();

            // Convert entities to DTOs
           var propertyRes = propertiesInDb.Adapt<List<PropertyResponse>>();

           return propertyRes;
        }
    }
}
