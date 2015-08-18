using System.IO;

namespace DescentGlovePie.Generator
{
    public interface IScriptStore
    {
        TextWriter OpenForWriting(string name);
    }
}