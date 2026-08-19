

namespace Application.Wrappers
{
    public interface IResponseWrapper
    {
        public List<string> Messages { get; set; }
        public bool IsSuccess { get; set; }


    }


    //Generic inteface 
    //This will have a response of type T and inherit a none generic one
    public interface IResponseWrapper<out T> : IResponseWrapper
    {   

        //propety of type T 
        public T Data { get; }
       
    }
}
