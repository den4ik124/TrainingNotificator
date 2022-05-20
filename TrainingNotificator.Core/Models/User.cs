using System.ComponentModel.DataAnnotations;

namespace TrainingNotificator.Core.Models
{
    public class CustomUser : Telegram.Bot.Types.User
    {
        [Key]
        public long IdentityId { get; set; }
    }
}