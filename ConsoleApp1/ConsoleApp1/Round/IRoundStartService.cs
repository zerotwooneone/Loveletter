namespace ConsoleApp1.Round
{
    public interface IRoundStartService
    {
        IRunningRoundState StartRound(IInitialRoundState round);
    }
}