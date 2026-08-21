using Application.feature.Agents.Queries;
using Application.feature.Properties.Command.CreateProperty;
using Application.feature.Properties.Command.DeleteProperty;
using Application.feature.Properties.Command.UpdateProperty;
using Application.feature.Properties.Queries;
using Application.feature.Properties.Queries.GetAllProperties;
using Application.feature.Properties.Queries.GetById;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {      //Use MediatR to send request and command to proper handers
        private readonly ISender sender;

        public PropertyController(ISender sender)
        {
            this.sender = sender;
        }

        //GET: api/Property
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //send to MediatR
            var properties = await sender.Send(new GetAllPropertiesQuery());

            return Ok(properties);

        }

        //GET: api/Property/1
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            //Send query to MediatR
            var property = await sender.Send(new GetPropertyByIdQuery(id));
            //if propert was not found 
            if (property == null)
            {
                return NotFound("Prperty not found");
            }


            return Ok(property);

        }


        // POST: api/Property
        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyRequest request)
        {

            //send to mediaR
            var property = await sender.Send(new CreatePropertyCommand(request));

            //null
            if (property == null)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(property);

        }

        // PUT: api/Property/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePropertyRequest request)
        {

            //Make Sure Id matches request ID
            if (id == request.Id)
            {
                BadRequest("The property ID do not match");
            }
            //Send command to MeDiaR
            var property = await sender.Send(new UpdatePropertyCommand(request));

            if (property == null)
            {
                return NotFound("Property not found");
            }

            return Ok(request);


        }

        // DELETE: api/Property/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Send command to MediatR
            var deleted = await sender.Send(
                new DeletePropertyCommand(id));

            // Property wasn't found
            if (!deleted)
            {
                return NotFound("Property was not found.");
            }

            return NoContent();
        }



    }
}
