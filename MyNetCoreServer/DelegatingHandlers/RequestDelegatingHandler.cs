using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyNetCoreServer.DelegatingHandlers
{
    public class RequestDelegatingHandler : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //处理请求
            request.Headers.Add("x-guid", Guid.NewGuid().ToString());
            var result = await base.SendAsync(request, cancellationToken);

            return result;
        }

    }
}
