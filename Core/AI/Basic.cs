namespace Core.AI
{
    using System;
    using System.Linq;
    using Core.Actors;
    using Core.Fights;

    public class Basic : IArtificialIntelligence
    {
        public string ComputeAction(Team currentTeam, Team ennemyTeam)
        {
            var myHeroLastTurn = currentTeam.LastEntities.FirstOrDefault(x => x is Hero) as Hero;
            var myHero = currentTeam.Entities.FirstOrDefault(x => x is Hero) as Hero;
            var myTower = currentTeam.Entities.FirstOrDefault(x => x is Tower) as Tower;
            var myLastCreep = currentTeam.Entities.Where(x => x is Creep).Cast<Creep>().FirstOrDefault(x => x.IsAlive);

            var target = ennemyTeam.Entities.FirstOrDefault(x => x is Creep) as Creep;
            var tower = ennemyTeam.Entities.FirstOrDefault(x => x is Tower) as Tower;
            var ennemyHero = ennemyTeam.Entities.FirstOrDefault(x => x is Hero) as Hero;

            Console.Error.WriteLine(
                $"MyHero: x={myHero.X} y={myHero.Y} range={myHero.AttackRange}\n" +
                $"Creep: x={target.X} y={target.Y} range={target.AttackRange}\n" +
                $"Tower: x={tower.X} y={tower.Y} range={tower.AttackRange}\n" +
                $"Distance between myHero and creep={myHero.Distance(target)}\n" +
                $"Distance between myHero and tower={myHero.Distance(tower)}\n");

            double distanceBetweenMyHeroAndTheTower = myHero.Distance(tower);

            // Safe
            if (myHero.Health < myHero.MaxHealth / 3)
            {
                if (myLastCreep != null)
                    return $"MOVE {myLastCreep.X - ((myLastCreep.X > myHero.MovementSpeed) ? myHero.MovementSpeed : 0)} {myLastCreep.Y}";
                else
                    return $"MOVE {myTower.X} {myTower.Y}";
            }

            if (distanceBetweenMyHeroAndTheTower > tower.AttackRange)
            {
                // Pick and roll
                if (myHeroLastTurn != null && myHero.Health < myHeroLastTurn.Health)
                    return $"MOVE {target.X - (target.MovementSpeed + target.AttackRange)} {target.Y}";
                        
                if (target != null)
                {
                    if(myHero.Distance(target) > target.AttackRange && myHero.Distance(ennemyHero) <= myHero.AttackRange)
                        return $"ATTACK_NEAREST {EntityType.HERO}";
                    else if (myHero.Distance(target) <= myHero.AttackRange + myHero.MovementSpeed)
                        return $"ATTACK_NEAREST {EntityType.UNIT}";
                    else if (myLastCreep != null)
                        return $"MOVE {myLastCreep.X} {myLastCreep.Y}";
                    else
                        return $"MOVE {myTower.X} {myTower.Y}";
                }
                else
                    return $"ATTACK_NEAREST {EntityType.HERO}";
            }
            else
            {
                if (myLastCreep != null)
                    return $"MOVE {myLastCreep.X - ((myLastCreep.X > myHero.MovementSpeed) ? myHero.MovementSpeed : 0)} {myLastCreep.Y}";
                else
                    return $"MOVE {myTower.X} {myTower.Y}";
            }
        }
    }
}
