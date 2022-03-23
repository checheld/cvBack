using Entities.Abstract;

namespace Entities
{
    public class TechnologyEntity : BaseEntity
    {
        public string Name { get; set; }
        public string? Type { get; set; }
    }
}