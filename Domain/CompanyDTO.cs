using Domain.Abstract;

namespace Domain
{
    public class CompanyDTO : BaseDomain
    {
        public string Name { get; set; }
        public virtual List<WorkExperienceDTO>? WorkExperienceCompanyList { get; set; }
    }
}
