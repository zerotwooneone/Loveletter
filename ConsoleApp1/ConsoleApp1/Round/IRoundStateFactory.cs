namespace ConsoleApp1.Round
{
    public interface IRoundStateFactory
    {
        IRunningRoundState StartRound(IInitialRoundState round);
    }
}