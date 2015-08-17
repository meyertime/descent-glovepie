using System;

namespace DescentGlovePie.Generator
{
    [Serializable]
    public class KeyboardMacroOutput : MappingOutput
    {
        public string[][] Outputs { get; set; }
    }
}