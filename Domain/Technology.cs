using Domain.Abstract;

namespace Domain
{
    public class Technology : BaseDomain
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public virtual List<Project>? ProjectList { get; set; }
        public virtual List<User>? UserList { get; set; }
    }
}