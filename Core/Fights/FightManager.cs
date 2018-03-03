namespace Core.Fights
{
    using Core.Actors;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
