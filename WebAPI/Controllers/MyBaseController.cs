using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebAPI.Controllers
{
   
    [ApiController]
    public class MyBaseController : ControllerBase
    {
        // Get MediatR's ISender service
      //  private ISender? _sender;
        private ISender sender = null;
        public ISender Sender => sender ??= HttpContext.RequestServices.GetService<ISender>();

        // Create the ISender only when we actually need it
    //    public ISender Sender
    //    {
    //        get
    //        {
    //            // If _sender is already created, use it
    //            if (_sender == null)
    //            {
    //                // Get ISender from ASP.NET Core's Dependency Injection container
    //                _sender = HttpContext.RequestServices.GetService<ISender>();
    //            }
    //            //Return existing sender instance 
    //            return _sender!;
    //        }
    //    }
   }
}
