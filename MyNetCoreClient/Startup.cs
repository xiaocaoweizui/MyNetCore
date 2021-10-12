using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyNetCoreServer.Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static MyNetCoreServer.Proto.BookGrpc;

namespace MyNetCoreClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

            services.AddControllers();

            services.AddGrpcClient<BookGrpc.BookGrpcClient>(options =>
            {
                options.Address = new Uri("https://localhost:6002");
            }).ConfigurePrimaryHttpMessageHandler(provider=>
            {
                var hanlder = new SocketsHttpHandler();
                //允许证书无效或者自签名证书
                hanlder.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true;
                return hanlder;
            });
    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                 {
                     BookGrpcClient service = context.RequestServices.GetService<BookGrpcClient>();

                     CreateBookResult r = null; 
                     try
                     {
                          r = service.CreateBook(new CreateBookCommand { BuyerId = "abc" });
                     }
                     catch(Exception ex)
                     {

                     }
                     await context.Response.WriteAsync(r.Id.ToString());
                 });
            });
        }
    }
}
