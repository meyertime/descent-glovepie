using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DescentGlovePie.Generator
{
    public class AxisMappingsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (AxisMapping[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                throw new JsonException("Expected {");

            var results = new List<AxisMapping>();

            reader.Read();
            while (reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    throw new JsonException("Expected property name");

                var input = (string) reader.Value;
                reader.Read();

                if (reader.TokenType != JsonToken.String)
                    throw new JsonException("Expected string");

                var output = (string) reader.Value;
                var invert = output.StartsWith("-");
                if (invert)
                    output = output.Substring(1);

                results.Add(new AxisMapping
                {
                    Input = input,
                    Output = output,
                    Invert = invert
                });

                reader.Read();
            }

            return results.ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}