using System.Text.Json.Serialization;
using Backend.Enums;

namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] //Jsono—Í‚É•¶š—ñ‚Ö•ÏŠ·
        public UserRole Role { get; set; }

        //Employee Employee { get; set; }

    }
}
