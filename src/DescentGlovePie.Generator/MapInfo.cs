using System;
using iSynaptic.Commons;

namespace DescentGlovePie.Generator
{
    [Serializable]
    public class MapInfo
    {
        private readonly string _name;
        private readonly string _fileName;
        private readonly string _comments;
        private readonly Map _map;

        public MapInfo(string name, string fileName, string comments, Map map)
        {
            _name = Guard.NotNullOrEmpty(name, "name");
            _fileName = Guard.NotNullOrEmpty(fileName, "fileName");
            _comments = comments;
            _map = Guard.NotNull(map, "map");
        }

        public string Name { get { return _name; } }
        public string FileName { get { return _fileName; } }
        public string Comments { get { return _comments; } }
        public Map Map { get { return _map; } }
    }
}