using Application.feature.Agents.Command;
using Application.feature.Agents.Queries;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AgentController : MyBaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> CreateAgentAsync([FromBody] CreateAgentRequest createAgent)
        {   //create command
            var command = new CreateAgentCommand { CreateAgent = createAgent };

            //Use MediatR request pipeline to pass this new request to the Create Command
            //We use this syntax becuase we using a class  not a record
            var response = await Sender.Send(command);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);

        }


        [HttpPut("update")]
        public async Task<IActionResult> updateAgentAsync([FromBody] UpdateAgentRequest updateAgent)
        {
            //Create new command 
            var command = new UpdateAgentCommand { UpdateAgent = updateAgent };

            //Use MediatR pipeline to pass this new request to the Create Command
            var response = await Sender.Send(command);

            //the response is a wrapper
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);

        }

        //Delete api/Agent/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteAgent(int id)
        {
            //Create commant
            var command = new DeleteCommand { AgentId = id };
            
            //send the command to MediaR
            var result = await Sender.Send(command);
            //return the result
            return Ok(result);

        }

        // GET: api/Agent
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Create the query
            var query = new GetAllAgentsQueries();

            // Send query to MediatR
            var result = await Sender.Send(query);

            // Return response
            return Ok(result);
        }

        // GET: api/Agent/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id)
        {
            // Create the query
            var query = new GetAgentByIdQery
            {
                AgentID = id
            };

            // Send query to MediatR
            var result = await Sender.Send(query);

            // Return response
            return Ok(result);
        }



    }
}

