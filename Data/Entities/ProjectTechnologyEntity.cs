
namespace Entities
{
    public class ProjectTechnologyEntity
    {
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
        public int TechnologyId { get; set; }
        public TechnologyEntity Technology { get; set; }
    }

}
