using Microsoft.EntityFrameworkCore;

namespace AirBB.Models
{
    public class AirBBContext : DbContext
    {
        public AirBBContext(DbContextOptions<AirBBContext> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Experience> Experiences => Set<Experience>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Categories (left filter)
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Beach" },
                new Category { CategoryId = 2, Name = "City" },
                new Category { CategoryId = 3, Name = "Mountains" }
            );

            // Experiences (table on the right)
            modelBuilder.Entity<Experience>().HasData(
                new Experience { ExperienceId = 1,  CategoryId = 1, Title = "Oceanfront Bungalow", Description = "Private deck, sunset views.", Price = 199.00m, DiscountPercent = 10 },
                new Experience { ExperienceId = 2,  CategoryId = 1, Title = "Surf Lessons Package",   Description = "Beginner to intermediate.", Price = 129.00m, DiscountPercent = 0  },
                new Experience { ExperienceId = 3,  CategoryId = 2, Title = "Downtown Loft Stay",     Description = "Walk to museums & food.", Price = 149.00m, DiscountPercent = 15 },
                new Experience { ExperienceId = 4,  CategoryId = 2, Title = "Food Tour",              Description = "Street food & cafes.",   Price = 89.00m,  DiscountPercent = 5  },
                new Experience { ExperienceId = 5,  CategoryId = 3, Title = "Cabin Retreat",          Description = "Fireplace & hiking.",    Price = 179.00m, DiscountPercent = 0  },
                new Experience { ExperienceId = 6,  CategoryId = 3, Title = "Guided Trail Hike",      Description = "Half-day with guide.",   Price = 59.00m,  DiscountPercent = 0  }
            );
        }
    }
}
