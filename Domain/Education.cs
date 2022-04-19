using Domain.Abstract;

namespace Domain
{
    public class Education : BaseDomain
    {
        public int? UniversityId { get; set; }
        public virtual University? University { get; set; }
        public string Speciality { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}