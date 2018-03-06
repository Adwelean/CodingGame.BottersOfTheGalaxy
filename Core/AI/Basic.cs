namespace Core.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Actors;
    using Core.AI.Commands;
    using Core.Fights;
    using Core.Items;
    using Core.Utils;

    public class Basic : IArtificialIntelligence
    {
        public enum State
        {
            Protect,
            Attack,
            TowerAssault
        }

        GameContext context;

        Team PlayerTeam => context.PlayerTeam;
        Team EnemyTeam => context.EnemyTeam;
        Hero PlayerHero => context.PlayerHero;
        CommandBase LastCommand => context.LastCommand;
        List<ItemBase> Items => context.Items;

        int FactorDirection => (PlayerTeam.Id > 0) ? -1 : 1;

        Queue<Item> pendingItemsToSell = new Queue<Item>();

        public CommandBase ComputeAction(GameContext context)
        {
            this.context = context;

            CommandBase action = new Commands.Wait();

            var playerHeroes = PlayerTeam.Entities.Where(x => x is Hero).Cast<Hero>().ToList();
            var playerTower = PlayerTeam.Entities.FirstOrDefault(x => x is Tower) as Tower;

            var enemyHeroes = EnemyTeam.Entities.Where(x => x is Hero).Cast<Hero>().ToList();
            var enemyTower = EnemyTeam.Entities.FirstOrDefault(x => x is Tower) as Tower;

            var playerState = ComputeState(playerTower, PlayerHero, enemyTower, enemyHeroes);

            switch (playerState)
            {
                case State.Attack:
                    action = AttackStrategy(playerTower, enemyTower);
                    break;

                case State.TowerAssault:
                    action = TowerAssaultStrategy(enemyTower);
                    break;

                default:
                    action = ProtectStrategy(playerTower);
                    break;
            }

            return action;
        }

        public State ComputeState(Tower playerTower, Hero playerHero, Tower enemyTower, List<Hero> enemyHeroes)
        {
            if (PlayerTeam.Entities.Count(x => x is Creep) < EnemyTeam.Entities.Count(x => x is Creep) &&
               enemyHeroes.Any(x => playerTower.Distance(x) < GameSettings.MapWidth / 3))
            {
                return State.Protect;
            }
            else if (//PlayerTeam.Entities.Count(x => x is Creep) > EnemyTeam.Entities.Count(x => x is Creep) &&
                     playerHero.Distance(enemyTower) < GameSettings.MapWidth / 4) // TODO: change by playerHero.IsInRange(enemyTower, enemyTower.AttackRange) ?
            {
                return State.TowerAssault;
            }
            else
                return State.Attack;
        }

        public CommandBase ProtectStrategy(Tower playerTower)
        {
            var creepEnemyWithLowHealth = EnemyCreepWithLowHealth;

            if (PlayerHero.Health < PlayerHero.MaxHealth / 2)
            {
                var potion = GetPotion();

                if (potion != null)
                    return PlayerTeam.Buy(potion, PlayerHero);
            }

            // Last Hit
            if (HasEnemyCreepWithLowHealth &&
                creepEnemyWithLowHealth != null &&
                creepEnemyWithLowHealth.IsInRange(PlayerHero, PlayerHero.AttackRange) &&
                creepEnemyWithLowHealth.Health <= PlayerHero.AttackDamage)
            {
                return new Commands.Raw($"ATTACK {creepEnemyWithLowHealth.Id}");
            }

            return new Commands.Move(playerTower.X, playerTower.Y);
        }

        public CommandBase AttackStrategy(Tower playerTower, Tower enemyTower)
        {
            var creepShield = FindPlayerShieldCreep(enemyTower);
            var creepEnemyWithLowHealth = EnemyCreepWithLowHealth;
            var hasEnemyCreepWithLowHealth = HasEnemyCreepWithLowHealth;

            // Back
            if (PlayerTeam.Entities.Where(x => x is Creep).All(x => x.Distance(enemyTower) > PlayerHero.Distance(enemyTower)))
                return new Commands.Move(playerTower.X, playerTower.Y);

            // Heal
            if (PlayerHero.Health < PlayerHero.MaxHealth / 2)
            {
                var potion = Items.Where(x => x is Consumable && x.Health > 0)
                                  .OrderByDescending(x => x.Health)
                                  .FirstOrDefault(x => (x.Health <= PlayerHero.MaxHealth - PlayerHero.Health) &&
                                                       PlayerTeam.CanBuyItem(x, PlayerHero));

                if (potion != null)
                    return new Commands.Raw($"BUY {potion.ItemName}");
                /*else // back
                    return new Commands.Move(PlayerHero.X + (PlayerHero.MovementSpeed * FactorDirection), PlayerHero.Y);*/
            }
            else
            {
                /*if (pendingItemsToSell.Count > 0)
                    return new Commands.Raw($"SELL {pendingItemsToSell.Dequeue().ItemName}");*/

                // Buy blade but reserve 1 slot for potion
                if (PlayerHero.ItemsOwned < GameSettings.MaxItemCount - 1)
                {
                    var damageItemsOrdered = Items.Where(x => x is Item && x.Damage > 0).OrderByDescending(x => x.Damage);

                    var blade = damageItemsOrdered.FirstOrDefault(x => x.ItemName.Contains("Blade") && PlayerTeam.CanBuyItem(x, PlayerHero));

                    if (blade != null)
                    {
                        PlayerTeam.Buy(blade, PlayerHero);
                        return new Commands.Raw($"BUY {blade.ItemName}");
                    }
                    else
                        damageItemsOrdered.FirstOrDefault(x => PlayerTeam.CanBuyItem(x, PlayerHero));
                }
                else
                {
                    /*var groupedItems = PlayerHero.Equipment.GroupBy(x => x.ItemName);
                    var duplicateItems = groupedItems.SelectMany(x => x.Where(y => y.ItemName == x.Max(z => z.ItemName)));

                    if (duplicateItems != null && duplicateItems.Count() > 1)
                    {
                        duplicateItems.ToList().ForEach(item => pendingItemsToSell.Enqueue(item));
                        return new Commands.Raw($"SELL {pendingItemsToSell.Dequeue().ItemName}");
                    }*/
                }
            }

            // Need a cover
            if (LastCommand != null &&
                LastCommand.Build().Contains("ATTACK") &&
                !(hasEnemyCreepWithLowHealth &&
                creepEnemyWithLowHealth != null &&
                creepEnemyWithLowHealth.IsInRange(PlayerHero, PlayerHero.AttackRange) &&
                creepEnemyWithLowHealth.Health <= PlayerHero.AttackDamage))
            {
                if (creepShield != null)
                    return new Commands.Move(creepShield.X, creepShield.Y);
                else
                    return new Commands.Move(playerTower.X, playerTower.Y);
            }

            // Safe
            if (!HasEnemyInRange(PlayerHero) &&
                !HasEnemyInRange(new Point(PlayerHero.X + (PlayerHero.MovementSpeed * FactorDirection), PlayerHero.Y), PlayerHero.AttackRange))
                return new Commands.Move(creepShield.X + ((creepShield.AttackRange / 2) * FactorDirection), creepShield.Y);

            // Last Hit
            if (hasEnemyCreepWithLowHealth &&
                creepEnemyWithLowHealth != null &&
                creepEnemyWithLowHealth.IsInRange(PlayerHero, PlayerHero.AttackRange) &&
                creepEnemyWithLowHealth.Health <= PlayerHero.AttackDamage)
            {
                return new Commands.Raw($"ATTACK {creepEnemyWithLowHealth.Id}");
            }
            // TODO: can be move to the creep and attack ? check ratio < 1
            /*else if (hasEnemyCreepWithLowHealth &&
                     creepEnemyWithLowHealth != null &&
                     creepEnemyWithLowHealth.Health <= PlayerHero.AttackDamage)
            {
                return new Commands.Raw($"MOVE_ATTACK {creepEnemyWithLowHealth.X} {creepEnemyWithLowHealth.Y} {creepEnemyWithLowHealth.Id}");
                //return new Commands.Move(creepShield.X, creepShield.Y);
            }*/
            // Pick Hero
            else if (EnemyTeam.Entities.Where(x => x is Creep).All(x => (x.Distance(enemyTower) + x.MovementSpeed + x.AttackRange) < EnemyTeam.Entities.OrderBy(y => y.Distance(PlayerHero)).FirstOrDefault()?.Distance(enemyTower)))
                return new Commands.Raw($"ATTACK_NEAREST {EntityType.HERO}");
            else
                return new Commands.Raw($"ATTACK_NEAREST {EntityType.UNIT}");
        }

        public CommandBase TowerAssaultStrategy(Tower enemyTower)
        {
            var creepShield = FindPlayerShieldCreep(enemyTower);
            Entity previousCreep;

            var playerCreeps = PlayerTeam.Entities.Where(x => x is Creep && x.IsAlive && x.Id != creepShield.Id);

            if (FactorDirection > 0)
                previousCreep = playerCreeps.OrderByDescending(x => x.X).FirstOrDefault();
            else
                previousCreep = playerCreeps.OrderBy(x => x.X).FirstOrDefault();

            if (previousCreep != null)
                return new Commands.Move(previousCreep.X, previousCreep.Y);
            else
                return new Commands.Move(creepShield.X, creepShield.Y);
        }

        public Creep FindPlayerShieldCreep(Tower tower)
        {
            Creep bestCreep = null;

            PlayerTeam.Entities.Where(x => x is Creep && x.IsAlive).ToList().ForEach(creep =>
            {
                if (bestCreep != null && creep.Distance(tower) < bestCreep.Distance(tower))
                    bestCreep = creep as Creep;
                else if (bestCreep == null)
                    bestCreep = creep as Creep;
            });

            return bestCreep;
        }


        public bool HasEnemyCreepWithLowHealth => EnemyTeam.Entities.Where(x => x is Creep && x.IsAlive).Any(x => x.Health != x.MaxHealth);
        public bool HasEnemyInRange(Hero hero) => EnemyTeam.Entities.Where(x => x is Creep && x.IsAlive).Any(x => x.IsInRange(hero, hero.AttackRange));
        public bool HasEnemyInRange(Point heroPosition, double heroAttackRange) => EnemyTeam.Entities.Where(x => x is Creep && x.IsAlive).Any(x => x.IsInRange(heroPosition, heroAttackRange));
        public Creep PlayerCreepWithLowHealth => PlayerTeam.Entities.Where(x => x is Creep && x.IsAlive).OrderBy(x => x.Health).FirstOrDefault() as Creep;
        public Creep EnemyCreepWithLowHealth => EnemyTeam.Entities.Where(x => x is Creep && x.IsAlive).OrderBy(x => x.Health).FirstOrDefault() as Creep;

        public Consumable GetPotion() {
            Consumable consumable = null;

            var potion = Items.Where(x => x is Consumable && x.Health > 0)
                                .OrderByDescending(x => x.Health)
                                .FirstOrDefault(x => (x.Health <= PlayerHero.MaxHealth - PlayerHero.Health) &&
                                                    PlayerTeam.CanBuyItem(x, PlayerHero));

            if (potion != null)
                consumable = potion as Consumable;

            return consumable;
        }

        public bool IsInRangeOfEnemyHeroes(Creep creep)
        {
            bool isInRange = false;

            EnemyTeam.Entities.Where(x => x is Hero && x.IsAlive).ToList().ForEach(hero =>
            {
                if (creep.IsInRange(hero, hero.AttackRange))
                    isInRange = true;
            });

            return isInRange;
        }
    }
}
