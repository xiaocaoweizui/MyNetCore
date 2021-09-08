using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNetEFCore.Events;
using System;

namespace MyNetEFCore.Extension
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(StudentContextTransactionBehavior<,>));
            return services.AddMediatR(typeof(Student).Assembly, typeof(Program).Assembly);
        }


        public static IServiceCollection AddDomainContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddDbContext<StudentContext>(optionsAction);
        }

        public static IServiceCollection AddSqlDomainContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDomainContext(builder =>
            {
                builder.UseSqlServer(connectionString);
            });
        }


        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            return services;
        }



        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddCap(options =>
            {
                options.UseEntityFramework<StudentContext>();

                options.UseRabbitMQ(options =>
                {
                    configuration.GetSection("RabbitMQ").Bind(options);
                });
                //options.UseDashboard();
            });

            return services;
        }
    }
}
