using Domain.Abstract;

namespace Domain
{
    public class PhotoForReturnDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
    }
}