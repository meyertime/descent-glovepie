using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using iSynaptic.Commons;

namespace DescentGlovePie.Generator
{
    public class MapStore : IMapStore
    {
        private readonly string _directory;

        private readonly JsonSerializer _json;

        public MapStore(string directory)
        {
            _directory = Guard.NotNullOrEmpty(directory, "directory");

            _json = new JsonSerializer();
        }

        public IEnumerable<string> GetNames()
        {
            return Directory.GetFiles(_directory, "*.json").Select(Path.GetFileNameWithoutExtension).ToList();
        }

        public MapInfo GetByName(string name)
        {
            var path = Path.Combine(_directory, name);
            Map map;
            string comments;

            using (var file = File.OpenRead(path + ".json"))
            {
                map = _json.Deserialize<Map>(new JsonTextReader(new StreamReader(file)));
            }

            try
            {
                using (var file = File.OpenRead(path + ".txt"))
                {
                    comments = new StreamReader(file).ReadToEnd().TrimEnd();
                }
            }
            catch (FileNotFoundException)
            {
                comments = null;
            }

            return new MapInfo(name, name + ".pie", comments, map);
        }
    }
}
