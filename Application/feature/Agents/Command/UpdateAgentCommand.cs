using Application.Models.Requests;
using Application.Models.Responds;
using Application.Wrappers;

using Domain.Entities;
using Mapster;
using MediatR;


namespace Application.feature.Agents.Command
{
    public class UpdateAgentCommand:IRequest<IResponseWrapper>
    {
        //We creating our Command of type UpdateAgentRequest
        public UpdateAgentRequest UpdateAgent { get; set; }
    }//End

    public class UpdateAgentCommandHandler : IRequestHandler<UpdateAgentCommand, IResponseWrapper>
    { //enject IAgentService
      //We inject our inteface
        private readonly IAgentService agentService;

        public UpdateAgentCommandHandler(IAgentService agentService)
        {
            this.agentService = agentService;
        }

       

        public async Task<IResponseWrapper> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
        {
            //
            //Use Mapster to convert DTO to Agent Entity
            var newUpdateAgent = request.Adapt<Agent>();
            //Use await since we will be communicationing with outside layers
            var updatedAgent = await agentService.UpdateAsync(newUpdateAgent);

            if(updatedAgent == null)
            {
                return ResponseWrapper<AgentResponse>.Fail(message: "Failed to update agent");
            }

            //Convert the entity back to DTO
            var updatedDto = updatedAgent.Adapt<AgentResponse>();

            //Response using wrapper class
            return ResponseWrapper<AgentResponse>.Success(data: updatedDto, message: "Agent was succesfully updated");
        }
    }


}
