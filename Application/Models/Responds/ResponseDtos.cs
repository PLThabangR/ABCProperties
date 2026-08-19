using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responds
{
    public class ResponseDtos
    {// Agent Response (without the navigation property to avoid circular references)
        public record AgentResponse(
            int Id,
            string FirstName,
            string LastName,
            string PhoneNumber,
            string Email,
            List<PropertySummaryResponse>? PropertyListings = null
        );

        // Property Summary (for nested lists)
        public record PropertySummaryResponse(
            int Id,
            string ShortDescription,
            decimal Price,
            DateTime ListingDate
        );

        // Detailed Property Response
        public record PropertyResponse(
            int Id,
            int AgentId,
            string ShortDescription,
            string LongDescription,
            decimal Price,
            DateTime ListingDate,
            AgentSummaryResponse? Agent = null
        );

        // Agent Summary (for nested display)
        public record AgentSummaryResponse(
            int Id,
            string FirstName,
            string LastName,
            string PhoneNumber
        );
    }
}
