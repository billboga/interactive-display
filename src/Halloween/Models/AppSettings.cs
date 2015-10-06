using System.Collections.Generic;

namespace Halloween.Models
{
    public class AppSettings
    {
        public string DisplayHeight { get; set; }
        public string DisplayWidth { get; set; }
        public Dictionary<string, Dictionary<string, string>> Displays { get; set; }
        public bool PortraitMode { get; set; }
    }
}
