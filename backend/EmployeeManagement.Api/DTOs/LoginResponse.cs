using System.Text.Json.Serialization;
using Backend.Enums;

namespace Backend.DTOs
{
    public class LoginResponse
    {

        public string Token { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
    }
}