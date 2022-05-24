using Entities.Abstract;

namespace Data.Entities
{
    public class ProfilePhotoEntity : BaseEntity
    {
        public int UserId { get; set; }
        public string Url { get; set; }
    }
}