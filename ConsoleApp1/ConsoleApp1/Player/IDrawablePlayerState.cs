using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Player
{
    public interface IDrawablePlayerState : IPlayerState
    {
        IList<IDiscardableCardState> TurnHand { get; }
    }
}