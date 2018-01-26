using System.Collections.Generic;
using ConsoleApp1.Card;
using ConsoleApp1.Player;
using ConsoleApp1.Turn;

namespace ConsoleApp1.Round
{
    public interface IRunningRoundState
    {
        IList<IRoundPlayerState> RemainingPlayers { get; }
        IList<IDrawableCardState> DrawDeck { get; }
        IList<ISetAsideCardState> RemovedFromRound { get; }
        IRoundPlayerState CurrentPlayerState { get; set; }
        IDrawableTurnState DrawableTurnState { get; set; }
        IDiscardableTurnState DiscardableTurnState { get; set; }
    }
}