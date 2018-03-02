namespace Core.Interactives
{
    using Core.Actors;

    public class Bush : EntityBase
    {
        int radius;

        public Bush(double x, double y, int radius) 
            : base(x, y)
        {
            this.radius = radius;
        }
    }
}
