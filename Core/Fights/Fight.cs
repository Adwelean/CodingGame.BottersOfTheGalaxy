namespace Core.Fights
{
    using Core.Actors;
    using Core.AI;
    using Core.AI.Commands;
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

        public Player CurrentPlayer { get; set; }
        public FightPhase FightPhase { get; set; }
        public Team PlayerTeam { get; set; }
        public Team EnnemyTeam { get; set; }
        public IArtificialIntelligence CurrentAI { get; set; }
        public CommandBase LastCommand { get; set; }
       

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
                    LastCommand = hero.AI.ComputeAction(PlayerTeam, EnnemyTeam, hero, LastCommand);
                else
                    LastCommand = CurrentAI.ComputeAction(PlayerTeam, EnnemyTeam, hero, LastCommand);

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
