using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreClient.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderServiceController : ControllerBase
    {
    
        [HttpGet]
        public   string Get()
        {
            return "MyNetCoreClient.Controllers.Get";
        }
    }
}
