using System.Collections.Generic;

namespace DescentGlovePie.Generator
{
    public interface IMapStore
    {
        MapInfo GetByName(string name);
        IEnumerable<string> GetNames();
    }
}