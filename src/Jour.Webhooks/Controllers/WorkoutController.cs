using System.Text.Json;
using System.Threading.Tasks;
using Jour.Webhooks.Rabbit;
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
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<WorkoutController> _logger;

        public WorkoutController(IMessageBroker messageBroker, ILogger<WorkoutController> logger)
        {
            _messageBroker = messageBroker;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        } 
        
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Update update)
        {
            if (update.Type != UpdateType.Message)
            {
                return Ok();
            }

            Message message = update.Message;
            string jsonMessage = JsonSerializer.Serialize(message);
            _logger.LogInformation("Message received from Telegram server: {JsonMessage}", jsonMessage);

            switch (message.Type)
            {
                case MessageType.Text:
                    _messageBroker.PublishMessage(queueName: "telegram-workout-received", message.Text);
                    break;

                case MessageType.Photo:
                    // Download photo
                    await Task.Yield();
                    break;
            }

            return Ok();
        }
    }
}