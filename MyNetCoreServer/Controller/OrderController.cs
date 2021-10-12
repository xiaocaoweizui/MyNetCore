using Microsoft.AspNetCore.Mvc;
using MyNetCoreServer.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreServer.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        OrderServiceClient _client;


        public OrderController(OrderServiceClient orderServiceClient )
        {
            _client = orderServiceClient;
        }


        public async Task<string> Get()
        {
            return await _client.Get();
        }

        [HttpGet("Get2")]
        public async Task<string> NamedClientGet([FromServices]NamedOrderServiceClient client)
        {
            return await client.Get();
        }

        [HttpGet("Get3")]
        public async Task<string> NamedClientGet([FromServices] TypedOrderServiceClient client)
        {
            return await client.Get();
        }
    }
}
