#nullable enable
using System;
using Jour.Webhooks.Telegram;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Jour.Webhooks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Jour.Webhooks", Version = "v1"});
            });
            
            string? telegramEndpoints = Environment.GetEnvironmentVariable("JOUR_WEBHOOKS_TelegramEndpoints");
            if (string.IsNullOrEmpty(telegramEndpoints))
                throw new ArgumentNullException(nameof(telegramEndpoints));

            TelegramHelper.AddTelegramTransformer(services, telegramEndpoints);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jour.Webhooks v1"));
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "telegram", pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                string pattern = $"{{{TelegramHelper.Name}}}/{{controller}}/{{key}}";
                endpoints.MapDynamicControllerRoute<TelegramTransformer>(pattern);
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}