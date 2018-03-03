namespace Core.AI
{
    using System.Linq;
    using Core.Actors;
    using Core.Fights;

    public class Rush : IArtificialIntelligence
    {
        public string ComputeAction(Team currentTeam, Team ennemyTeam)
        {
            var target = ennemyTeam.Entities.FirstOrDefault(x => x is Tower) as Tower;

            return $"MOVE_ATTACK {target.X} {target.Y} {target.Id}";            
        }
    }
}
