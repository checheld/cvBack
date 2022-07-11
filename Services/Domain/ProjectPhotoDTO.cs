using Domain.Abstract;

namespace Domain
{
    public class ProjectPhotoDTO : BaseDomain
    {
        public int ProjectId { get; set; }
        public virtual ProjectDTO? Project { get; set; }
        public string Url { get; set; }
    }
}