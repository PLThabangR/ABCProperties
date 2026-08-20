using Application.feature.Agents.Command;
using Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AgentController : MyBaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> CreateAgentAsync([FromBody] CreateAgentRequest createAgent)
        {

            //Use MediatR request pipeline to pass this new request to the Create Command
            var response = await Sender.Send(new CreateAgentCommand { CreateAgent = createAgent });

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);

        }


        [HttpPut("update")]
        public async Task<IActionResult> updateAgentAsync([FromBody] UpdateAgentRequest updateAgent)
        {

            //Use MediatR pipeline to pass this new request to the Create Command
            var response = await Sender.Send(new UpdateAgentCommand { UpdateAgent = updateAgent });

            //the response is a wrapper
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);



        }


    }
}
