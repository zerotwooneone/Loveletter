using ConsoleApp1.Player;

namespace ConsoleApp1.Round
{
    public interface ICompletedRoundState
    {
        IRoundPlayerState WinningPlayerState { get; }
    }
}