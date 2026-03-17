using System.Text.Json.Serialization;

namespace barsonar_desktop.classes
{
    public class Place
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("googlePlaceID")]
        public string GooglePlaceID { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("address")]
        public string Address { get; set; } = "";
    }
}
