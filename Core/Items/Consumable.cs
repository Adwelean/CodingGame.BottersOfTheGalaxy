namespace Core.Items
{
    public class Consumable : ItemBase
    {
        public Consumable(string itemName, int itemCost, int health, int mana, int moveSpeed, int manaRegeneration) 
            : base(itemName, itemCost, 0, health, 0, mana, 0, moveSpeed, manaRegeneration)
        {
        }
    }
}
