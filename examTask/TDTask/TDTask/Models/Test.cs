using Newtonsoft.Json;

namespace TDTask.Models
{
    public class Test
    {
        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Test other && Name == other.Name && Method == other.Method && Status == other.Status && StartTime == other.StartTime && EndTime == other.EndTime && Duration == other.Duration;
        }
    }
}
