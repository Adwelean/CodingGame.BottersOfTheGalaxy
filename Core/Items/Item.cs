namespace Core.Items
{
    public class Item : ItemBase
    {
        public Item(string itemName, int itemCost, int damage, int health, int maxHealth, int mana, int maxMana, int moveSpeed, int manaRegeneration) 
            : base(itemName, itemCost, damage, health, maxHealth, mana, maxMana, moveSpeed, manaRegeneration)
        {
        }
    }
}
