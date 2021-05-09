using System;
using Microsoft.Extensions.DependencyInjection;

namespace Jour.Webhooks.Telegram
{
    public static class TelegramHelper
    {
        public const string Name = "telegram";

        public static void AddTelegramTransformer(IServiceCollection services, string telegramEndpoints)
        {
            var endpoints = new TelegramEndpoints();

            foreach (string pair in telegramEndpoints.Split(';'))
            {
                string key = pair.Split('=')[0];
                string value = pair.Split('=')[1];

                endpoints.Endpoints.Add(key, value);
            }

            services.AddSingleton(endpoints);

            services.AddSingleton<TelegramTransformer>();
        }
    }
}