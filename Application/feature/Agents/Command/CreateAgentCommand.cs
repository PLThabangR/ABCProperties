using Application.Models.Requests;
using Application.Wrappers;
using Domain.Entities;
using Mapster;
using MediatR;
using System.Threading.Tasks;


namespace Application.feature.Agents.Command
{   
    //Make this a mediatR request by inheritng the IRequest interface
    //this wil travel through mediatR pipline 
    public class CreateAgentCommand:IRequest<IResponseWrapper>
    {
            public CreateAgentRequest CreateAgent {get;set;}   

    }

    //Handler class
    public class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, IResponseWrapper>
    {   
        //We inject our inteface
        private readonly IAgentService agentService;

        public CreateAgentCommandHandler(IAgentService agentService)
        {
            this.agentService = agentService;
        }

        public async Task<IResponseWrapper> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
        {    //Use Mapster to convert DTO to Agent Entity
            var newAgent = request.Adapt<Agent>();
            //Call the service so I will handle the creation in the Domain
            var agentId = await agentService.CreateAsync(newAgent);

            //Response using wrapper class
            return ResponseWrapper<int>.Success(data:agentId,message:"Agent created succesfully");
        }
    }
}
