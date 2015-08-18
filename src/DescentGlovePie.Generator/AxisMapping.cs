using System;

namespace DescentGlovePie.Generator
{
    [Serializable]
    public class AxisMapping
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public bool Invert { get; set; }
    }
}