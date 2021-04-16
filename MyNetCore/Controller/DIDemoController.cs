using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNetCore.ServiceLifeTime;

namespace MyNetCore.Controllers
{

    /// <summary>
    ///  Web API的创建过程，后续会讲
    /// 参见  https://docs.microsoft.com/zh-cn/aspnet/core/web-api/?view=aspnetcore-5.0
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DIDemoController : ControllerBase
    {
        // 1.第一种：构造函数注入
        private IService _service = null;
        private ISingletonService _singletonService = null;
        private IScopeService _scopeService = null;
        private ITransientService _transientService = null;

        public DIDemoController(IService service, ISingletonService singletonService, IScopeService scopeService, ITransientService transientService)
        {
            _service = service;
            _scopeService = scopeService;
            _singletonService = singletonService;
            _transientService = transientService;
        }

        // 2.第二种：通过方法注入
        [HttpPost]
        //  [Consumes("application/json")]
        // [Consumes("application/x-www-form-urlencoded")]
        public IActionResult MyPostTest([FromServices] IService service)
        {
            service.DoSomething();
            return Content("MyPostTest");
            //IEnumerable<int> values =new List<int>{ 1, 2, 3, 45 };
            //return Ok(new { Consumes = "application/json", Values = values });
        }

        // 3.第三种：通过HttpContext，手动取得
        [HttpGet]
        public IActionResult MyGetTest()
        {
            this._service.DoSomething();
            Console.WriteLine("this._service 的 hashCode :" + this._service.GetHashCode());
            return Content("MyGetTest");

        }


        // 3.第三种：通过HttpContext，获取的类是否一致
        [HttpGet]
        public IActionResult MyGetTest2([FromServices] ISingletonService singletonService1,
            [FromServices] ISingletonService singletonService2, [FromServices] IScopeService scopeService1,
            [FromServices] IScopeService scopeService2, [FromServices] ITransientService transientService1, [FromServices] ITransientService transientService2)
        {
            Console.WriteLine($"构造函数 中获取的 singletonService1 的 hashCode :{ _singletonService.GetHashCode()}");
            Console.WriteLine($"构造函数 中获取的 scopeService1 的 hashCode :{ _scopeService.GetHashCode()}");
            Console.WriteLine($"构造函数 中获取的 transientService1 的 hashCode :{ _transientService.GetHashCode()}");

            Console.WriteLine($"HttpContext 中获取的 singletonService1 的 hashCode :{ singletonService1.GetHashCode()}");
            Console.WriteLine($"HttpContext 中获取的 singletonService2 的 hashCode :{ singletonService2.GetHashCode()}");
            Console.WriteLine($"HttpContext 中获取的 scopeService1 的 hashCode :{ scopeService1.GetHashCode()}");
            Console.WriteLine($"HttpContext 中获取的 scopeService2 的 hashCode :{ scopeService2.GetHashCode()}");
            Console.WriteLine($"HttpContext 中获取的 transientService1 的 hashCode :{ transientService1.GetHashCode()}");
            Console.WriteLine($"HttpContext 中获取的 transientService2 的 hashCode :{ transientService2.GetHashCode()}");
            //serviceProvider对象
            Console.WriteLine($"HttpContext的ServiceProvider 的 hashCode :{ HttpContext.RequestServices.GetHashCode()}");

            return Content("MyGetTest");

        }


        // 3.第三种：通过HttpContext，手动取得
        [HttpGet]
        public int GetServiceList([FromServices] IService service)
        {

            return 1;
        }

    }
}
