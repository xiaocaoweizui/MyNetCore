using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore.DependencyInjection
{
    public class DIDemo:ControllerBase
    {
        // 1.第一种：构造函数注入
        private IService _service = null;
        public DIDemo(IService service)
        {
            this._service = service;
        }

        // 2.第二种：通过方法注入
        [HttpPost]
        public void Method1([FromServices] IService service) => service.DoSomething();

        // 3.第三种：通过HttpContext，手动取得
        [HttpGet]
        public void Method2([FromServices] IService service)
        {

  
        }

    }
}
