namespace Entities
{
    public class UserTechnologyEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int TechnologyId { get; set; }
        public TechnologyEntity Technology { get; set; }
    }
}