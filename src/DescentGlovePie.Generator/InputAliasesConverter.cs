using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DescentGlovePie.Generator
{
    public class InputAliasesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (InputAlias[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                throw new JsonException("Expected {");

            var results = new List<InputAlias>();

            reader.Read();
            while (reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    throw new JsonException("Expected property name");

                var alias = (string)reader.Value;
                reader.Read();

                if (reader.TokenType != JsonToken.String)
                    throw new JsonException("Expected string");

                var value = (string)reader.Value;
                
                results.Add(new InputAlias
                {
                    Name = alias,
                    Value = value
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