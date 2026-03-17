using System.Text.Json.Serialization;

namespace barsonar_desktop.classes
{
    class Photo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = "";

        [JsonPropertyName("userID")]
        public int UserID { get; set; }

        [JsonPropertyName("placeID")]
        public int PlaceID { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; } = "";

        [JsonPropertyName("approved")]
        public bool Approved { get; set; }
    }
}
