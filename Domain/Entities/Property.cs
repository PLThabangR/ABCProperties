using System;


namespace Domain.Entities
{
    public  class Property
    {
            //ID
            public int Id {get;set;}
        //foreign key
        public int AgentId {get;set;}
             public string ShortDecription {get;set;}
              public string LongDescription {get;set;}
               public decimal Price {get;set;}
                public DateTime ListindDate {get;set;}

                    //Navigation property
                public Agent Agent  {get;set;}


    }
}
