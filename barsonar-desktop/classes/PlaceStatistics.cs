using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace barsonar_desktop.classes
{
    public class PlaceStatistics
    {
        [JsonPropertyName("placeId")]
        public int PlaceId { get; set; }

        [JsonPropertyName("placeName")]
        public string PlaceName { get; set; } = "";

        [JsonPropertyName("totalPhotos")]
        public int TotalPhotos { get; set; }

        [JsonPropertyName("totalComments")]
        public int TotalComments { get; set; }

        [JsonPropertyName("averageRating")]
        public double AverageRating { get; set; }

        public int PopularityScore => TotalPhotos + TotalComments + (int)(AverageRating * 2);
    }
}
