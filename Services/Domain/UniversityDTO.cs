
using Domain.Abstract;

namespace Domain
{
    public class UniversityDTO : BaseDomain
    {
        public string Name { get; set; }
        public virtual List<EducationDTO>? EducationUniversityList { get; set; }
    }
}
