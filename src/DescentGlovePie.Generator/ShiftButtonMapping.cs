using System;

namespace DescentGlovePie.Generator
{
    [Serializable]
    public class ShiftButtonMapping
    {
        public string ShiftInput { get; set; }
        public MappingOutput Output { get; set; }
    }
}