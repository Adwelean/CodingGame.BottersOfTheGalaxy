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
        int attackRange;
        int health;
        int maxHealth;
        int shield; // useful in bronze
        int attackDamage;
        int movementSpeed;
        int stunDuration; // useful in bronze
        int goldValue;
        

        public Entity(
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
            int goldValue) : base(x, y)
        {
            this.id = id;
            this.attackRange = attackRange;
            this.health = health;
            this.maxHealth = maxHealth;
            this.shield = shield;
            this.attackDamage = attackDamage;
            this.movementSpeed = movementSpeed;
            this.stunDuration = stunDuration;
            this.goldValue = goldValue;
        }

        public int Id { get => id; set => id = value; }
        public int AttackRange { get => attackRange; set => attackRange = value; }
        public int Health { get => health; set => health = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Shield { get => shield; set => shield = value; }
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }
        public int MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public int StunDuration { get => stunDuration; set => stunDuration = value; }
        public int GoldValue { get => goldValue; set => goldValue = value; }

        public bool IsAlive => Health > 0;
    }
}
