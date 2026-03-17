using System.Text.Json.Serialization;

namespace barsonar_desktop.classes
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userName")]
        public string Username { get; set; } = "";

        [JsonPropertyName("email")]
        public string Email { get; set; } = "";

        [JsonPropertyName("password")]
        public string Password { get; set; } = "";

        [JsonPropertyName("age")]
        public int? Age { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; } = "user";
    }
}
