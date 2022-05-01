using Email.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Email.Infrastructure.Configurations
{
    public static class TemplateConfig
    {
        public static IServiceCollection GetMongoSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));
            return services;
        }
    }
}
