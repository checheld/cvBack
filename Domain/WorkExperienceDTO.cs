
using Domain.Abstract;

namespace Domain
{
    public class WorkExperienceDTO : BaseDomain
    {
        public int? CompanyId { get; set; }
        public virtual CompanyDTO? Company { get; set; }
        public string Position { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public virtual UserDTO? User { get; set; }
    }
}