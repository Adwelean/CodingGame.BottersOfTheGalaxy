namespace Core.Actors
{
    public enum EntityType
    {
        UNIT,
        HERO,
        TOWER,
        GROOT,
    }

    public class Entity : EntityBase
    {
        int id;

        /// <summary>
        /// For unboxing
        /// </summary>
        public Entity()
            : base(0, 0)
        {

        }

        public Entity(double x, double y, int id) 
            : base(x, y)
        {
            this.id = id;
        }
    }
}
