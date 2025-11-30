using Microsoft.AspNetCore.Http;

namespace AirBB.Models.Utilities
{
    public class AirBBSession
    {
        private readonly ISession _session;
        private const string LocKey = "activeLocationId";
        private const string StartKey = "startDate";
        private const string EndKey = "endDate";
        private const string GuestsKey = "guests";
        private const string ResCountKey = "reservationCount";

        public AirBBSession(ISession session) => _session = session;

        public void SetFilters(string? locId, DateTime? start, DateTime? end, int guests)
        {
            _session.SetString(LocKey, string.IsNullOrWhiteSpace(locId) ? "all" : locId!);
            _session.SetString(StartKey, start?.ToString("o") ?? "");
            _session.SetString(EndKey, end?.ToString("o") ?? "");
            _session.SetInt32(GuestsKey, guests <= 0 ? 1 : guests);
        }

        public (string locId, DateTime? start, DateTime? end, int guests) GetFilters()
        {
            var loc = _session.GetString(LocKey) ?? "all";
            var startStr = _session.GetString(StartKey);
            var endStr = _session.GetString(EndKey);
            DateTime? s = string.IsNullOrEmpty(startStr) ? null : DateTime.Parse(startStr);
            DateTime? e = string.IsNullOrEmpty(endStr) ? null : DateTime.Parse(endStr);
            var g = _session.GetInt32(GuestsKey) ?? 1;
            return (loc, s, e, g);
        }

        public int GetReservationCount() => _session.GetInt32(ResCountKey) ?? 0;
        public void SetReservationCount(int n) => _session.SetInt32(ResCountKey, n);
    }
}
