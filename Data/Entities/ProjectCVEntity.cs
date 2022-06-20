using Entities.Abstract;

namespace Entities
{
    public class ProjectCVEntity : BaseEntity
    {
        public int ProjectId { get; set; }
        public virtual ProjectEntity? Project { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? CVId { get; set; }
        public virtual CVEntity? CV { get; set; }
    }
}