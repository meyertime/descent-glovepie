using System.IO;
using iSynaptic.Commons;

namespace DescentGlovePie.Generator
{
    public class ScriptStore : IScriptStore
    {
        private readonly string _directory;

        public ScriptStore(string directory)
        {
            _directory = Guard.NotNullOrEmpty(directory, "directory");
        }

        public TextWriter OpenForWriting(string name)
        {
            Directory.CreateDirectory(_directory);
            var file = File.Create(Path.Combine(_directory, name + ".pie"));
            return new StreamWriter(file);
        }
    }
}
