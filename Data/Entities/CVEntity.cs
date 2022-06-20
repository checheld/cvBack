using Entities.Abstract;

namespace Entities
{
    public class CVEntity : BaseEntity
    {
        public string CVName { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public List<ProjectCVEntity> ProjectCVList { get; set; }
    }
}