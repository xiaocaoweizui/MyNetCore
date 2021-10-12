using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyNetCoreServer.Client;
using MyNetCoreServer.DelegatingHandlers;
using MyNetCoreServer.GrpcService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyNetCoreServer
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

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true;

            services.AddControllers();
            //ע��
            services.AddHttpClient();
            services.AddScoped<OrderServiceClient>();
            services.AddSingleton<RequestDelegatingHandler>();

            //Ϊ��ͬ��client ��������
            services.AddHttpClient("OrderClient1", client =>
             {
                 client.DefaultRequestHeaders.Add("client-name", "namedClient");
                 client.BaseAddress = new Uri("https://localhost:7001");
             }).SetHandlerLifetime(TimeSpan.FromMinutes(10))
             .AddHttpMessageHandler(provider => provider.GetService<RequestDelegatingHandler>())
             .ConfigurePrimaryHttpMessageHandler(()=>handler);

            services.AddScoped<NamedOrderServiceClient>();


            //���Ϳͻ���:�Ƽ�ʹ��
            //Ϊ��ͬ��client ��������
            services.AddHttpClient<TypedOrderServiceClient>(client =>
            {
                client.DefaultRequestHeaders.Add("client-name", "namedClient");
                client.BaseAddress = new Uri("https://localhost:7001");
            }).ConfigurePrimaryHttpMessageHandler(() => handler);



            #region Grpc

            services.AddGrpc(options =>
            {
                //�������ϸ��Ϣ
                options.EnableDetailedErrors = false;
                //����������
               // options.Interceptors.Add<ExceptionInterceptor>();
            });

            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<BookService>();
                endpoints.MapControllers();
            });
        }
    }
}
