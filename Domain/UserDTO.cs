
using Domain.Abstract;

namespace Domain
{
    public class UserDTO : BaseDomain
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string photoUrl { get; set; }
        public string? pdfUrl { get; set; }
        public List<EducationDTO>? EducationList { get; set; }
        public List<WorkExperienceDTO>? WorkExperienceList { get; set; }
        public List<TechnologyDTO>? TechnologyList { get; set; }
    }
}
