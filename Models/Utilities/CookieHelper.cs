using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AirBB.Models.Utilities
{
    public static class CookieHelper
    {
        public static void SetObject<T>(this IResponseCookies cookies, string key, T value, int days = 7)
        {
            var options = new CookieOptions { Expires = DateTime.Now.AddDays(days) };
            cookies.Append(key, JsonSerializer.Serialize(value), options);
        }

        public static T? GetObject<T>(this IRequestCookieCollection cookies, string key)
        {
            if (cookies.TryGetValue(key, out string? value))
                return JsonSerializer.Deserialize<T>(value);
            return default;
        }
    }
}
