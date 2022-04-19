using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repositories.Infrastructure
{
    public class TechnologyEntityMap : IEntityTypeConfiguration<TechnologyEntity>
    {
        public void Configure(EntityTypeBuilder<TechnologyEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.HasMany(x => x.ProjectList).WithMany(x => x.TechnologyList).UsingEntity<ProjectTechnology>
                (
                j => j
                    .HasOne(pt => pt.Project)
                    .WithMany(t => t.ProjectTechnology)
                    .HasForeignKey(pt => pt.ProjectId),
                j => j
                    .HasOne(pt => pt.Technology)
                    .WithMany(p => p.ProjectTechnologies)
                    .HasForeignKey(pt => pt.TechnologyId),
                j =>
                {
                    j.HasKey(t => new { t.ProjectId, t.TechnologyId });
                  
                }
                );
        }
    }
}
