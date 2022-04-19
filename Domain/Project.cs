using Domain.Abstract;

namespace Domain
{
    public class Project : BaseDomain
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string Link { get; set; }
        public virtual List<Technology>? TechnologyList { get; set; }
    }
}