using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repositories.Infrastructure
{
    public class EducationUniversityMap : IEntityTypeConfiguration<EducationEntity>
    {
        public void Configure(EntityTypeBuilder<EducationEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(x => x.University)
                .WithMany(x => x.EducationUniversityList)
                .HasForeignKey(ed=>ed.UniversityId);
        }
    }
}