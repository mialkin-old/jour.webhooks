using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Jour.Webhooks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new {ok = true});
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Update update)
        {
            await Task.Yield();

            if (update.Type != UpdateType.Message)
            {
                return Ok();
            }

            var message = update.Message;

            switch (message.Type)
            {
                case MessageType.Text:
                    // Echo each Message
                    break;

                case MessageType.Photo:
                    // Download Photo
                    break;
            }

            return Ok();
        }
    }
}