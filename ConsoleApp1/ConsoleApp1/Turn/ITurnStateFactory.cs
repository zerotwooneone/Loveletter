using ConsoleApp1.Player;

namespace ConsoleApp1.Turn
{
    public interface ITurnStateFactory
    {
        IDrawableTurnState CreateTurn(IRoundPlayerState playerState);
    }
}