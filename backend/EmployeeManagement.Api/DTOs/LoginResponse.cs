using System.Text.Json.Serialization;
using Backend.Enums;
public class LoginResponse
{

    public string Token {get;set;}

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRole Role { get;set;}
}