using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TravelRecordApp.Deserializers
{
    public class Deserializer
    {
        public static async Task<T> DeserializeToGeneric<T>(HttpResponseMessage responseMessage)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
