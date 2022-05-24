using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repositories.Infrastructure
{
    public class UserWorkExperienceMap : IEntityTypeConfiguration<WorkExperienceEntity>
    {
        public void Configure(EntityTypeBuilder<WorkExperienceEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(x => x.User)
                .WithMany(x => x.WorkExperienceList)
                .HasForeignKey(ed => ed.UserId);
        }
    }
}