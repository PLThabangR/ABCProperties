using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebAPI.Controllers
{
   
    [ApiController]
    public class MyBaseController : ControllerBase
    {
        private ISender _sender;
            //we do not need to initialize this aal tim
        public ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>()!;
    }
}
