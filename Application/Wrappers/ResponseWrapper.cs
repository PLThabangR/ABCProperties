namespace Application.Wrappers
{       
    //This is the implementation of our wrapper interfaces non generic
    public class ResponseWrapper :IResponseWrapper
    {
        public List<string> Messages { get; set; }=[];
        public bool IsSuccess { get; set; }

        //Failure implementations
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
        

        //Succes implementations 
         public static IResponseWrapper Success(){
            return new ResponseWrapper (){ IsSuccess=true};
        }//end of fail m,ethod  

        //if the is a parameter message for failure
        public static IResponseWrapper Success(string message){
            return new ResponseWrapper (){ IsSuccess=true,Messages=[message]};
        }

        //list of messages 
        public static IResponseWrapper Success(List<string> messages){
            return new ResponseWrapper (){ IsSuccess=true,Messages=messages};
        }
        
        

    }

    //Implentation for our  generic
    public class ResponseWrapper<T> :ResponseWrapper,IResponseWrapper<T>
    {
        public T Data {get;set;}
        //Use new keyword that this will not use inherted method
        #region Failures
        public new static ResponseWrapper<T> Fail()
        {
            return new ResponseWrapper<T>() { IsSuccess = false };
        }


        public new static ResponseWrapper<T>Fail(string message)
        {
            return new ResponseWrapper<T>(){IsSuccess=false,Messages=[message]};
        }

        public new static ResponseWrapper<T> Fail(List<string> messages)
        {
            return new ResponseWrapper<T>() { IsSuccess = false, Messages = messages };
        }





        #endregion

        #region Success

        public new static ResponseWrapper<T> Success()
        {
            return new ResponseWrapper<T>() { IsSuccess = true };
        }


        public new static ResponseWrapper<T> Success(string message)
        {
            return new ResponseWrapper<T>() { IsSuccess = true, Messages = [message] };
        }

        public new static ResponseWrapper<T> Success(List<string> messages)
        {
            return new ResponseWrapper<T>() { IsSuccess = true, Messages = messages };
        }

        //Below we include T data on our return
        public static ResponseWrapper<T> Success(T data)
        {      
            return new ResponseWrapper<T>() {Data=data,IsSuccess=true};
        }

        public static ResponseWrapper<T> Success(T data,string message)
        {
            return new ResponseWrapper<T>() { Data = data, IsSuccess = true,Messages=[message] };
        }
        public static ResponseWrapper<T> Success(T data,List<string> messages)
        {
            return new ResponseWrapper<T>() { Data = data, IsSuccess = true ,Messages=messages};
        }



        #endregion

    }

}
