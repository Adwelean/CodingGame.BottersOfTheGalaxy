namespace Core.Fights
{
    using System.Collections.Generic;

    using Core;
    using Core.Actors;
   
    public class Team
    {
        int id;
        int gold;
        List<EntityBase> entities;

        public Team(int id, int gold = GameSettings.StartingGoldValue)
        {
            this.id = id;
            this.gold = gold;

            this.entities = new List<EntityBase>();
        }

        public int Id { get => this.id; set => this.id = value; }
        public int Gold { get => this.gold; set => this.gold = value; }
        public List<EntityBase> Entities { get => this.entities; set => this.entities = value; }
    }
}
