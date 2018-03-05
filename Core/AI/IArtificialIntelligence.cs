namespace Core.AI
{
    using Core.Actors;
    using Core.AI.Commands;
    using Core.Fights;

    public interface IArtificialIntelligence
    {
        CommandBase ComputeAction(GameContext context);
    }
}
