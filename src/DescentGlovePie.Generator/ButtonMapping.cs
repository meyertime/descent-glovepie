using System;

namespace DescentGlovePie.Generator
{
    [Serializable]
    public class ButtonMapping
    {
        public string Input { get; set; }
        public MappingOutput NormalOutput { get; set; }
        public ShiftButtonMapping[] ShiftMappings { get; set; }
    }
}