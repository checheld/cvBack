using Entities.Abstract;

namespace Entities
{
    public class UniversityEntity : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<EducationEntity>? EducationUniversityList { get; set; }
    }
}
