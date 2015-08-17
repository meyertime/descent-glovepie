using System;
using System.IO;
using System.Text;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using iSynaptic.Commons;

namespace DescentGlovePie.Generator
{
    public class ScriptGenerator : IScriptGenerator
    {
        private readonly AppDomain _domain;
        private readonly RazorEngineDomainProxy _proxy;

        public ScriptGenerator()
        {
            _domain = AppDomain.CreateDomain("RazorEngineAppDomain");
            try
            {
                _proxy = (RazorEngineDomainProxy) _domain.CreateInstanceAndUnwrap(typeof (RazorEngineDomainProxy).Assembly.FullName, typeof (RazorEngineDomainProxy).FullName);
                _proxy.Initialize();
            }
            catch (Exception)
            {
                AppDomain.Unload(_domain);
                throw;
            }
        }

        public string Generate(MapInfo map)
        {
            return _proxy.Generate(map);
        }

        private class RazorEngineDomainProxy : MarshalByRefObject
        {
            private const string TemplateName = "GlovePieScript.cshtml";

            private IRazorEngineService _razor;

            public void Initialize()
            {
                string templateSource;
                var stream = typeof(Program).Assembly.GetManifestResourceStream("DescentGlovePie.Generator.Views." + TemplateName);
                if (stream == null)
                    throw new ApplicationException("Missing assembly resource for Razor view " + TemplateName);

                using (stream)
                {
                    templateSource = new StreamReader(stream).ReadToEnd();
                }

                // This template uses iSynaptic.Commons, so let's make sure the runtime loads it
                templateSource = templateSource.ToMaybe().Value;

                var config = new TemplateServiceConfiguration
                {
                    EncodedStringFactory = new RawStringFactory()
                };
                
                _razor = RazorEngineService.Create(config);

                _razor.Compile(templateSource, TemplateName, typeof(MapInfo));
            }

            public string Generate(MapInfo map)
            {
                var sb = new StringBuilder();
                var writer = new StringWriter(sb);
                _razor.Run(TemplateName, writer, typeof(MapInfo), map);
                writer.Flush();
                return sb.ToString();
            }
        }
    }
}
