using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Email.API.Filters;
using Email.Application.Configurations;
using Email.Application.Settings;
using Email.Infrastructure.Configurations;
using FluentValidation.AspNetCore;
using Hangfire;

namespace Email.API
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
            //services.AddScoped<CustomValidatorResponse>();
            services.Configure<EmailSettings>(this.Configuration.GetSection("EmailSettings"));
            services.GetDependencyInjectionInfrastructure();
            services.GetMongoSettings(this.Configuration);
            services.GetDependencyInjectionApplicationLayer();
            services.GetHangfire(this.Configuration);
            CacheSettings redisSettings = new();

            Configuration.GetSection("CacheSettings").Bind(redisSettings);

            if (!redisSettings.Enabled)
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                    options.Configuration = redisSettings.RedisConnection);
            }

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CustomValidatorResponse));
            }).AddFluentValidation();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmailFileAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Email.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard("/hangfire-dashboard");
        }
    }
}
