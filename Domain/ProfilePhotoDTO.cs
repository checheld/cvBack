using Domain.Abstract;

namespace Domain
{
    public class ProfilePhotoDTO : BaseDomain
    {
        public int UserId { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }  
    }
}