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
/*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("Connection is opened");
            optionsBuilder.UseSqlServer(@"Server=(localdb)\\MSSQLLocalDB;Database=LeviossaCV;Trusted_Connection=True;");
        }*/


        // проверить необходимость кода ниже

     /*   protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyEntity>().HasData(new CompanyEntity[]
            {
          *//*      new CompanyEntity
                {
                    Id = 1,
                    Name = "qwerty"
                }
            });

            modelBuilder.Entity<UniversityEntity>().HasData(new UniversityEntity[]
           {
                new UniversityEntity
                {
                    Id = 2,
                    Name = "asdfg"
                }*//*
           });
        }*/
    }
}
