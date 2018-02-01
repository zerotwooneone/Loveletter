using ConsoleApp1.Card;
using ConsoleApp1.Player;

namespace ConsoleApp1.Round
{
    public interface IRoundService
    {
        void Draw(IRunningRoundState round);
        void Discard(IRunningRoundState round, IDiscardableCardState card, ITargetablePlayerState targetPlayer = null);
    }
}