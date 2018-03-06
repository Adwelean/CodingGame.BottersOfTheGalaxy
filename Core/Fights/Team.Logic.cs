namespace Core.Fights
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Actors;
    using Core.Items;
    using Core.Extensions;
    using System;
    using Core.AI.Commands;

    public partial class Team
    {
        public void UpdateEntities(List<Entity> newEntities)
        {
            if (Entities.Count > 0)
                LastEntities = Entities.ConvertAll(x => (Entity)x.Clone());

            Entities.Except(newEntities, x => x.Id).ToList().ForEach(x => Entities.Remove(x));
        }

        public bool CanBuyItem(ItemBase item, Hero hero) => item.ItemCost <= Gold && hero.ItemsOwned < GameSettings.MaxItemCount;

        public CommandBase Buy(ItemBase item, Hero hero)
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

            return new AI.Commands.Raw($"BUY {item.ItemName}");
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
