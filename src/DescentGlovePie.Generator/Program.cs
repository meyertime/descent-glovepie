using System;
using System.IO;
using Newtonsoft.Json;

namespace DescentGlovePie.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new JsonSerializer();
            using (var file = File.OpenRead("../../../maps/ps3.json"))
            {
                var map = serializer.Deserialize<Map>(new JsonTextReader(new StreamReader(file)));

                Console.ReadLine();
            }
        }
    }
}
