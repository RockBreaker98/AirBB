using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Repositories
{
    public class LocationRepository : Repository<Location>
    {
        public LocationRepository(AirBBContext ctx) : base(ctx) { }
    }
}