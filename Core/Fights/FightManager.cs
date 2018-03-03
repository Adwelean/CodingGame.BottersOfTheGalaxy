namespace Core.Fights
{
    using Core.Actors;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FightManager
    {
        StringBuilder output;
        Fight fight;

        public FightManager()
        {
            output = new StringBuilder();
            this.fight = new Fight(ref this.output);
        }

        public void Initialize (Player player) => this.fight.Initialize(player);

        public void AddTeams(List<Team> teams) => teams.ForEach(x => this.fight.AddTeam(x));

        public void StartFight() => this.fight.Start();
        public void NextTurn()
        {
            output.Clear();
            this.fight.Next();
        }

        public string RenderOutput()
        {
            var outputResult = output.ToString();

            return outputResult != "" ? outputResult : "WAIT";
        }
    }
}
