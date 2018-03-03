namespace Core.AI
{
    using System.Linq;
    using Core.Actors;
    using Core.AI.Commands;
    using Core.Fights;

    public class Rush : IArtificialIntelligence
    {
        public CommandBase ComputeAction(Team playerTeam, Team ennemyTeam, Hero playerHero, CommandBase lastCommand)
        {
            var target = ennemyTeam.Entities.FirstOrDefault(x => x is Tower) as Tower;

            return new Commands.Raw($"MOVE_ATTACK {target.X} {target.Y} {target.Id}");
        }
    }
}
