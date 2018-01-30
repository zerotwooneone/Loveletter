using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Player
{
    public interface IDiscardablePlayerState : IPlayerState
    {
        IList<IDiscardableCardState> TurnHand { get; }
        IDiscardedCardState TurnDiscard { get; set; }
    }
}