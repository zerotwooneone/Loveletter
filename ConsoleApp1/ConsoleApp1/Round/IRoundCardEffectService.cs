namespace ConsoleApp1.Round
{
    public interface IRoundCardEffectService
    {
        void AfterDraw(IRunningRoundState round);
        void AfterDiscard(IRunningRoundState round);
    }
}