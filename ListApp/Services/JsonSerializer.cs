using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using NetJsonSerializer = System.Text.Json.JsonSerializer;

namespace ListApp.Services
{
    public class JsonSerializer : ISerializer
    {
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            AllowTrailingCommas = false,
            PropertyNameCaseInsensitive = true
        };

        public T Deserialize<T>(string content) where T: new()
        {
            return NetJsonSerializer.Deserialize<T>(content, options);
        }

        public async Task<T> DeserializeAsync<T>(Stream content) where T : new()
        {
            return await NetJsonSerializer.DeserializeAsync<T>(content, options);
        }

        public string Serialize<T>(T objectToSerialize) where T : new()
        {
            return NetJsonSerializer.Serialize(objectToSerialize, options);
        }
    }
}
