namespace LeviossaCV.Services.Models
{
    public class EducationList
    {
        public int UniversityId { get; set; }
        public string Speciality { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int UserId { get; set; }
    }

    public class UpdateUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public List<EducationList> EducationList { get; set; }
        public List<object> WorkExperienceList { get; set; }
        public List<object> TechnologyList { get; set; }
        public int UserId { get; set; }
    }
}
