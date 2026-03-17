using System.Text.Json.Serialization;

namespace barsonar_desktop.classes
{
    class Comment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("commentText")]
        public string CommentText { get; set; } = "";

        [JsonPropertyName("rating")]
        public int? Rating { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("userID")]
        public int UserID { get; set; }

        [JsonPropertyName("placeID")]
        public int PlaceID { get; set; }

        [JsonPropertyName("approved")]
        public bool Approved { get; set; }
    }
}
