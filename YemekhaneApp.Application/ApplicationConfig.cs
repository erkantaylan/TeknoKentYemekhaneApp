using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace YemekhaneApp.Application
{
    public static class ApplicationLayerConfig
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        }
    }
}
