
using Entities.Abstract;

namespace Entities
{
    public class EducationEntity : BaseEntity
    {
        public int? UniversityId { get; set; }
        public virtual UniversityEntity? University { get; set; }
        public string Speciality { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? UserId { get; set; }
        public virtual UserEntity? User { get; set; }
    }
}