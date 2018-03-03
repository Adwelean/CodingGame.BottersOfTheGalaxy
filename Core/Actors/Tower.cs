namespace Core.Actors
{
    public class Tower : Entity
    {
        public Tower(
            double x,
            double y,
            int id,
            int attackRange,
            int health,
            int maxHealth,
            int shield,
            int attackDamage,
            int movementSpeed,
            int stunDuration,
            int goldValue
        ) : base(
            x,
            y,
            id,
            attackRange,
            health,
            maxHealth,
            shield,
            attackDamage,
            movementSpeed,
            stunDuration,
            goldValue)
        {
        }
    }
}
