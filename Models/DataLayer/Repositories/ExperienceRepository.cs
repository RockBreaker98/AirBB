using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Repositories
{
    public class ExperienceRepository : Repository<Experience>
    {
        public ExperienceRepository(AirBBContext ctx) : base(ctx) { }
    }
}