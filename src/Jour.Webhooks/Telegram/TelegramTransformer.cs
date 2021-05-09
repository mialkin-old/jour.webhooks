using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Jour.Webhooks.Telegram
{
    public class TelegramTransformer : DynamicRouteValueTransformer
    {
        private readonly TelegramEndpoints _endpoints;

        public TelegramTransformer(TelegramEndpoints endpoints)
        {
            _endpoints = endpoints;
        }

        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext,
            RouteValueDictionary values)
        {
            if ((string) values[TelegramHelper.Name]! != TelegramHelper.Name)
                return new ValueTask<RouteValueDictionary>(values);

            if (_endpoints.Exists((string) values["controller"]!, (string) values["key"]!))
                values["action"] = "Update";

            return new ValueTask<RouteValueDictionary>(values);
        }
    }
}