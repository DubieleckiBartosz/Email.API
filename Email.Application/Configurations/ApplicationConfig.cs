using Email.Application.Interfaces.Service;
using Email.Application.Models.Dto;
using Email.Application.Services;
using Email.Application.Validators;
using FluentValidation;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Email.Application.Configurations
{
    public static class ApplicationConfig
    {
        public static IServiceCollection GetDependencyInjectionApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();

            //Validators
            services.AddTransient<IValidator<EmailDto>, EmailValidator>();
            return services;
        }

        public static IServiceCollection GetHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x =>
                x.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"))
            );

            services.AddHangfireServer();

            return services;
        }
    }
}
