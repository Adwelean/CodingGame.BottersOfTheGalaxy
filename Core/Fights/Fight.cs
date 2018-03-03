namespace Core.Fights
{
    using Core.Actors;
    using Core.IA;
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
        StringBuilder output;

        public Player CurrentPlayer { get; set; }
        public FightPhase FightPhase { get; set; }
        public Team PlayerTeam { get; set; }
        public Team EnnemyTeam { get; set; }
        public IArtificialIntelligence CurrentAI { get; set; }
       

        public Fight(ref StringBuilder output)
        {
            this.output = output;
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
                this.output.AppendLine(CurrentPlayer.ChooseHero(EnnemyTeam).ToString());
                FightPhase = FightPhase.FIGHTING;
            }
        }

        public void NextTurn()
        {
            PlayerTeam.Entities.Where(x => x is Hero).Cast<Hero>().ToList().ForEach(hero => {
                if (hero.IA != null)
                    this.output.AppendLine(hero.IA.ComputeAction(PlayerTeam, EnnemyTeam));
                else
                    this.output.AppendLine(CurrentAI.ComputeAction(PlayerTeam, EnnemyTeam));
            });
        }
    }
}
