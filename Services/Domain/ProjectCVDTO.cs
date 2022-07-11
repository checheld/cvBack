using Domain.Abstract;

namespace Domain
{
    public class ProjectCVDTO : BaseDomain
    {
        public int ProjectId { get; set; }
        public virtual ProjectDTO? Project { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? CVId { get; set; }
        public virtual CVDTO? CV { get; set; }

    }
}