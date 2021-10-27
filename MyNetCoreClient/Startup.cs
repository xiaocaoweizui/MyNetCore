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
using Polly;
using Polly.CircuitBreaker;
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
            }).ConfigurePrimaryHttpMessageHandler(provider =>
            {
                var hanlder = new SocketsHttpHandler();
                //允许证书无效或者自签名证书
                hanlder.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true;
                return hanlder;
            })
            //.AddTransientHttpErrorPolicy(p=>p.WaitAndRetryAsync(20,i=>TimeSpan.FromSeconds(i*2)));
            //制定重试的次数，每次间隔一定时间
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(i * 3)));
            //重试直到成功，每次间隔一定时间

            //自定义重试策略
            var reg = services.AddPolicyRegistry();
            reg.Add("retryforever", Policy.HandleResult<HttpResponseMessage>(msg =>
             {
                 return msg.StatusCode == System.Net.HttpStatusCode.Created;
             }).RetryForeverAsync());
            #region polly

            services.AddHttpClient("client2").AddPolicyHandlerFromRegistry("retryforever");
            services.AddHttpClient("client3").AddPolicyHandlerFromRegistry((registry, msg) =>
            {
                return msg.Method == HttpMethod.Get ? registry.Get<IAsyncPolicy<HttpResponseMessage>>("retryforever") : Policy.NoOpAsync<HttpResponseMessage>();
            });

            //熔断
            services.AddHttpClient("client4").AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().CircuitBreakerAsync(
               handledEventsAllowedBeforeBreaking: 10, // 报错多少次发送熔断
               durationOfBreak: TimeSpan.FromSeconds(10),//熔断的时间
               onBreak: (r, t) => { },//发送熔断时发生的事件
               onReset: () => { },//熔断回复时发生的事件
               onHalfOpen: () => { } //熔断期间验证服务是否可用
               ));

            //高级熔断
            services.AddHttpClient("client4").AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync(
            failureThreshold: 0.8, // 请求报错的比例大于80%时 ，进行熔断
            samplingDuration: TimeSpan.FromSeconds(30),//熔断采样的时间
            minimumThroughput: 100,//最小的吞吐量
            durationOfBreak: TimeSpan.FromSeconds(30),//熔断的时间
              onBreak: (r, t) => { },//发送熔断时发生的事件
               onReset: () => { },//熔断回复时发生的事件
               onHalfOpen: () => { } //熔断期间验证服务是否可用
            ));

            //服务降级
            var message = new HttpResponseMessage()
            {
                Content = new StringContent("{}")
            };
            var fallBack = Policy<HttpResponseMessage>.Handle<BrokenCircuitException>().FallbackAsync(message);
                 var fallBack = Policy<HttpResponseMessage>.Handle<BrokenCircuitException>().FallbackAsync(message);
           // var retry=Policy.WrapAsync

            #endregion

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
