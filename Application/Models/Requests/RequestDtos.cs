using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{

    // Create Agent Request
    public record CreateAgentRequest(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email // Fixed: should be string, not int
    );

    // Update Agent Request
    public record UpdateAgentRequest(
        int Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Email
    );

    // Create Property Request
    public record CreatePropertyRequest(
        int AgentId,
        string ShortDescription,
        string LongDescription,
        decimal Price,
        DateTime ListingDate
    );

    // Update Property Request
    public record UpdatePropertyRequest(
        int Id,
        int AgentId,
        string ShortDescription,
        string LongDescription,
        decimal Price,
        DateTime ListingDate
    );
}
   
