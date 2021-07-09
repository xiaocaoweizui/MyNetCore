using Microsoft.AspNetCore.Mvc;
using MyNetCoreWithAutofac.DITest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreWithAutofac.Controller
{
    [Route("{controller}/{action}")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Index([FromServices] ITest test)
        {
            return Content(test.show());
        }
    }
}
