
namespace Entities
{
    public class ProjectTechnology
    {
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
        public int TechnologyId { get; set; }
        public TechnologyEntity Technology { get; set; }
    }

}
