using ConsoleApp1.Card;
using ConsoleApp1.Player;

namespace ConsoleApp1.Turn
{
    public interface ITurnService
    {
        void Draw(IDrawableTurnState turn);
        void Discard(IDiscardableTurnState turn, 
            IDiscardableCardState card,
            ITargetablePlayerState targetPlayer=null);
    }
}