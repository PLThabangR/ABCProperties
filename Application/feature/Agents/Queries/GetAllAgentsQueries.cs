using Application.Models.Responds;
using Application.Wrappers;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Agents.Queries
{
    public class GetAllAgentsQueries:IRequest<ResponseWrapper<List<AgentResponse>>>
    {
    }

    public class GetAllQueriesHandler : IRequestHandler<GetAllAgentsQueries, ResponseWrapper<List<AgentResponse>>>
    {
        private IAgentService agentService;

        public GetAllQueriesHandler(IAgentService agentService)
        {
            this.agentService = agentService;
        }

        public async Task<ResponseWrapper<List<AgentResponse>>> Handle(GetAllAgentsQueries request, CancellationToken cancellationToken)
        {       //This will return a list 
            var agentInDb = await agentService.GetAllAsync();

            if (agentInDb.Count >0) {

                //Convert this to list of agents dtos
                var agentDTO = agentInDb.Adapt<List<AgentResponse>>();

                return ResponseWrapper<List<AgentResponse>>.Success(data: agentDTO);
               
            }
            return ResponseWrapper<List<AgentResponse>>.Fail("No agent were found");

        }
    }
}
