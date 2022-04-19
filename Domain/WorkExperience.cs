
using Domain.Abstract;

namespace Domain
{
    public class WorkExperience : BaseDomain
    {
        public int? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        public string Position { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}