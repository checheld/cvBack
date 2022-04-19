using Entities.Abstract;

namespace Entities
{
    public class TechnologyEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? Type { get; set; }

        public virtual List<ProjectEntity>? ProjectList { get; set; }
        public virtual List<ProjectTechnology>? ProjectTechnologies { get; set; }
        public virtual List<UserEntity>? UserList { get; set; }
        public virtual List<UserTechnologyEntity>? UserTechnologies { get; set; }
    }
}