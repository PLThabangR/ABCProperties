namespace Application.Wrappers
{       
    //This is the implementation of our wrapper interfaces
    public class ResponseWrapper :IResponseWrapper
    {
        public List<string> Messages { get; set; }=[];
        public bool IsSuccess { get; set; }

        //this will make a fail
        public static IResponseWrapper Fail(){
            return new ResponseWrapper (){ IsSuccess=false};
        }//end of fail m,ethod  

        //if the is a parameter message for failure
        public static IResponseWrapper Fail(string message){
            return new ResponseWrapper (){ IsSuccess=false,Messages=[message]};
        }

        //list of messages 
        public static IResponseWrapper Fail(List<string> messages){
            return new ResponseWrapper (){ IsSuccess=false,Messages=messages};
        }
        

    }

       
}
