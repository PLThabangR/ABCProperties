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
        {    // Validate required fields
             // Check if request itself is null
            if (request == null || request.CreateAgent == null)
            {
                return ResponseWrapper<string>.Fail("Request data is missing");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(request.CreateAgent.FirstName))
            {
                return ResponseWrapper<string>.Fail("FirstName is required");
            }

            if (string.IsNullOrWhiteSpace(request.CreateAgent.LastName))
            {
                return ResponseWrapper<string>.Fail("LastName is required");
            }

            if (string.IsNullOrWhiteSpace(request.CreateAgent.PhoneNumber))
            {
                return ResponseWrapper<string>.Fail("PhoneNumber is required");
            }

            if (string.IsNullOrWhiteSpace(request.CreateAgent.Email))
            {
                return ResponseWrapper<string>.Fail("Email is required");
            }





            //Use Mapster to convert DTO to Agent Entity
            var newAgent = request.CreateAgent.Adapt<Agent>();
            //Call the service so I will handle the creation in the Domain
            var agentId = await agentService.CreateAsync(newAgent);

            //Response using wrapper class
            return ResponseWrapper<int>.Success(data:agentId,message:"Agent created succesfully");
        }
    }
}
