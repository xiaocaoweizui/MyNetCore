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
                //����֤����Ч������ǩ��֤��
                hanlder.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true;
                return hanlder;
            })
            //.AddTransientHttpErrorPolicy(p=>p.WaitAndRetryAsync(20,i=>TimeSpan.FromSeconds(i*2)));
            //�ƶ����ԵĴ�����ÿ�μ��һ��ʱ��
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(i * 3)));
            //����ֱ���ɹ���ÿ�μ��һ��ʱ��

            //�Զ������Բ���
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

            //�۶�
            services.AddHttpClient("client4").AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().CircuitBreakerAsync(
               handledEventsAllowedBeforeBreaking: 10, // ������ٴη����۶�
               durationOfBreak: TimeSpan.FromSeconds(10),//�۶ϵ�ʱ��
               onBreak: (r, t) => { },//�����۶�ʱ�������¼�
               onReset: () => { },//�۶ϻظ�ʱ�������¼�
               onHalfOpen: () => { } //�۶��ڼ���֤�����Ƿ����
               ));

            //�߼��۶�
            services.AddHttpClient("client4").AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync(
            failureThreshold: 0.8, // ���󱨴�ı�������80%ʱ �������۶�
            samplingDuration: TimeSpan.FromSeconds(30),//�۶ϲ�����ʱ��
            minimumThroughput: 100,//��С��������
            durationOfBreak: TimeSpan.FromSeconds(30),//�۶ϵ�ʱ��
              onBreak: (r, t) => { },//�����۶�ʱ�������¼�
               onReset: () => { },//�۶ϻظ�ʱ�������¼�
               onHalfOpen: () => { } //�۶��ڼ���֤�����Ƿ����
            ));

            //���񽵼�
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
