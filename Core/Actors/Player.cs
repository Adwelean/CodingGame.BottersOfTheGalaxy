namespace Core.Actors
{
    using System.Linq;
    using Core.Fights;
    using Core.AI;

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
            return HeroType.DEADPOOL;
        }

        public int TeamId { get => teamId; set => teamId = value; }
        public IArtificialIntelligence AI { get => ai; set => ai = value; }
    }
}
