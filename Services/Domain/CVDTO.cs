using Domain.Abstract;

namespace Domain
{
    public class CVDTO : BaseDomain
    {
        public string CVName { get; set; }
        public int UserId { get; set; }
        public UserDTO? User { get; set; }
        public List<ProjectCVDTO> ProjectCVList { get; set; }
    }
}