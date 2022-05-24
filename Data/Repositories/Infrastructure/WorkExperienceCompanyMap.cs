using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repositories.Infrastructure
{
    public class WorkExperienceCompanyMap : IEntityTypeConfiguration<WorkExperienceEntity>
    {
        public void Configure(EntityTypeBuilder<WorkExperienceEntity> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(x => x.Company)
                .WithMany(x => x.WorkExperienceCompanyList)
                .HasForeignKey(ed => ed.CompanyId);
        }
    }
}