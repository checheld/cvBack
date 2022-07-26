using Domain;
using Domain.Abstract;

namespace Services.Domain
{
    public class ProjectTypeDTO : BaseDomain
    {
        public string Name { get; set; }
        public virtual List<ProjectDTO>? ProjectProjectTypeList { get; set; }
    }
}