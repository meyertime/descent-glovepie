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
        public string ButtonOutput { get; set; }

        [JsonProperty("forwardDigital")]
        public string ForwardDigitalInput { get; set; }

        [JsonProperty("forwardAnalog")]
        public string ForwardAnalogInput { get; set; }

        [JsonProperty("reverseDigital")]
        public string ReverseDigitalInput { get; set; }

        [JsonProperty("reverseAnalog")]
        public string ReverseAnalogInput { get; set; }
    }
}