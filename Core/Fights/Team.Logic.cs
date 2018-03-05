using Core.Actors;
using Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Fights
{
    public partial class Team
    {
        public void UpdateEntities()
        {
            //Entities.Clear();
            if (Entities.Count > 0)
            {
                LastEntities = new List<Entity>(Entities);
                Entities.Clear();
            }
        }

        public bool CanBuyItem(ItemBase item, Hero hero) => item.ItemCost <= Gold && hero.ItemsOwned < GameSettings.MaxItemCount;

        public void Buy(ItemBase item, Hero hero)
        {

            if (CanBuyItem(item, hero))
            {
                if (item is Consumable)
                {
                    hero.Health += item.Health;
                    hero.Mana += item.Mana;
                    hero.AttackDamage += item.Damage;
                    hero.MovementSpeed += item.MoveSpeed;
                }
                else
                    Entities.FirstOrDefault(x => x.Id == hero.Id).Equipment.Add(item as Item);

                Gold -= item.ItemCost;
            }
        }

        public void Sell(ItemBase item, Hero hero)
        {
            hero.Health -= item.Health;
            hero.Mana -= item.Mana;
            hero.AttackDamage -= item.Damage;
            hero.MovementSpeed -= item.MoveSpeed;
            hero.ManaRegeneration -= item.ManaRegeneration;
            hero.MaxHealth -= item.MaxHealth;
            hero.MaxMana -= item.MaxMana;

            Gold += item.ItemCost / 2;
        }
    }
}
