using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Jour.Webhooks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly ILogger<WorkoutController> _logger;

        public WorkoutController(ILogger<WorkoutController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Update update)
        {
            await Task.Yield();

            if (update.Type != UpdateType.Message)
            {
                return Ok();
            }

            Message message = update.Message;
            string json = JsonSerializer.Serialize(message);
            
            _logger.LogInformation(json);

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