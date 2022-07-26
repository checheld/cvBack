using Data.Entities;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repositories.Infrastructure
{
    public class ProjectProjectTypeMap : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> entityTypeBuilder)
        {
            {
                entityTypeBuilder
                    .HasOne(x => x.ProjectType)
                    .WithMany(x => x.ProjectProjectTypeList)
                    .HasForeignKey(ed => ed.ProjectTypeId);
            }
        }
    }
}