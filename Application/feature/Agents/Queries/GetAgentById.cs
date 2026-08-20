using Application.Models.Responds;
using Application.Wrappers;
using Mapster;
using MediatR;


namespace Application.feature.Agents.Queries
{
    //We use implemtation because we will need to deserialize from cache
    //We cant deserialize an instance
        public class GetAgentByIdQery : IRequest<ResponseWrapper<AgentResponse>>
    {

        public int AgentID { get; set; }
    }

    //Query handler
    public class GetAgentByIdHandler : IRequestHandler<GetAgentByIdQery, ResponseWrapper<AgentResponse>>
    {
        private IAgentService agentService;

        public GetAgentByIdHandler(IAgentService agentService)
        {
            this.agentService = agentService;
        }

        public async Task<ResponseWrapper<AgentResponse>> Handle(GetAgentByIdQery request, CancellationToken cancellationToken)
        {
           var agentInDb =  await agentService.GetByIdAsync(request.AgentID);
            if (agentInDb == null)
            {
                return ResponseWrapper<AgentResponse>.Fail(message:"Agent was not found");
            }
           var agentResponse = agentInDb.Adapt<AgentResponse>();

           return ResponseWrapper<AgentResponse>.Success(data:agentResponse);
        }
    }
}
