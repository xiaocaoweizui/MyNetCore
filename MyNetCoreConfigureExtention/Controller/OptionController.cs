using Microsoft.AspNetCore.Mvc;
using MyNetCoreConfigureExtention.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreConfigureExtention.Controller
{
     [ApiController]
    [Route("api/[controller]/[action]")]
    public class OptionController : ControllerBase
    {

        public OptionController()
        {
          
        }

        [HttpGet]
        public IActionResult MyOptionTest([FromServices] IApplySerivce service)
        {
            Console.WriteLine("this.service 的 GetMaxCount :" + service.GetMaxCount().ToString());
            return Content("MyOptionTest");

        }

        [HttpGet]
        public IActionResult MyOptionTest2([FromServices] IOrderSerivce service)
        {
            Console.WriteLine("this.service 的 GetMaxCount :" + service.GetMaxCount().ToString());
            return Content("MyOptionTest");
        }

        [HttpGet]
        public IActionResult MyOptionTest3([FromServices] IBookSerivce service)
        {
            Console.WriteLine("this.service 的 GetMaxCount :" + service.GetMaxCount().ToString());
            return Content("MyOptionTest");
        }

        [HttpGet]
        public IActionResult MyOptionTest4([FromServices] IPhoneSerivce service)
        {
            Console.WriteLine("this.service 的 GetMaxCount :" + service.GetMaxCount().ToString());
            return Content("MyOptionTest");
        }
    }
}
