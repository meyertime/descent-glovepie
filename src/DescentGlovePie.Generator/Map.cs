using Newtonsoft.Json;

namespace DescentGlovePie.Generator
{
    public class Map
    {
        [JsonProperty("input")]
        public string InputPrefix { get; set; }

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

    public class AxisMapping
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public bool Invert { get; set; }
    }

    public class ButtonMapping
    {
        public string Input { get; set; }
        public MappingOutput NormalOutput { get; set; }
        public ShiftButtonMapping[] ShiftMappings { get; set; }
    }

    public class ShiftButtonMapping
    {
        public string ShiftInput { get; set; }
        public MappingOutput Output { get; set; }
    }

    public abstract class MappingOutput
    { }

    public class BasicMappingOutput : MappingOutput
    {
        public string Output { get; set; }
    }

    public class KeyboardMacroOutput : MappingOutput
    {
        public string[][] Outputs { get; set; }
    }

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
