using Entities;
using Entities.Abstract;

namespace Data.Entities
{
    public class ProjectTypeEntity : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<ProjectEntity>? ProjectProjectTypeList { get; set; }
    }
}
