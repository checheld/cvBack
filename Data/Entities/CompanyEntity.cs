using Entities.Abstract;

namespace Entities
{
    public class CompanyEntity : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<WorkExperienceEntity>? WorkExperienceCompanyList { get; set; }
    }
}
