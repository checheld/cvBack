using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CompanyEntity>? Companies { get; set; }
        public DbSet<UniversityEntity>? Universities { get; set; }
        public DbSet<TechnologyEntity>? Technologies { get; set; }
        public DbSet<User>? Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            Guid MainAdmin = Guid.NewGuid();
            var TheOnlyUser = new User
            {
                Id = 100000,
                CreatedAt = DateTime.Now,
                Name = "User1"
            };
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                TheOnlyUser
            });
            modelBuilder.Entity<CompanyEntity>().HasData(new List<CompanyEntity>
            {
                new CompanyEntity {
                    Id = 10,
                    Name = "1111111",
                    UserId = TheOnlyUser.Id
                },
                new CompanyEntity {
                    Id = 20,
                    Name = "22222222",
                    UserId = TheOnlyUser.Id
                },
                new CompanyEntity {
                    Id = 30,
                    Name = "33333333",
                    UserId = TheOnlyUser.Id

                }
            });
        }
    }
}
