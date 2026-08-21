using Application.feature.Properties.Command.CreateProperty;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI.EndPoint
{
    public static class CreatePropertyEndPoint
    { 
        public static RouteHandlerBuilder MapCreatePropertyEndPoint(this IEndpointRouteBuilder endpoint)
        {
            return endpoint.MapPost("/add",async (CreateAgentRequest createProperty,ISender  sender)=>{

                    //We are using record instead of class
                var property = await sender.Send(createProperty);
               
                if (property == null)
                {
                    return Results.Ok("Something went wrong");
                }

                return Results.Ok(property);

            });
        }
    }
}
