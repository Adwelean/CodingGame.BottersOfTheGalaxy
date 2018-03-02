namespace Core.Actors
{
    using Core.Utils;

    public abstract class EntityBase : Point
    {
        public EntityBase(double x, double y)
            : base(x, y)
        {
        }
    }
}
