using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Nate.Core.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteAsJsonAsync<T>(this HttpResponse response, T value, JsonSerializerOptions options = null)
        {
            var jsonString = JsonSerializer.Serialize(value, options);
            await response.WriteAsync(jsonString);
        }
    }
}
