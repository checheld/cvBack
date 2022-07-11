using Domain.Abstract;

namespace Domain
{
    public class EducationDTO : BaseDomain
    {
        public int UniversityId { get; set; }
        public virtual UniversityDTO? University { get; set; }
        public string Speciality { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? UserId { get; set; }
        public virtual UserDTO? User { get; set; }
    }
}