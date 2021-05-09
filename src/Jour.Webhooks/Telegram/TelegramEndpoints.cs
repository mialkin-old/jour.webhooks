using System;
using System.Collections.Generic;

namespace Jour.Webhooks.Telegram
{
    public class TelegramEndpoints
    {
        public readonly Dictionary<string, string> Endpoints;

        public TelegramEndpoints()
        {
            Endpoints = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public bool Exists(string key, string? value)
        {
            if (!Endpoints.ContainsKey(key)) return false;
            
            return Endpoints[key] == value;
        }
    }
}