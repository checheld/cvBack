
using Domain.Abstract;

namespace Domain
{
    public class University : BaseDomain
    {
        public string Name { get; set; }
        public virtual List<Education>? EducationUniversityList { get; set; }
    }
}
