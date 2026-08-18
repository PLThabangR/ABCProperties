using System;

namespace Domain.Entities
{
    public class Agent
    {
        public int Id {get;set;}
          public string FirstName {get;set;}
            public int LastName {get;set;}
              public string PhoneNumber {get;set;}
                public int Email {get;set;}

                //Agent can have a list of properties/listings
                public List<Property> PropertyListings {get;set;}

    }
}
