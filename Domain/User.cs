
using Domain.Abstract;

namespace Domain
{
    public class User : BaseDomain
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public List<Education>? EducationList { get; set; }
        public List<WorkExperience>? WorkExperienceList { get; set; }
        public List<Technology>? TechnologyList { get; set; }
    }
}
