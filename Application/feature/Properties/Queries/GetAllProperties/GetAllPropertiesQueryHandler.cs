using Application.Models.Responds;
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
            var properties = await _propertyService.GetAllAsync();

            // Convert entities to DTOs
            return properties.Select(property =>
                new PropertyResponse(
                    property.Id,
                    property.AgentId,
                    property.ShortDecription,
                    property.LongDescription,
                    property.Price,
                    property.ListindDate,

                    property.Agent == null
                        ? null
                        : new AgentSummaryResponse(
                            property.Agent.Id,
                            property.Agent.FirstName,
                            property.Agent.LastName,
                            property.Agent.PhoneNumber
                        )
                )
            ).ToList();
        }
    }
}
