using Core.Fights;
using Core.IA;

namespace Core.Actors
{
    public class Player
    {
        int teamId;

        IArtificialIntelligence ai;

        public Player(int teamId)
        {
            this.teamId = teamId;
            this.ai = new Rush();
        }

        public HeroType ChooseHero(Team team)
        {
            return HeroType.HULK;
        }

        public int TeamId { get => teamId; set => teamId = value; }
        public IArtificialIntelligence AI { get => ai; set => ai = value; }
    }
}
