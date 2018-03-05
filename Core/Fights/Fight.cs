namespace Core.Fights
{
    using Core.Actors;
    using Core.AI;
    using Core.AI.Commands;
    using Core.Items;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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

        Team playerTeam;
        Team enemyTeam;

        public Player CurrentPlayer { get; set; }
        public FightPhase FightPhase { get; set; }
        public Team PlayerTeam { get => playerTeam; set => playerTeam = value; }
        public Team EnemyTeam { get => enemyTeam; set => enemyTeam = value; }
        public IArtificialIntelligence CurrentAI { get; set; }
        public CommandBase LastCommand { get; set; }
        public List<ItemBase> Items { get; set; }
      
        public Fight()
        {
        }

        public void Initialize(Player currentPlayer, List<ItemBase> items)
        {
            CurrentPlayer = currentPlayer;
            Items = items;
            CurrentAI = currentPlayer.AI;
            FightPhase = FightPhase.PLACEMENT;
        }

        public void AddTeam(ref Team team)
        {
            if (team.Id == CurrentPlayer.TeamId)
                PlayerTeam = team;
            else
                EnemyTeam = team;
        }

        public void PickingPhase()
        {
            if (FightPhase == FightPhase.PLACEMENT)
            {
                this.output.Add(CurrentPlayer.ChooseHero(EnemyTeam).ToString());
                FightPhase = FightPhase.FIGHTING;
            }
        }

        public void NextTurn()
        {
            PlayerTeam.Entities.Where(x => x is Hero).Cast<Hero>().ToList().ForEach(hero => {
                if (hero.AI != null)
                    LastCommand = hero.AI.ComputeAction(new GameContext(ref this.playerTeam, ref this.enemyTeam, ref hero, Items, LastCommand));
                else
                    LastCommand = CurrentAI.ComputeAction(new GameContext(ref this.playerTeam, ref this.enemyTeam, ref hero, Items, LastCommand));

                this.output.Add(LastCommand.Build());
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
