using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repositories.Infrastructure
{
    public class ProjectProjectCVMap : IEntityTypeConfiguration<ProjectCVEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectCVEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(x => x.Project)
                .WithMany(x => x.CVProjectCVList)
                .HasForeignKey(ed => ed.ProjectId);
        }
    }
}