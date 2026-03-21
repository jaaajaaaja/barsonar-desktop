using System;
using System.Collections.Generic;
using System.Text;

namespace barsonar_desktop.classes
{
    public class ChartBarData
    {
        public int Rank { get; set; }
        public string PlaceName { get; set; } = "";
        public int TotalPhotos { get; set; }
        public int TotalComments { get; set; }
        public double AverageRating { get; set; }
        public int PopularityScore { get; set; }
        public double BarWidth { get; set; }
        public string StatsText => $"{TotalPhotos} fotó • {TotalComments} komment • ⭐ {AverageRating:F1}";
    }
}
