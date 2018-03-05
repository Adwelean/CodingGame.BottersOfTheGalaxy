namespace Core.Actors
{
    using Core.AI;
    using Core.Items;
    using System.Collections.Generic;
    using System.Linq;

    public enum HeroType
    {
        DEADPOOL,
        VALKYRIE,
        DOCTOR_STRANGE,
        HULK,
        IRONMAN,
    }

    public partial class Hero : Entity
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
            goldValue,
            itemsOwned)
        {
            this.heroType = heroType;
            this.countDown1 = countDown1;
            this.countDown2 = countDown2;
            this.countDown3 = countDown3;
            this.mana = mana;
            this.maxMana = maxMana;
            this.manaRegeneration = manaRegeneration;
            this.isVisible = isVisible;
        }

        public HeroType HeroType { get => heroType; set => heroType = value; }
        public int CountDown1 { get => countDown1; set => countDown1 = value; }
        public int CountDown2 { get => countDown2; set => countDown2 = value; }
        public int CountDown3 { get => countDown3; set => countDown3 = value; }
        public int Mana { get => mana + Equipment.Sum(x => x.Mana); set => mana = (value > MaxMana) ? MaxMana : value; }
        public int MaxMana { get => maxMana + Equipment.Sum(x => x.MaxMana); set => maxMana = value; }
        public int ManaRegeneration { get => manaRegeneration + Equipment.Sum(x => x.ManaRegeneration); set => manaRegeneration = value; }
        public int IsVisible { get => isVisible; set => isVisible = value; }  
        public IArtificialIntelligence AI { get; set; }
    }
}
