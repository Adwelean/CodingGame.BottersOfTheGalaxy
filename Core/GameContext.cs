namespace Core
{
    using Core.Actors;
    using Core.AI.Commands;
    using Core.Fights;
    using Core.Items;
    using System.Collections.Generic;

    public class GameContext
    {
        Team playerTeam;
        Team enemyTeam;
        Hero playerHero;
        List<ItemBase> items;
        CommandBase lastCommand;

        public GameContext(Team playerTeam, Team enemyTeam, Hero playerHero, List<ItemBase> items, CommandBase lastCommand)
        {
            this.playerTeam = playerTeam;
            this.enemyTeam = enemyTeam;
            this.playerHero = playerHero;
            this.items = items;
            this.lastCommand = lastCommand;
        }

        public Team PlayerTeam { get => playerTeam; set => playerTeam = value; }
        public Team EnemyTeam { get => enemyTeam; set => enemyTeam = value; }
        public Hero PlayerHero { get => playerHero; set => playerHero = value; }
        public List<ItemBase> Items { get => items; set => items = value; }
        public CommandBase LastCommand { get => lastCommand; set => lastCommand = value; }
    }
}
