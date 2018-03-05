namespace Core.Items
{
    public abstract class ItemBase
    {
        string itemName; // contains keywords such as BRONZE, SILVER and BLADE, BOOTS connected by "_" to help you sort easier
        int itemCost; // BRONZE items have lowest cost, the most expensive items are LEGENDARY
        int damage; // keyword BLADE is present if the most important item stat is damage
        int health;
        int maxHealth;
        int mana;
        int maxMana;
        int moveSpeed; // keyword BOOTS is present if the most important item stat is moveSpeed
        int manaRegeneration;

        public ItemBase(string itemName, int itemCost, int damage, int health, int maxHealth, int mana, int maxMana, int moveSpeed, int manaRegeneration)
        {
            this.itemName = itemName;
            this.itemCost = itemCost;
            this.damage = damage;
            this.health = health;
            this.maxHealth = maxHealth;
            this.mana = mana;
            this.maxMana = maxMana;
            this.moveSpeed = moveSpeed;
            this.manaRegeneration = manaRegeneration;
        }

        public string ItemName { get => itemName; set => itemName = value; }
        public int ItemCost { get => itemCost; set => itemCost = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Health { get => health; set => health = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Mana { get => mana; set => mana = value; }
        public int MaxMana { get => maxMana; set => maxMana = value; }
        public int MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public int ManaRegeneration { get => manaRegeneration; set => manaRegeneration = value; }

        public override string ToString()
        {
            return $"ItemName: {ItemName}\n" +
                   $"ItemCost: {ItemCost}\n" +
                   $"Damage: {Damage}"
                   /*$"Health: {Health}\n" +
                   $"Mana: {Mana}\n" +
                   $"MaxHealth: {MaxHealth}\n" +
                   $"MaxMana: {MaxMana}\n"*/;
        }
    }
}
