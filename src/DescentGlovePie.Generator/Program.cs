using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iSynaptic.Commons;

namespace DescentGlovePie.Generator
{
    class Program
    {
        private static IMapStore _mapStore;
        private static IScriptStore _scriptStore;
        private static IScriptGenerator _scriptGenerator;
        private static IScriptPostprocessor _scriptPostprocessor;

        public Program(
            IMapStore mapStore, 
            IScriptStore scriptStore, 
            IScriptGenerator scriptGenerator,
            IScriptPostprocessor scriptPostprocessor)
        {
            _mapStore = Guard.NotNull(mapStore, "mapStore");
            _scriptStore = Guard.NotNull(scriptStore, "scriptStore");
            _scriptGenerator = Guard.NotNull(scriptGenerator, "scriptGenerator");
            _scriptPostprocessor = Guard.NotNull(scriptPostprocessor, "scriptPostprocessor");
        }

        public void Run(IEnumerable<string> names)
        {
            var namesMemo = (names != null) ? names.ToList() : null;
            if (namesMemo == null || namesMemo.Count == 0)
                namesMemo = _mapStore.GetNames().ToList();

            Console.WriteLine("Reading maps...");
            var maps = namesMemo.Select(name =>
            {
                Console.WriteLine(name);
                return _mapStore.GetByName(name);
            }).ToList();

            Console.WriteLine();
            Console.WriteLine("Generating scripts...");
            foreach (var map in maps)
            {
                Console.WriteLine(map.FileName);
                var script = _scriptGenerator.Generate(map);
                script = _scriptPostprocessor.Postprocess(script);
                using (var writer = _scriptStore.OpenForWriting(map.Name))
                {
                    writer.Write(script);
                    writer.Flush();
                }
            }

            Console.WriteLine();
            Console.WriteLine("Done!");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("DescentGlovePie.Generator");
            
            if (args.Length < 2)
            {
                Console.WriteLine();
                Console.WriteLine("Usage: DescentGlovePie.Generator.exe MapsPath OutputPath [ScriptNames ...]");
                return;
            }

            var mapsDir = Path.GetFullPath(args[0]);
            var scriptsDir = Path.GetFullPath(args[1]);

            Console.WriteLine("Maps directory: " + mapsDir);
            Console.WriteLine("Output directory: " + scriptsDir);
            Console.WriteLine();

            var program = new Program(
                new MapStore(mapsDir),
                new ScriptStore(scriptsDir),
                new ScriptGenerator(),
                new ScriptPostprocessor()
            );

            program.Run(args.Skip(2));


#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
