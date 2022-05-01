using Email.Application.Interfaces.Repository;
using Email.Infrastructure.Database;
using Email.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Email.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection GetDependencyInjectionInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<ITemplateContext, TemplateContext>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            return services;
        }
    }
}
