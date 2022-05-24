using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repositories.Infrastructure
{
    public class UserEducationMap : IEntityTypeConfiguration<EducationEntity>
    {
        public void Configure(EntityTypeBuilder<EducationEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(x => x.User)
                .WithMany(x => x.EducationList)
                .HasForeignKey(ed => ed.UserId);
        }
    }
}