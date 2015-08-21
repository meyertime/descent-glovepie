using System;
using Newtonsoft.Json;

namespace DescentGlovePie.Generator
{
    [Serializable]
    public class Map
    {
        [JsonProperty("input")]
        public string InputPrefix { get; set; }

        [JsonProperty("alias")]
        [JsonConverter(typeof(InputAliasesConverter))]
        public InputAlias[] InputAliases { get; set; }

        [JsonProperty("shift")]
        public string[] ShiftButtons { get; set; }

        [JsonProperty("axis")]
        [JsonConverter(typeof(AxisMappingsConverter))]
        public AxisMapping[] AxisMappings { get; set; }

        [JsonProperty("button")]
        [JsonConverter(typeof(ButtonMappingsConverter))]
        public ButtonMapping[] ButtonMappings { get; set; }

        [JsonProperty("throttle")]
        public ThrottleMapping ThrottleMapping { get; set; }
    }
}
