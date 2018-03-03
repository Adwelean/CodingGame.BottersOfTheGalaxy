using Core.Fights;
using Core.AI;

namespace Core.Actors
{
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
            return HeroType.VALKYRIE;
        }

        public int TeamId { get => teamId; set => teamId = value; }
        public IArtificialIntelligence AI { get => ai; set => ai = value; }
    }
}
