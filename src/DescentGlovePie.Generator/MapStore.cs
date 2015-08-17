using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using iSynaptic.Commons;

namespace DescentGlovePie.Generator
{
    public class MapStore : IMapStore
    {
        private readonly string _directory;

        private readonly JsonSerializer _json;
        private readonly JSchema _schema;

        public MapStore(string directory)
        {
            _directory = Guard.NotNullOrEmpty(directory, "directory");

            _json = new JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };

            var stream = typeof(Program).Assembly.GetManifestResourceStream("DescentGlovePie.Generator.map.schema.json");
            if (stream == null)
                throw new ApplicationException("Missing assembly resource for map.schema.json.");

            using (stream)
            {
                _schema = JSchema.Load(new JsonTextReader(new StreamReader(stream)));
            }
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
                var reader = new JSchemaValidatingReader(new JsonTextReader(new StreamReader(file)))
                {
                    Schema = _schema
                };

                map = _json.Deserialize<Map>(reader);
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
