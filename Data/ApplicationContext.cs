using Data.Repositories.Infrastructure;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CompanyEntity>? Companies { get; set; }
        public DbSet<UniversityEntity>? Universities { get; set; }
        public DbSet<TechnologyEntity>? Technologies { get; set; }
        public DbSet<ProjectEntity>? Projects { get; set; }
        public DbSet<UserEntity>? Users { get; set; }
        public DbSet<EducationEntity>? Educations { get; set; }
        public DbSet<WorkExperienceEntity>? WorkExperiences { get; set; }
        //
        public DbSet<ProjectTechnology>? ProjectTechnology { get; set; }
        public DbSet<UserTechnologyEntity>? UserTechnology { get; set; }
        //
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TechnologyEntityMap());
            modelBuilder.ApplyConfiguration(new UserTechnologyMap());
            modelBuilder.Entity<EducationEntity>(e =>
            {
                e.HasOne(r => r.University)
                .WithMany(t => t.EducationUniversityList)
                .HasForeignKey(pt => pt.UniversityId);
            });
            modelBuilder.Entity<WorkExperienceEntity>(e =>
            {
                e.HasOne(r => r.Company)
                .WithMany(t => t.WorkExperienceCompanyList)
                .HasForeignKey(pt => pt.CompanyId);
            });
            modelBuilder.Entity<EducationEntity>(e =>
            {
                e.HasOne(r => r.User)
                .WithMany(t => t.EducationList)
                .HasForeignKey(pt => pt.UserId);
            });
            modelBuilder.Entity<WorkExperienceEntity>(e =>
            {
                e.HasOne(r => r.User)
                .WithMany(t => t.WorkExperienceList)
                .HasForeignKey(pt => pt.UserId);
            });
        }
        
    }
}
