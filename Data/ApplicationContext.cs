using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CompanyEntity>? Companies { get; set; }
        public DbSet<UniversityEntity>? Universities { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {
        }

    }
}
