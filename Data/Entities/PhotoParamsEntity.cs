using Entities.Abstract;

namespace Data.Entities
{
    public class PhotoParamsEntity : BaseEntity
    {
        public double Scale { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
    }
}