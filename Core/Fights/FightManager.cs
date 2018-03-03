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

        public void AddTeam(ref Team team) => this.fight.AddTeam(ref team);

        public void Picking() => this.fight.PickingPhase();

        public void NextTurn()
        {
            output.Clear();
            this.fight.NextTurn();
        }

        public string RenderOutput()
        {
            var outputResult = output.ToString();

            return !string.IsNullOrEmpty(outputResult) ? outputResult : "WAIT";
        }
    }
}
