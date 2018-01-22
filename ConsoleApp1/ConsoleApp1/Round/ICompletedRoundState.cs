using ConsoleApp1.Player;

namespace ConsoleApp1.Round
{
    public interface ICompletedRoundState
    {
        IRoundPlayer WinningPlayer { get; }
    }
}