using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Items
{
    public class Consumable : ItemBase
    {
        public Consumable(string itemName, int itemCost, int damage, int health, int maxHealth, int mana, int maxMana, int moveSpeed, int manaRegeneration) 
            : base(itemName, itemCost, damage, health, maxHealth, mana, maxMana, moveSpeed, manaRegeneration)
        {
        }
    }
}
