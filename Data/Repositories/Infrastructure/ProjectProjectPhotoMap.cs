using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repositories.Infrastructure
{
    public class ProjectProjectPhotoMap : IEntityTypeConfiguration<ProjectPhotoEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectPhotoEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(x => x.Project)
                .WithMany(x => x.PhotoList)
                .HasForeignKey(ed => ed.ProjectId);
        }
    }
}