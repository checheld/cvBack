using Domain.Abstract;

namespace Domain
{
    public class TechnologyDTO : BaseDomain
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public virtual List<ProjectDTO>? ProjectList { get; set; }
        public virtual List<UserDTO>? UserList { get; set; }
    }
}