using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Repositories
{
    public class ResidenceRepository : Repository<Residence>
    {
        public ResidenceRepository(AirBBContext ctx) : base(ctx) { }
    }
}