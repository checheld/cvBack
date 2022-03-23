using Entities.Abstract;

namespace Entities
{
    public class CompanyEntity : BaseEntity
    {
        public string Name { get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; }
    }
}
