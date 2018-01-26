using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Player
{
    public interface IRoundPlayerState: IPlayerState
    {
        bool OutOfRound { get; set; }
        IDiscardableCardState RoundHand { get; set; }
        IList<IDiscardedCardState> RoundDiscard { get; }
    }
}