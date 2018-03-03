namespace Core.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Actors;
    using Core.AI.Commands;
    using Core.Fights;
    using Core.Utils;

    public class Basic : IArtificialIntelligence
    {
        public enum State
        {
            Protect,
            Attack,
            TowerAssault
        }

        Team PlayerTeam { get; set; }
        Team EnnemyTeam { get; set; }

        int FactorDirection => (PlayerTeam.Id > 0) ? -1 : 1;

        public CommandBase ComputeAction(Team playerTeam, Team ennemyTeam, Hero playerHero, CommandBase lastCommand)
        {
            CommandBase action = new Commands.Wait();

            PlayerTeam = playerTeam;
            EnnemyTeam = ennemyTeam;

            var playerHeroes = PlayerTeam.Entities.Where(x => x is Hero).Cast<Hero>().ToList();
            var playerTower = PlayerTeam.Entities.FirstOrDefault(x => x is Tower) as Tower;

            var ennemyHeroes = EnnemyTeam.Entities.Where(x => x is Hero).Cast<Hero>().ToList();
            var ennemyTower = EnnemyTeam.Entities.FirstOrDefault(x => x is Tower) as Tower;

            var playerState = ComputeState(playerTower, playerHero, ennemyTower, ennemyHeroes);

            switch(playerState)
            {
                case State.Attack:
                    action = AttackStrategy(playerTower, ennemyTower, playerHero, lastCommand);
                    break;

                case State.TowerAssault:
                    action = TowerAssaultStrategy(ennemyTower);
                    break;

                default:
                    action = ProtectStrategy(playerTower);
                    break;
            }

            return action;
        }

        public State ComputeState(Tower playerTower, Hero playerHero, Tower ennemyTower, List<Hero> ennemyHeroes)
        {
            if (PlayerTeam.Entities.Count(x => x is Creep) < EnnemyTeam.Entities.Count(x => x is Creep) &&
               ennemyHeroes.Any(x => playerTower.Distance(x) < GameSettings.MapWidth / 3))
            {
                return State.Protect;
            }
            else if (PlayerTeam.Entities.Count(x => x is Creep) > EnnemyTeam.Entities.Count(x => x is Creep) &&
                     playerHero.Distance(ennemyTower) < GameSettings.MapWidth / 4) // TODO: change by playerHero.IsInRange(ennemyTower, ennemyTower.AttackRange) ?
            {
                return State.TowerAssault;
            }
            else
                return State.Attack;
        }

        public CommandBase ProtectStrategy(Tower playerTower)
        {
            return new Commands.Move(playerTower.X, playerTower.Y);
        }

        public CommandBase AttackStrategy(Tower playerTower, Tower ennemyTower, Hero playerHero, CommandBase lastCommand)
        {
            var creepShield = FindPlayerShieldCreep(ennemyTower);
            var creepEnnemyWithLowHealth = EnnemyCreepWithLowHealth;
            var hasEnnemyCreepWithLowHealth = HasEnnemyCreepWithLowHealth;

            if (lastCommand != null &&
                lastCommand.Build().Contains("ATTACK") &&
                !(hasEnnemyCreepWithLowHealth &&
                creepEnnemyWithLowHealth != null &&
                creepEnnemyWithLowHealth.IsInRange(playerHero, playerHero.AttackRange) &&
                creepEnnemyWithLowHealth.Health <= playerHero.AttackDamage))
            {
                if (creepShield != null)
                    return new Commands.Move(creepShield.X, creepShield.Y);
                else
                    return new Commands.Move(playerTower.X, playerTower.Y);
            }

            // Check last condition (direction)
            if(!HasEnnemyInRange(playerHero) && !HasEnnemyInRange(new Point(playerHero.X + playerHero.MovementSpeed, playerHero.Y), playerHero.AttackRange))
                return new Commands.Move(creepShield.X, creepShield.Y);

            if (hasEnnemyCreepWithLowHealth &&
                creepEnnemyWithLowHealth != null &&
                creepEnnemyWithLowHealth.IsInRange(playerHero, playerHero.AttackRange) &&
                creepEnnemyWithLowHealth.Health <= playerHero.AttackDamage)
            {
                return new Commands.Raw($"ATTACK {creepEnnemyWithLowHealth.Id}");
            }
            // TODO: can be move to the creep and attack ? check ratio < 1
            /*else if (hasEnnemyCreepWithLowHealth &&
                     creepEnnemyWithLowHealth != null &&
                     creepEnnemyWithLowHealth.Health <= playerHero.AttackDamage)
            {
                return new Commands.Raw($"MOVE_ATTACK {creepEnnemyWithLowHealth.X} {creepEnnemyWithLowHealth.Y} {creepEnnemyWithLowHealth.Id}");
                //return new Commands.Move(creepShield.X, creepShield.Y);
            }*/
            else
                return new Commands.Raw($"ATTACK_NEAREST {EntityType.UNIT}"); // TODO: wait a cover if the distance between hero and these creeps is too large
        }

        public CommandBase TowerAssaultStrategy(Tower ennemyTower)
        {
            var creepShield = FindPlayerShieldCreep(ennemyTower);

            // TODO: warning direction !
            var previousCreep = PlayerTeam.Entities.Where(x => x is Creep && x.IsAlive && x.Id != creepShield.Id).OrderByDescending(x => x.X).FirstOrDefault();

            if(previousCreep != null)
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


        public bool HasEnnemyCreepWithLowHealth => EnnemyTeam.Entities.Where(x => x is Creep && x.IsAlive).Any(x => x.Health != x.MaxHealth);
        public bool HasEnnemyInRange(Hero hero) => EnnemyTeam.Entities.Where(x => x is Creep && x.IsAlive).Any(x => x.IsInRange(hero, hero.AttackRange));
        public bool HasEnnemyInRange(Point heroPosition, double heroAttackRange) => EnnemyTeam.Entities.Where(x => x is Creep && x.IsAlive).Any(x => x.IsInRange(heroPosition, heroAttackRange));
        public Creep PlayerCreepWithLowHealth => PlayerTeam.Entities.Where(x => x is Creep && x.IsAlive).OrderBy(x => x.Health).FirstOrDefault() as Creep;
        public Creep EnnemyCreepWithLowHealth => EnnemyTeam.Entities.Where(x => x is Creep && x.IsAlive).OrderBy(x => x.Health).FirstOrDefault() as Creep;

        public bool IsInRangeOfEnemyHeroes(Creep creep)
        {
            bool isInRange = false;

            EnnemyTeam.Entities.Where(x => x is Hero && x.IsAlive).ToList().ForEach(hero =>
            {
                if (creep.IsInRange(hero, hero.AttackRange))
                    isInRange = true;
            });

            return isInRange;
        }
    }
}
