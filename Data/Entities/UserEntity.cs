using Entities.Abstract;

namespace Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string? photoUrl { get; set; }
        public List<EducationEntity>? EducationList { get; set; }
        public List<WorkExperienceEntity>? WorkExperienceList { get; set; }
        public virtual List<TechnologyEntity>? TechnologyList { get; set; }
        public virtual List<UserTechnologyEntity>? UserTechnology { get; set; }
    }
}