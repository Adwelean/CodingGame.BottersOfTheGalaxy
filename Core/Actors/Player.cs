namespace Core.Actors
{
    public class Player
    {
        int teamId;

        public Player(int teamId)
        {
            this.teamId = teamId;
        }

        public int TeamId { get => teamId; set => teamId = value; }
    }
}
