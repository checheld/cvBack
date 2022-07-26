using Data.Entities;
using Entities.Abstract;

namespace Entities
{
    public class ProjectEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ProjectTypeId { get; set; }
        public virtual ProjectTypeEntity? ProjectType { get; set; }
        public string Country { get; set; }
        public string Link { get; set; }
        public virtual List<ProjectPhotoEntity>? PhotoList { get; set; }
        public virtual List<TechnologyEntity>? TechnologyList { get; set; }
        public virtual List<ProjectTechnologyEntity>? ProjectTechnology { get; set; }
        public virtual List<ProjectCVEntity>? CVProjectCVList { get; set; }

    }
}