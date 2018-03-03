namespace Core.Actors
{
    using Core.AI;

    public enum HeroType
    {
        DEADPOOL,
        VALKYRIE,
        DOCTOR_STRANGE,
        HULK,
        IRONMAN,
    }

    public class Hero : Entity
    {
        HeroType heroType;
        int countDown1; // all countDown and mana variables are useful starting in bronze
        int countDown2;
        int countDown3;
        int mana;
        int maxMana;
        int manaRegeneration;
        int isVisible; // 0 if it isn't
        int itemsOwned; // useful from wood1

        public Hero(
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
            int goldValue,
            int countDown1,
            int countDown2,
            int countDown3,
            int mana,
            int maxMana,
            int manaRegeneration,
            int isVisible,
            int itemsOwned,
            HeroType heroType
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
            goldValue
            )
        {
            this.heroType = heroType;
            this.countDown1 = countDown1;
            this.countDown2 = countDown2;
            this.countDown3 = countDown3;
            this.mana = mana;
            this.maxMana = maxMana;
            this.manaRegeneration = manaRegeneration;
            this.isVisible = isVisible;
            this.itemsOwned = itemsOwned;
        }

        public HeroType HeroType { get => heroType; set => heroType = value; }
        public int CountDown1 { get => countDown1; set => countDown1 = value; }
        public int CountDown2 { get => countDown2; set => countDown2 = value; }
        public int CountDown3 { get => countDown3; set => countDown3 = value; }
        public int Mana { get => mana; set => mana = value; }
        public int MaxMana { get => maxMana; set => maxMana = value; }
        public int ManaRegeneration { get => manaRegeneration; set => manaRegeneration = value; }
        public int IsVisible { get => isVisible; set => isVisible = value; }
        public int ItemsOwned { get => itemsOwned; set => itemsOwned = value; }

        public IArtificialIntelligence AI { get; set; }
    }
}
