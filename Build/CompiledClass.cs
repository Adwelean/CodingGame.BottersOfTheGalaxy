/*
 * File generated by SourceCombiner using 24 source files.
 * Created On: 03/03/2018 16:29:40
*/
using Core;
using Core.Actors;
using Core.AI;
using Core.Factories;
using Core.Fights;
using Core.Interactives;
using Core.Items;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Core
{
    public class GameEngine
    {
        Team playerTeam;
        Team ennemyTeam;
        FightManager fightManager = new FightManager();
        public List<Bush> Bushes { get; set; }
        public List<Spawn> Spawns { get; set; }
        public List<ItemBase> Items { get; set; }
        public Player CurrentPlayer { get; set; }
        public Team PlayerTeam { get => this.playerTeam; set => this.playerTeam = value; }
        public Team EnnemyTeam { get => this.ennemyTeam; set => this.ennemyTeam = value; }
        public GameEngine()
        {
            Bushes = new List<Bush>();
            Spawns = new List<Spawn>();
            Items = new List<ItemBase>();
        }
        static void Main(string[] args)
        {
            GameEngine game = new GameEngine();
            game.Initialize();
            game.Start();
        }
        public void Initialize()
        {
            int currentPlayerTeamId;
            string[] inputs;
            if (int.TryParse(Console.ReadLine(), out currentPlayerTeamId))
            {
                CurrentPlayer = new Player(currentPlayerTeamId);
            }
            int bushAndSpawnPointCount = int.Parse(Console.ReadLine()); // useful from wood1, represents the number of bushes and the number of places where neutral units can spawn
            for (int i = 0; i < bushAndSpawnPointCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                var interactiveObject = InteractiveObjectFactory.ParseInteractiveObject(inputs);
                if(interactiveObject != null)
                {
                    if (interactiveObject is Bush)
                        Bushes.Add(interactiveObject as Bush);
                    else if (interactiveObject is Spawn)
                        Spawns.Add(interactiveObject as Spawn);
                }
            }
            int itemCount = int.Parse(Console.ReadLine()); // useful from wood2
            for (int i = 0; i < itemCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                var item = ItemFactory.ParseItem(inputs);
                if (item != null)
                    Items.Add(item);
            }
            PlayerTeam = new Team(currentPlayerTeamId);
            EnnemyTeam = new Team(currentPlayerTeamId == 0 ? 1 : 0);
        }
        public void Start()
        {
            string[] inputs;
            this.fightManager.Initialize(CurrentPlayer);
            this.fightManager.AddTeam(ref this.playerTeam);
            this.fightManager.AddTeam(ref this.ennemyTeam);
            this.fightManager.Picking();
            // game loop
            while (true)
            {
                try
                {
                    PlayerTeam.UpdateEntities();
                    EnnemyTeam.UpdateEntities();
                    PlayerTeam.Gold = int.Parse(Console.ReadLine());
                    EnnemyTeam.Gold = int.Parse(Console.ReadLine());
                    int roundType = int.Parse(Console.ReadLine()); // a positive value will show the number of heroes that await a command
                    int entityCount = int.Parse(Console.ReadLine());
                    for (int i = 0; i < entityCount; i++)
                    {
                        inputs = Console.ReadLine().Split(' ');
                        int teamId = int.Parse(inputs[1]);
                        var entity = EntityFactory.ParseEntity(inputs);
                        // TODO: add method ? observer ?
                        var team = (PlayerTeam.Id == teamId) ? PlayerTeam : EnnemyTeam;
                        team.Entities.Add(entity);
                    }
                    // Write an action using Console.WriteLine()
                    // To debug: Console.Error.WriteLine("Debug messages...");
                    this.fightManager.NextTurn();
                    // If roundType has a negative value then you need to output a Hero name, such as "DEADPOOL" or "VALKYRIE".
                    // Else you need to output roundType number of any valid action, such as "WAIT" or "ATTACK unitId"
                    //Console.WriteLine("WAIT");
                    Console.WriteLine(this.fightManager.RenderOutput());
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.InnerException);
                    Console.WriteLine("WAIT");
                }
            }
        }
    }
}
namespace Core
{
    class GameSettings
    {
        //LEAGUE
        // wood3 = 0;
        // wood2 = 1;
        // wood1 = 2;
        // bronze and above = 3+;
        public const bool RemoveForestCreatures = false;
        public const bool IgnoreItems = false;
        public const bool IgnoreSkills = false;
        public const double TowerHealthScale = 1.0;
        public const bool IgnoreBushes = false;
        //MISC
        public const double Epsilon = 0.00001;
        public const double RoundTime = 1.0;
        public const int Rounds = 250;
        public const int MapWidth = 1920;
        public const int MapHeight = 780;
        public const int HeroCount = 2;
        public const int MaxItemCount = 4;
        public const int MeleeUnitCount = 3;
        public const int RangedUnitCount = 1;
        public const double SellItemRefund = 0.5;
        //TEAM A
        public static readonly Point TowerTeamA = new Point(100, 540);
        public static readonly Point SpawnTeamA = new Point(TowerTeamA.X + 60, TowerTeamA.Y - 50);
        public static readonly Point HeroSpawnTeamA = new Point(TowerTeamA.X + 100, TowerTeamA.Y + 50);
        //public static readonly Point HeroSpawnTeamAHero2 = new Point(HeroSpawnTeamA.X, HeroSpawnTeamA.Y - 50); TODO: dynamically
        //TEAM B
        public static readonly Point TowerTeamB = new Point(MapWidth - TowerTeamA.X, TowerTeamA.Y);
        public static readonly Point SpawnTeamB = new Point(MapWidth - SpawnTeamA.X, SpawnTeamA.Y);
        public static readonly Point HeroSpawnTeamB = new Point(MapWidth - HeroSpawnTeamA.X, HeroSpawnTeamA.Y);
        //public static readonly Point HEROSPAWNTEAM1HERO2 = new Point(HEROSPAWNTEAM1.x, HEROSPAWNTEAM0HERO2.y);
        //HERO
        public const int SkillCount = 3;
        public const int MaxMoveSpeed = 450;
        //UNIT
        public const int SpawnRate = 15;
        public const int UnitTargetDistance = 400;
        public const int UnitTargetDistance2 = UnitTargetDistance * UnitTargetDistance;
        public const int AggroUnitRange = 300;
        public const int AggroUnitRange2 = AggroUnitRange * AggroUnitRange;
        public const int AggroUnitTime = 3;
        public const double DenyHealth = 0.4;
        public const double BushRadius = 50;
        //TOWERS
        public const int TowerHealth = 3000;
        //NEUTRAL CREEP
        public const int NeutralSpawnTime = 4;
        public const int NeutralSpawnRate = 40;
        public const int NeutralGold = 100;
        public const int NeutralAggroRange = 600;
        /// SPELLS
        /// 
        // KNIGHT
        public const double ExplosiveShieldRange2 = 151 * 151;
        public const int ExplosiveShieldDamage = 50;
        // DOCTOR STRANGE
        public const double RepelRadius = 150;
        public const double RepelPushDistance = 200;
        // LANCER
        public const int PowerUpMoveSpeed = 0;
        public const double PowerUpDamageIncrease = 0.3;
        public const int PowerUpRange = 10;
        //GOLD UNIT VALUES
        public const int StartingGoldValue = 0;
        public const int MeleeUnitGoldValue = 30;
        public const int RangerUnitGoldValue = 50;
        public const int HeroGoldValue = 300;
        public const int GlobalId = 1;
    }
}
namespace Core.Actors
{
    public class Creep : Entity
    {
        public Creep(
            double x,
            double y,
            int id,
            int attackRange,
            int health,
            int maxHealth,
            int shield,
            int attackDamage,
            int movementSpeed,
            int stunDuration,
            int goldValue
        ) : base(
            x,
            y,
            id,
            attackRange,
            health,
            maxHealth,
            shield,
            attackDamage,
            movementSpeed,
            stunDuration,
            goldValue)
        {
        }
    }
}
namespace Core.Actors
{
    public enum EntityType
    {
        UNIT,
        HERO,
        TOWER,
        GROOT,
    }
    public class Entity : EntityBase
    {
        int id;
        int attackRange;
        int health;
        int maxHealth;
        int shield; // useful in bronze
        int attackDamage;
        int movementSpeed;
        int stunDuration; // useful in bronze
        int goldValue;
        public Entity(
            double x,
            double y,
            int id,
            int attackRange,
            int health,
            int maxHealth,
            int shield,
            int attackDamage,
            int movementSpeed,
            int stunDuration,
            int goldValue) : base(x, y)
        {
            this.id = id;
            this.attackRange = attackRange;
            this.health = health;
            this.maxHealth = maxHealth;
            this.shield = shield;
            this.attackDamage = attackDamage;
            this.movementSpeed = movementSpeed;
            this.stunDuration = stunDuration;
            this.goldValue = goldValue;
        }
        public int Id { get => id; set => id = value; }
        public int AttackRange { get => attackRange; set => attackRange = value; }
        public int Health { get => health; set => health = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Shield { get => shield; set => shield = value; }
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }
        public int MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public int StunDuration { get => stunDuration; set => stunDuration = value; }
        public int GoldValue { get => goldValue; set => goldValue = value; }
        public bool IsAlive => Health > 0;
    }
}
namespace Core.Actors
{
    public abstract class EntityBase : Point
    {
        public EntityBase(double x, double y)
            : base(x, y)
        {
        }
    }
}
namespace Core.Actors
{
    public class Groot : Entity
    {
        public Groot(
            double x,
            double y,
            int id,
            int attackRange,
            int health,
            int maxHealth,
            int shield,
            int attackDamage,
            int movementSpeed,
            int stunDuration,
            int goldValue
        ) : base(
            x,
            y,
            id,
            attackRange,
            health,
            maxHealth,
            shield,
            attackDamage,
            movementSpeed,
            stunDuration,
            goldValue)
        {
        }
    }
}
namespace Core.Actors
{
    public enum HeroType
    {
        DEADPOOL,
        VALKYRIE,
        DOCTOR_STRANGE,
        HULK,
        IRONMAN,
    }
    public class Hero : Entity
    {
        HeroType heroType;
        int countDown1; // all countDown and mana variables are useful starting in bronze
        int countDown2;
        int countDown3;
        int mana;
        int maxMana;
        int manaRegeneration;
        int isVisible; // 0 if it isn't
        int itemsOwned; // useful from wood1
        public Hero(
            double x,
            double y,
            int id,
            int attackRange,
            int health,
            int maxHealth,
            int shield,
            int attackDamage,
            int movementSpeed,
            int stunDuration,
            int goldValue,
            int countDown1,
            int countDown2,
            int countDown3,
            int mana,
            int maxMana,
            int manaRegeneration,
            int isVisible,
            int itemsOwned,
            HeroType heroType
        ) : base(
            x,
            y,
            id,
            attackRange,
            health,
            maxHealth,
            shield,
            attackDamage,
            movementSpeed,
            stunDuration,
            goldValue
            )
        {
            this.heroType = heroType;
            this.countDown1 = countDown1;
            this.countDown2 = countDown2;
            this.countDown3 = countDown3;
            this.mana = mana;
            this.maxMana = maxMana;
            this.manaRegeneration = manaRegeneration;
            this.isVisible = isVisible;
            this.itemsOwned = itemsOwned;
        }
        public HeroType HeroType { get => heroType; set => heroType = value; }
        public int CountDown1 { get => countDown1; set => countDown1 = value; }
        public int CountDown2 { get => countDown2; set => countDown2 = value; }
        public int CountDown3 { get => countDown3; set => countDown3 = value; }
        public int Mana { get => mana; set => mana = value; }
        public int MaxMana { get => maxMana; set => maxMana = value; }
        public int ManaRegeneration { get => manaRegeneration; set => manaRegeneration = value; }
        public int IsVisible { get => isVisible; set => isVisible = value; }
        public int ItemsOwned { get => itemsOwned; set => itemsOwned = value; }
        public IArtificialIntelligence AI { get; set; }
    }
}
namespace Core.Actors
{
    public class Player
    {
        int teamId;
        IArtificialIntelligence ai;
        public Player(int teamId)
        {
            this.teamId = teamId;
            //this.ai = new AI.Rush();
            this.ai = new AI.Basic();
        }
        public HeroType ChooseHero(Team team)
        {
            return HeroType.VALKYRIE;
        }
        public int TeamId { get => teamId; set => teamId = value; }
        public IArtificialIntelligence AI { get => ai; set => ai = value; }
    }
}
namespace Core.Actors
{
    public class Tower : Entity
    {
        public Tower(
            double x,
            double y,
            int id,
            int attackRange,
            int health,
            int maxHealth,
            int shield,
            int attackDamage,
            int movementSpeed,
            int stunDuration,
            int goldValue
        ) : base(
            x,
            y,
            id,
            attackRange,
            health,
            maxHealth,
            shield,
            attackDamage,
            movementSpeed,
            stunDuration,
            goldValue)
        {
        }
    }
}
namespace Core.AI
{
    public class Basic : IArtificialIntelligence
    {
        double ApplyDirection(int teamId, double to) => (teamId > 0) ? GameSettings.HeroSpawnTeamB.X - to : to;
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
                    return $"MOVE {ApplyDirection(currentTeam.Id, myLastCreep.X - ((myLastCreep.X > myHero.MovementSpeed) ? myHero.MovementSpeed : 0))} {myLastCreep.Y}";
                else
                    return $"MOVE {myTower.X} {myTower.Y}";
            }
            if (distanceBetweenMyHeroAndTheTower > tower.AttackRange)
            {
                // Pick and roll
                if (myHeroLastTurn != null && myHero.Health < myHeroLastTurn.Health)
                    return $"MOVE {ApplyDirection(currentTeam.Id, target.X - (target.MovementSpeed + target.AttackRange))} {target.Y}";
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
                    return $"MOVE {ApplyDirection(currentTeam.Id, myLastCreep.X - ((myLastCreep.X > myHero.MovementSpeed) ? myHero.MovementSpeed : 0))} {myLastCreep.Y}";
                else
                    return $"MOVE {myTower.X} {myTower.Y}";
            }
        }
    }
}
namespace Core.AI
{
    public interface IArtificialIntelligence
    {
        string ComputeAction(Team currentTeam, Team ennemyTeam);
    }
}
namespace Core.AI
{
    public class Rush : IArtificialIntelligence
    {
        public string ComputeAction(Team currentTeam, Team ennemyTeam)
        {
            var target = ennemyTeam.Entities.FirstOrDefault(x => x is Tower) as Tower;
            return $"MOVE_ATTACK {target.X} {target.Y} {target.Id}";            
        }
    }
}
namespace Core.Factories
{
    public static class EntityFactory
    {
        public static Entity ParseEntity(string[] inputs)
        {
            EntityType entityType;
            Entity entity = null;
            int unitId = int.Parse(inputs[0]);
            int x = int.Parse(inputs[3]);
            int y = int.Parse(inputs[4]);
            int attackRange = int.Parse(inputs[5]);
            int health = int.Parse(inputs[6]);
            int maxHealth = int.Parse(inputs[7]);
            int shield = int.Parse(inputs[8]); // useful in bronze
            int attackDamage = int.Parse(inputs[9]);
            int movementSpeed = int.Parse(inputs[10]);
            int stunDuration = int.Parse(inputs[11]); // useful in bronze
            int goldValue = int.Parse(inputs[12]);
            int countDown1 = int.Parse(inputs[13]); // all countDown and mana variables are useful starting in bronze
            int countDown2 = int.Parse(inputs[14]);
            int countDown3 = int.Parse(inputs[15]);
            int mana = int.Parse(inputs[16]);
            int maxMana = int.Parse(inputs[17]);
            int manaRegeneration = int.Parse(inputs[18]);
            int isVisible = int.Parse(inputs[20]); // 0 if it isn't
            int itemsOwned = int.Parse(inputs[21]); // useful from wood1
            //entity = new Entity(x, y, unitId, attackRange, health, maxHealth, shield, attackDamage, movementSpeed, stunDuration, goldValue);
            string unitType = inputs[2]; // UNIT, HERO, TOWER, can also be GROOT from wood1
            if(Enum.TryParse(unitType, out entityType))
            {
                switch (entityType)
                {
                    case EntityType.UNIT:
                        //entity = UnBoxingHelper.ToDerived<Entity, Creep>(entity);
                        entity = new Creep(x, y, unitId, attackRange, health, maxHealth, shield, attackDamage, movementSpeed, stunDuration, goldValue);
                        break;
                    case EntityType.HERO:
                        HeroType heroType;
                        string tmpHeroType = inputs[19]; // DEADPOOL, VALKYRIE, DOCTOR_STRANGE, HULK, IRONMAN
                        // TODO: refactoring
                        if(Enum.TryParse(tmpHeroType, out heroType))
                        {
                            /*entity = UnBoxingHelper.ToDerived<Entity, Hero>(entity);
                            (entity as Hero).HeroType = heroType;*/
                            entity = new Hero(x, y, unitId, attackRange, health, maxHealth, shield, attackDamage, movementSpeed, stunDuration, goldValue, countDown1, countDown2, countDown3, mana, maxMana, manaRegeneration, isVisible, itemsOwned, heroType);
                        }
                        break;
                    case EntityType.TOWER:
                        //entity = UnBoxingHelper.ToDerived<Entity, Tower>(entity);
                        entity = new Tower(x, y, unitId, attackRange, health, maxHealth, shield, attackDamage, movementSpeed, stunDuration, goldValue);
                        break;
                    case EntityType.GROOT:
                        //entity = UnBoxingHelper.ToDerived<Entity, Groot>(entity);
                        entity = new Groot(x, y, unitId, attackRange, health, maxHealth, shield, attackDamage, movementSpeed, stunDuration, goldValue);
                        break;
                }
            }
            return entity;
        }
    }
}
namespace Core.Factories
{
    public static class InteractiveObjectFactory
    {
        public enum InteractiveObjectType
        {
            NONE,
            BUSH,
            SPAWN,
        }
        public static EntityBase ParseInteractiveObject(string[] inputs)
        {
            EntityBase entity = null;
            InteractiveObjectType interactiveType = InteractiveObjectType.NONE;
            string entityType = inputs[0]; // BUSH, from wood1 it can also be SPAWN
            int x = int.Parse(inputs[1]);
            int y = int.Parse(inputs[2]);
            int radius = int.Parse(inputs[3]);
            if(Enum.TryParse(entityType, out interactiveType))
            {
                switch(interactiveType)
                {
                    case InteractiveObjectType.BUSH:
                        entity = new Bush(x, y, radius);
                        break;
                    case InteractiveObjectType.SPAWN:
                        entity = new Spawn(x, y);
                        break;
                }
            }
            return entity;
        }
    }
}
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
namespace Core.Fights
{
    public enum FightPhase
    {
        NONE,
        PLACEMENT,
        FIGHTING,
        ENDED
    }
    public class Fight
    {
        List<string> output = new List<string>();
        public Player CurrentPlayer { get; set; }
        public FightPhase FightPhase { get; set; }
        public Team PlayerTeam { get; set; }
        public Team EnnemyTeam { get; set; }
        public IArtificialIntelligence CurrentAI { get; set; }
        public Fight()
        {
        }
        public void Initialize(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;
            CurrentAI = currentPlayer.AI;
            FightPhase = FightPhase.PLACEMENT;
        }
        public void AddTeam(ref Team team)
        {
            if (team.Id == CurrentPlayer.TeamId)
                PlayerTeam = team;
            else
                EnnemyTeam = team;
        }
        public void PickingPhase()
        {
            if (FightPhase == FightPhase.PLACEMENT)
            {
                this.output.Add(CurrentPlayer.ChooseHero(EnnemyTeam).ToString());
                FightPhase = FightPhase.FIGHTING;
            }
        }
        public void NextTurn()
        {
            PlayerTeam.Entities.Where(x => x is Hero).Cast<Hero>().ToList().ForEach(hero => {
                if (hero.AI != null)
                    this.output.Add(hero.AI.ComputeAction(PlayerTeam, EnnemyTeam));
                else
                    this.output.Add(CurrentAI.ComputeAction(PlayerTeam, EnnemyTeam));
            });
        }
        public string BuildOutput()
        {
            var outputResult = string.Concat(output);
            output.Clear();
            return outputResult;
        }
    }
}
namespace Core.Fights
{
    public class FightManager
    {
        Fight fight;
        public FightManager()
        {
            this.fight = new Fight();
        }
        public void Initialize (Player player) => this.fight.Initialize(player);
        public void AddTeam(ref Team team) => this.fight.AddTeam(ref team);
        public void Picking() => this.fight.PickingPhase();
        public void NextTurn()
        {
            this.fight.NextTurn();
        }
        public string RenderOutput()
        {
            var outputResult = this.fight.BuildOutput();
            return !string.IsNullOrEmpty(outputResult) ? outputResult : "WAIT";
        }
    }
}
namespace Core.Fights
{
    public class Team
    {
        int id;
        int gold;
        List<EntityBase> entities, lastEntities;
        public Team(int id, int gold = GameSettings.StartingGoldValue)
        {
            this.id = id;
            this.gold = gold;
            this.entities = new List<EntityBase>();
            this.lastEntities = new List<EntityBase>();
        }
        public void UpdateEntities()
        {
            //Entities.Clear();
            if (Entities.Count > 0)
            {
                LastEntities = new List<EntityBase>(Entities);
                Entities.Clear();
            }
        }
        public int Id { get => this.id; set => this.id = value; }
        public int Gold { get => this.gold; set => this.gold = value; }
        public List<EntityBase> Entities { get => this.entities; set => this.entities = value; }
        public List<EntityBase> LastEntities { get => lastEntities; set => lastEntities = value; }
    }
}
namespace Core.Interactives
{
    public class Bush : EntityBase
    {
        int radius;
        public Bush(double x, double y, int radius) 
            : base(x, y)
        {
            this.radius = radius;
        }
    }
}
namespace Core.Interactives
{
    public class Spawn : EntityBase
    {
        public Spawn(double x, double y) 
            : base(x, y)
        {
        }
    }
}
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
namespace Core.Utils
{
    public class Point
    {
        double x;
        double y;
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public double Distance(Point p) => Math.Sqrt(this.ComputeDistance(p));
        public double ComputeDistance(Point p) => ((this.x - p.x) * (this.x - p.x) + (this.y - p.y) * (this.y - p.y));
        public bool IsInRange(Point p, double range) => p != this && this.Distance(p) <= range;
        public double X { get => this.x; set => this.x = value; }
        public double Y { get => this.y; set => this.y = value; }
    }
}
