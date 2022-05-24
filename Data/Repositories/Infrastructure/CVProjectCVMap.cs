using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repositories.Infrastructure
{
    public class CVProjectCVMap : IEntityTypeConfiguration<ProjectCVEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectCVEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(x => x.CV)
                .WithMany(x => x.ProjectCVList)
                .HasForeignKey(ed => ed.CVId);
        }
    }
}