using System.ComponentModel.DataAnnotations;

namespace Jour.Webhooks.Options
{
    public class RabbitOptions
    {
        public const string Rabbit = "Rabbit";

        [Required(AllowEmptyStrings = false)]
        public string Hostname { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; } = null!;
    }
}