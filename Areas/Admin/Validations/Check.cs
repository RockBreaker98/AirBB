using System.Linq;
using AirBB.Models;
using AirBB.Models.DataLayer;
using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Areas.Admin.Validations
{
    public static class Check
    {
        public static string EmailExists(AirBBContext ctx, string? email, int? excludeClientId = null)
        {
            if (string.IsNullOrWhiteSpace(email)) return string.Empty;

            var exists = ctx.Clients.Any(u =>
                (u.Email ?? "").ToLower() == email.ToLower() &&
                (!excludeClientId.HasValue || u.ClientId != excludeClientId.Value));

            return exists ? $"Email address {email} already exists." : string.Empty;
        }

        public static string PhoneExists(AirBBContext ctx, string? phone, int? excludeClientId = null)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return string.Empty;

            // Normalize input to digits only
            var targetDigits = new string(phone.Where(char.IsDigit).ToArray());

            // Pull minimal fields from DB, then switch to in-memory for digit-stripping
            var exists = ctx.Clients
                .AsNoTracking()
                .Select(u => new { u.ClientId, u.PhoneNumber })
                .Where(u => u.PhoneNumber != null)        // filter nulls in SQL
                .AsEnumerable()                            // from here we can use char.IsDigit safely
                .Any(u =>
                {
                    var digits = new string((u.PhoneNumber ?? string.Empty)
                                            .Where(char.IsDigit).ToArray());  // <- handles nulls
                    return digits == targetDigits
                        && (!excludeClientId.HasValue || u.ClientId != excludeClientId.Value);
                });

            return exists ? $"Phone number {phone} already exists." : string.Empty;
        }
    }
}