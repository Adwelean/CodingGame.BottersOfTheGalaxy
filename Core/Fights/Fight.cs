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
        public List<Team> Teams { get; set; }

        public IArtificialIntelligence BasicIA = new Rush();
        

        public Fight(ref StringBuilder output)
        {
            this.output = output;
            Teams = new List<Team>();
        }

        public void Initialize(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;
            FightPhase = FightPhase.PLACEMENT;
        }

        public void AddTeam(Team team)
        {
            if(FightPhase == FightPhase.PLACEMENT && !Teams.Any(x => x.Id == team.Id))
                Teams.Add(team);
        }

        public void Pick()
        {
            if (FightPhase == FightPhase.PLACEMENT)
            {
                this.output.AppendLine(HeroType.DEADPOOL.ToString());
                FightPhase = FightPhase.FIGHTING;
            }
        }

        public void Next()
        {
            var currentTeam = Teams.FirstOrDefault(x => x.Id == CurrentPlayer.TeamId);

            currentTeam.Entities.Where(x => x is Hero).Cast<Hero>().ToList().ForEach(hero => {
                if (hero.IA != null)
                    this.output.AppendLine(hero.IA.ComputeAction(currentTeam, Teams.FirstOrDefault(x => x.Id != CurrentPlayer.TeamId)));
                else
                    this.output.AppendLine(BasicIA.ComputeAction(currentTeam, Teams.FirstOrDefault(x => x.Id != CurrentPlayer.TeamId)));
            });
        }
    }
}
