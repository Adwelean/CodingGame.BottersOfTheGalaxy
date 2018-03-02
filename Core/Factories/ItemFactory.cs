using Core.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Factories
{
    public static class ItemFactory
    {
        public static ItemBase ParseItem(string[] inputs)
        {
            ItemBase itemBase;

            string itemName = inputs[0]; // contains keywords such as BRONZE, SILVER and BLADE, BOOTS connected by "_" to help you sort easier
            int itemCost = int.Parse(inputs[1]); // BRONZE items have lowest cost, the most expensive items are LEGENDARY
            int damage = int.Parse(inputs[2]); // keyword BLADE is present if the most important item stat is damage
            int health = int.Parse(inputs[3]);
            int maxHealth = int.Parse(inputs[4]);
            int mana = int.Parse(inputs[5]);
            int maxMana = int.Parse(inputs[6]);
            int moveSpeed = int.Parse(inputs[7]); // keyword BOOTS is present if the most important item stat is moveSpeed
            int manaRegeneration = int.Parse(inputs[8]);
            int isPotion = int.Parse(inputs[9]); // 0 if it's not instantly consumed

            if (isPotion == 0)
                itemBase = new Item(itemName, itemCost, damage, health, maxHealth, mana, maxMana, moveSpeed, manaRegeneration);
            else
                itemBase = new Consumable(itemName, itemCost, damage, health, maxHealth, mana, maxMana, moveSpeed, manaRegeneration);

            return itemBase;
        }
    }
}
