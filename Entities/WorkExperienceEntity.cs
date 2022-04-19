
using Entities.Abstract;

namespace Entities
{
    public class WorkExperienceEntity : BaseEntity
    {
        public int? CompanyId { get; set; }
        public virtual CompanyEntity? Company { get; set; }
        public string Position { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public virtual UserEntity? User { get; set; }
    }
}