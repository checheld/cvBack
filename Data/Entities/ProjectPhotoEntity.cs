using Entities;
using Entities.Abstract;

namespace Data.Entities
{
    public class ProjectPhotoEntity : BaseEntity
    {
        public int ProjectId { get; set; }
        public virtual ProjectEntity? Project { get; set; }
        public string Url { get; set; }
    }
}
