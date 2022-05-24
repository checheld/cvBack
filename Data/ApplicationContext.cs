using Data.Entities;
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
        public DbSet<CVEntity>? CVs { get; set; }
        public DbSet<ProjectCVEntity>? ProjectCVs { get; set; }
        public DbSet<ProjectTechnology>? ProjectTechnology { get; set; }
        public DbSet<UserTechnologyEntity>? UserTechnology { get; set; }
        public DbSet<ProfilePhotoEntity>? ProfilePhoto { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TechnologyEntityMap());
            modelBuilder.ApplyConfiguration(new UserTechnologyMap());
            modelBuilder.ApplyConfiguration(new EducationUniversityMap());
            modelBuilder.ApplyConfiguration(new WorkExperienceCompanyMap());
            modelBuilder.ApplyConfiguration(new UserEducationMap());
            modelBuilder.ApplyConfiguration(new UserWorkExperienceMap());
            modelBuilder.ApplyConfiguration(new ProjectProjectCVMap());
            modelBuilder.ApplyConfiguration(new CVProjectCVMap());  
        }
        
    }
}
