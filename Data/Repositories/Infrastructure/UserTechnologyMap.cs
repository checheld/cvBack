using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Repositories.Infrastructure
{
    public class UserTechnologyMap : IEntityTypeConfiguration<TechnologyEntity>
    {
        public void Configure(EntityTypeBuilder<TechnologyEntity> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.HasMany(x => x.UserList).WithMany(x => x.TechnologyList).UsingEntity<UserTechnologyEntity>
                (
                    j => j
                        .HasOne(pt => pt.User)
                        .WithMany(t => t.UserTechnology)
                        .HasForeignKey(pt => pt.UserId),
                    j => j
                        .HasOne(pt => pt.Technology)
                        .WithMany(p => p.UserTechnologies)
                        .HasForeignKey(pt => pt.TechnologyId),
                    j =>
                    {
                        j.HasKey(t => new { t.UserId, t.TechnologyId });

                    }
                );
        }
    }
}