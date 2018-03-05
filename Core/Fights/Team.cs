namespace Core.Fights
{
    using System.Collections.Generic;

    using Core;
    using Core.Actors;
   
    public partial class Team
    {
        int id;
        int gold;
        List<Entity> entities, lastEntities;

        public Team(int id, int gold = GameSettings.StartingGoldValue)
        {
            this.id = id;
            this.gold = gold;

            this.entities = new List<Entity>();
            this.lastEntities = new List<Entity>();
        }

        public int Id { get => this.id; set => this.id = value; }
        public int Gold { get => this.gold; set => this.gold = value; }
        public List<Entity> Entities { get => this.entities; set => this.entities = value; }
        public List<Entity> LastEntities { get => lastEntities; set => lastEntities = value; }
    }
}
