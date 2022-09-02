using Domain;
using Services.Domain;

namespace LeviossaCV.UI
{
    public class ProjectUI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectTypeId { get; set; }
        public virtual ProjectTypeDTO? ProjectType { get; set; }
        public string Country { get; set; }
        public string Link { get; set; }
        public virtual List<ProjectPhotoDTO>? PhotoList { get; set; }
        public virtual List<TechnologyDTO>? TechnologyList { get; set; }
       
    }
}