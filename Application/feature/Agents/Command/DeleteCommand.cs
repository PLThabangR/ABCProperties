using Application.Wrappers;
using Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.feature.Agents.Command
{
    public class DeleteCommand:IRequest<IResponseWrapper>
    {
        public int AgentId { get; set; }

        
    }


    //Create our command handler 

    public class DeleteCommnadHandler : IRequestHandler<DeleteCommand, IResponseWrapper>
    {//We inject our inteface
        private readonly IAgentService agentService;

        public DeleteCommnadHandler(IAgentService agentService)
        {
            this.agentService = agentService;
        }
        public async Task<IResponseWrapper> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {

            //Call service3 to do the Delete
            var agentID =await agentService.DeleteAsync(request.AgentId);

            if(agentID > 0)
            {
                return ResponseWrapper<int>.Success(data: agentID, message: "Agent deleted successfully");
            }

            return ResponseWrapper<int>.Fail( message: "Agent not found");
        }
    }
}
