using AirBB.Models.DomainModels;

namespace AirBB.Models.DataLayer.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(AirBBContext ctx) : base(ctx) { }
    }
}