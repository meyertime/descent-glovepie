using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DescentGlovePie.Generator
{
    public class ButtonMappingsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (ButtonMapping[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                throw new JsonException("Expected {");

            var results = new List<ButtonMapping>();

            reader.Read();
            while (reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    throw new JsonException("Expected property name");

                var input = (string) reader.Value;
                reader.Read();

                if (reader.TokenType == JsonToken.String)
                {
                    var output = (string) reader.Value;
                    results.Add(new ButtonMapping
                    {
                        Input = input,
                        NormalOutput = StringToMappingOutput(output),
                        ShiftMappings = new ShiftButtonMapping[0]
                    });
                }
                else if (reader.TokenType == JsonToken.StartObject)
                {
                    reader.Read();
                    results.Add(ReadComplexButtonMapping(input, reader, serializer));
                }
                else
                {
                    throw new JsonException("Expected string or {");
                }

                reader.Read();
            }

            return results.ToArray();
        }

        private MappingOutput StringToMappingOutput(string s)
        {
            if (s.StartsWith("~"))
            {
                s = s.Substring(1);
                return new KeyboardMacroOutput
                {
                    Outputs = s.Split(' ').Select(t => t.Split('+')).ToArray()
                };
            }

            return new BasicMappingOutput
            {
                Output = s
            };
        }

        private ButtonMapping ReadComplexButtonMapping(string input, JsonReader reader, JsonSerializer serializer)
        {
            var normalOutput = (MappingOutput) null;
            var shiftMappings = new List<ShiftButtonMapping>();

            while (reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    throw new JsonException("Expected property name");

                var shiftInput = (string) reader.Value;

                reader.Read();

                if (reader.TokenType != JsonToken.String)
                    throw new JsonException("Expected string");

                var output = (string) reader.Value;

                if (shiftInput == "normal")
                {
                    normalOutput = StringToMappingOutput(output);
                }
                else
                {
                    shiftMappings.Add(new ShiftButtonMapping
                    {
                        ShiftInput = input,
                        Output = StringToMappingOutput(output)
                    });
                }

                reader.Read();
            }

            return new ButtonMapping
            {
                Input = input,
                NormalOutput = normalOutput,
                ShiftMappings = shiftMappings.ToArray()
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}