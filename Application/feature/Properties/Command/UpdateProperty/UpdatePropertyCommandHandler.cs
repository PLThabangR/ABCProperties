using Application.feature.Agents;
using Application.Models.Responds;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.feature.Properties.Command.UpdateProperty
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, PropertyResponse>
    {
        private readonly IPropertyService _propertyService;
        private readonly IAgentService _agentService;

        public UpdatePropertyCommandHandler(
            IPropertyService propertyService,
            IAgentService agentService)
        {
            _propertyService = propertyService;
            _agentService = agentService;
        }


        public async Task<PropertyResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            //check if property exist 
            var propertyInDb = await _propertyService.GetByIdAsync(request.Request.Id);

            if (propertyInDb == null) {
                return null;
            }

            //check of agent exist 
            var agentInDb = await _agentService.GetByIdAsync(request.Request.AgentId);
            if (agentInDb == null) {
                return null;
            }

            //update 
            propertyInDb.AgentId = request.Request.AgentId;
            propertyInDb.ShortDecription = request.Request.ShortDescription;
            propertyInDb.LongDescription = request.Request.LongDescription;
            propertyInDb.Price = request.Request.Price;
            propertyInDb.ListindDate = request.Request.ListingDate;

            //Save
            var updatedProperty = await _propertyService.UpdateAsync(propertyInDb);

            if(updatedProperty == null)
            {
                return null;
            }

            //convert to dto
            var updatedRes = updatedProperty.Adapt<PropertyResponse>();

            return updatedRes;


        }
    }
}
