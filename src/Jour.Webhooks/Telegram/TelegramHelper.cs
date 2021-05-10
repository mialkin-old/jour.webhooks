using System;
using Microsoft.Extensions.DependencyInjection;

namespace Jour.Webhooks.Telegram
{
    public static class TelegramHelper
    {
        public const string Name = "telegram";

        public static TelegramEndpoints GetTelegramEndpoints(IServiceCollection services, string endpointsString)
        {
            var endpoints = new TelegramEndpoints();

            foreach (string pair in endpointsString.Split(';'))
            {
                string key = pair.Split('=')[0];
                string value = pair.Split('=')[1];

                endpoints.Endpoints.Add(key, value);
            }

            return endpoints;
        }
    }
}