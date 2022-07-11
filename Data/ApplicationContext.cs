#region Imports
using Data.Entities;
using Data.Repositories.Infrastructure;
using Entities;
using Microsoft.EntityFrameworkCore;
#endregion

namespace Data
{
    public class ApplicationContext : DbContext
    {
        #region db
        public DbSet<CompanyEntity>? Companies { get; set; }
        public DbSet<UniversityEntity>? Universities { get; set; }
        public DbSet<TechnologyEntity>? Technologies { get; set; }
        public DbSet<ProjectEntity>? Projects { get; set; }
        public DbSet<UserEntity>? Users { get; set; }
        public DbSet<EducationEntity>? Educations { get; set; }
        public DbSet<WorkExperienceEntity>? WorkExperiences { get; set; }
        public DbSet<CVEntity>? CVs { get; set; }
        public DbSet<ProjectCVEntity>? ProjectCVs { get; set; }
        public DbSet<ProjectPhotoEntity>? ProjectPhotoEntity { get; set; }
        public DbSet<ProjectTechnologyEntity>? ProjectTechnology { get; set; }
        public DbSet<UserTechnologyEntity>? UserTechnology { get; set; }
        public DbSet<PhotoParamsEntity>? PhotoParams { get; set; }
        #endregion
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region config
            modelBuilder.ApplyConfiguration(new TechnologyEntityMap());
            modelBuilder.ApplyConfiguration(new UserTechnologyMap());
            modelBuilder.ApplyConfiguration(new EducationUniversityMap());
            modelBuilder.ApplyConfiguration(new WorkExperienceCompanyMap());
            modelBuilder.ApplyConfiguration(new UserEducationMap());
            modelBuilder.ApplyConfiguration(new UserWorkExperienceMap());
            modelBuilder.ApplyConfiguration(new ProjectProjectCVMap());
            modelBuilder.ApplyConfiguration(new CVProjectCVMap());
            modelBuilder.ApplyConfiguration(new ProjectProjectPhotoMap());
            #endregion
        }

    }
}
