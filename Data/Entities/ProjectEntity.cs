using Entities.Abstract;

namespace Entities
{
    public class ProjectEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string Link { get; set; }
        public virtual List<TechnologyEntity>? TechnologyList { get; set; }
        public virtual List<ProjectTechnology>? ProjectTechnology { get; set; }
        public virtual List<ProjectCVEntity>? CVProjectCVList { get; set; }

    }
}