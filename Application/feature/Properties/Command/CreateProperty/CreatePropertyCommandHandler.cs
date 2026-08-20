using Application.feature.Agents;
using Application.feature.Properties.Command.CreateProperty;
using Application.Models.Responds;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Properties.Command
{
    public  class CreatePropertyCommandHandler:IRequestHandler<CreatePropertyCommand,PropertyResponse>
        {
        private readonly IPropertyService _propertyService;
        private readonly IAgentService _agentService;

        public CreatePropertyCommandHandler(
            IPropertyService propertyService,
            IAgentService agentService)
        {
            _propertyService = propertyService;
            _agentService = agentService;
        }

        public async Task<PropertyResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            // Check that the Agent exists
            var agent = await _agentService.GetByIdAsync(request.Request.AgentId);

            if (agent == null)
            {
                return null;
            }

            // Create Property entity
            var property = new Property
            {
                AgentId = request.Request.AgentId,
                ShortDecription = request.Request.ShortDescription,
                LongDescription = request.Request.LongDescription,
                Price = request.Request.Price,
                ListindDate = request.Request.ListingDate
            };


            // Save property
            var propertyId = await _propertyService
                .CreateAsync(property);

            // Get the newly created property
            var createdProperty = await _propertyService
                .GetByIdAsync(propertyId);

            if (createdProperty == null)
            {
                return null;
            }
            // Convert Entity to Response DTO
            return new PropertyResponse(
                createdProperty.Id,
                createdProperty.AgentId,
                createdProperty.ShortDecription,
                createdProperty.LongDescription,
                createdProperty.Price,
                createdProperty.ListindDate,

                new AgentSummaryResponse(
                    createdProperty.Agent.Id,
                    createdProperty.Agent.FirstName,
                    createdProperty.Agent.LastName,
                    createdProperty.Agent.PhoneNumber
                )
            );


        }
        }
        }

    

