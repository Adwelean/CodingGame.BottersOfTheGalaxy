namespace Core.IA
{
    using Core.Fights;

    public interface IArtificialIntelligence
    {
        string ComputeAction(Team currentTeam, Team ennemyTeam);
    }
}
