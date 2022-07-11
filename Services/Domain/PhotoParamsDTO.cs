using Domain.Abstract;

namespace Domain
{
    public class PhotoParamsDTO : BaseDomain
    {
        public double Scale { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
    }
}