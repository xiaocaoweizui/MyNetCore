using Grpc.Core;
using MyNetCoreServer.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyNetCoreServer.GrpcService
{
    public class BookService:BookGrpc.BookGrpcBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context">当前请求的上下文</param>
        /// <returns></returns>
        public override Task<CreateBookResult> CreateBook(CreateBookCommand request,ServerCallContext context)
        {
            //可以写创建book的真实业务
            return Task.FromResult(new CreateBookResult { Id = 1 });
        }
    }
}
