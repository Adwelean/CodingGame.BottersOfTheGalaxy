using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Items
{
    public class ItemBase
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

        public string ItemName { get => this.itemName; }
        public int ItemCost { get => this.itemCost; set => this.itemCost = value; }
    }
}
