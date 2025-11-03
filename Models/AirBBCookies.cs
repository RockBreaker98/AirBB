using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirBB.Models
{
    public class AirBBCookies
    {
        private const string ResIdsKey = "airbb_reservation_ids";
        private readonly IRequestCookieCollection _requestCookies;
        private readonly IResponseCookies _responseCookies;

        // One-constructor version (used for read-only)
        public AirBBCookies(IRequestCookieCollection requestCookies)
        {
            _requestCookies = requestCookies;
            _responseCookies = null!;
        }

        // Two-parameter constructor (used for both read/write in controller)
        public AirBBCookies(IRequestCookieCollection requestCookies, IResponseCookies responseCookies)
        {
            _requestCookies = requestCookies;
            _responseCookies = responseCookies;
        }

        // Get IDs from cookie
        public int[] GetReservationIds()
        {
            if (_requestCookies.TryGetValue(ResIdsKey, out var csv) && !string.IsNullOrWhiteSpace(csv))
            {
                return csv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                          .Select(s => int.TryParse(s, out var id) ? id : (int?)null)
                          .Where(id => id.HasValue)
                          .Select(id => id!.Value)
                          .ToArray();
            }
            return Array.Empty<int>();
        }

        // Write IDs to cookie
        public void WriteReservationIds(IEnumerable<int> ids)
        {
            if (_responseCookies == null) return;

            var value = string.Join(",", ids);
            _responseCookies.Append(ResIdsKey, value, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                HttpOnly = true,
                IsEssential = true
            });
        }

        // Optional: clear cookie if needed
        public void ClearReservationIds()
        {
            _responseCookies?.Delete(ResIdsKey);
        }
    }
}
