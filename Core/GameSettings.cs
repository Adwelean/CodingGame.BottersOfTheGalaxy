using Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Game
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
