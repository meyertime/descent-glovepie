using System;
using Newtonsoft.Json;

namespace DescentGlovePie.Generator
{
    [Serializable]
    public class ThrottleMapping
    {
        [JsonProperty("axis")]
        public string AxisOutput { get; set; }

        [JsonProperty("button")]
        public string BothOutput { get; set; }

        [JsonProperty("forwardButton")]
        public string ForwardOutput { get; set; }

        [JsonProperty("reverseButton")]
        public string ReverseOutput { get; set; }

        [JsonProperty("forwardDigital")]
        public string ForwardDigitalInput { get; set; }

        [JsonProperty("forwardAnalog")]
        public string ForwardAnalogInput { get; set; }

        [JsonProperty("reverseDigital")]
        public string ReverseDigitalInput { get; set; }

        [JsonProperty("reverseAnalog")]
        public string ReverseAnalogInput { get; set; }

        [JsonProperty("analogThreshold")]
        public double? AnalogThreshold { get; set; }
    }
}