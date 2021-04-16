using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore
{
    [ApiController]
    [Route("api/[controller]/{action}")]
    public class DIDemoController:ControllerBase
    {
        // 1.第一种：构造函数注入
        private IService _service = null;
        public DIDemoController(IService service)
        {
            this._service = service;
        }

        // 2.第二种：通过方法注入
        [HttpPost]
        public IActionResult MyPostTest([FromServices] IService service)
        {
            service.DoSomething();
            return Content("MyPostTest");
        }

        // 3.第三种：通过HttpContext，手动取得
        [HttpGet]
        public IActionResult MyGeTest([FromServices] IService service)
        {

            return Content("MyGeTest");
        }

    }
}
