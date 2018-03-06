namespace Core.Actors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Items;

    public enum EntityType
    {
        UNIT,
        HERO,
        TOWER,
        GROOT,
    }

    public class Entity : EntityBase, ICloneable
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
        int itemsOwned;
        List<Item> equipment;

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
            int goldValue,
            int itemsOwned) : base(x, y)
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
            this.itemsOwned = itemsOwned;
            this.equipment = new List<Item>();
        }


        public void Update(Entity entity)
        {
            base.X = entity.X;
            base.Y = entity.Y;
            this.id = entity.Id;
            this.attackRange = entity.AttackRange;
            this.health = entity.Health;
            this.maxHealth = entity.MaxHealth;
            this.shield = entity.Shield;
            this.attackDamage = entity.AttackDamage;
            this.movementSpeed = entity.MovementSpeed;
            this.stunDuration = entity.StunDuration;
            this.goldValue = entity.GoldValue;
            this.itemsOwned = entity.itemsOwned;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int Id { get => id; set => id = value; }
        public int AttackRange { get => attackRange; set => attackRange = value; }
        public int Health { get => health + Equipment.Sum(x => x.Health); set => health = (value > MaxHealth) ? MaxHealth : value; }
        public int MaxHealth { get => maxHealth + Equipment.Sum(x => x.MaxHealth); set => maxHealth = value; }
        public int Shield { get => shield; set => shield = value; }
        public int AttackDamage { get => attackDamage + Equipment.Sum(x => x.Damage); set => attackDamage = value; }
        public int MovementSpeed { get => movementSpeed + Equipment.Sum(x => x.MoveSpeed); set => movementSpeed = value; }
        public int StunDuration { get => stunDuration; set => stunDuration = value; }
        public int GoldValue { get => goldValue; set => goldValue = value; }
        public int ItemsOwned { get => itemsOwned; set => itemsOwned = value; }
        public List<Item> Equipment { get => equipment; set => equipment = value; }

        public bool IsAlive => Health > 0;
    }
}
