using System;
using System.Collections.Generic;
using System.Linq;
using iSynaptic.Commons;

namespace DescentGlovePie.Generator
{
    public class ScriptPostprocessor : IScriptPostprocessor
    {
        public string Postprocess(string input)
        {
            var lines = SplitLines(input);
            lines = ProcessAfterComments(lines, TrimLines);
            lines = ProcessAfterComments(lines, NormalizeEmptyLines);
            lines = ProcessAfterComments(lines, NormalizeIndentation);
            return String.Join("\r\n", lines);
        }

        private IEnumerable<string> SplitLines(string input)
        {
            return input.Split('\n').Select(l => l.EndsWith("\r") ? l.Substring(0, l.Length - 1) : l);
        }

        private IEnumerable<string> ProcessAfterComments(IEnumerable<string> lines, Func<IEnumerable<string>, IEnumerable<string>> process)
        {
            var enumerator = lines.GetEnumerator();
            if (!enumerator.MoveNext())
                yield break;
            
            if (enumerator.Current.Contains("/*"))
            {
                while (!enumerator.Current.Contains("*/"))
                {
                    yield return enumerator.Current;
                    if (!enumerator.MoveNext())
                        yield break;
                }
                yield return enumerator.Current;
            }

            var remainder = new ContinueEnumerable<string>(enumerator);
            foreach (var processed in process(remainder))
            {
                yield return processed;
            }
        }

        private IEnumerable<string> TrimLines(IEnumerable<string> lines)
        {
            return lines.Select(l => l.Trim());
        }

        private IEnumerable<string> NormalizeEmptyLines(IEnumerable<string> lines)
        {
            yield return "";
            foreach (var line in lines)
            {    
                if (line == "")
                    continue;

                if (line.StartsWith("//"))
                {
                    yield return "";
                    if (line.EndsWith(":"))
                        yield return "";
                }

                yield return line;
            }
        }

        private IEnumerable<string> NormalizeIndentation(IEnumerable<string> lines)
        {
            int indent = 0;
            foreach (var line in lines)
            {
                if (line.StartsWith("}"))
                    indent--;
                
                yield return new String(' ', 4 * indent) + line;

                if (line.EndsWith("{"))
                    indent++;
            }
        }

        private class ContinueEnumerable<T> : IEnumerable<T>
        {
            private readonly IEnumerator<T> _enumerator;
 
            public ContinueEnumerable(IEnumerator<T> enumerator)
            {
                _enumerator = Guard.NotNull(enumerator, "enumerator");
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _enumerator;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _enumerator;
            }
        }
    }
}
