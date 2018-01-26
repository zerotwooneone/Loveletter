using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Card;
using ConsoleApp1.Player;
using ConsoleApp1.Turn;

namespace ConsoleApp1.Round
{
    public class RoundState : IInitialRoundState,
        IRunningRoundState,
        ICompletedRoundState
    {
        public int RoundIndex { get; } //
        public IEnumerable<IRoundPlayerState> Players { get; } //
        public IEnumerable<IShufflableCardState> ShufflableDeck { get; }
        public IList<ISetAsideCardState> RemovedFromRound { get; }
        public IRoundPlayerState WinningPlayerState { get; set; } //
        public IList<IRoundPlayerState> RemainingPlayers { get; } //
        public IList<IDrawableCardState> DrawDeck { get; }
        public IRoundPlayerState CurrentPlayerState { get; set; }
        public IDrawableTurnState DrawableTurnState { get; set; }
        public IDiscardableTurnState DiscardableTurnState { get; set; }

        public RoundState(IEnumerable<IRoundPlayerState> players,
            IList<IRoundPlayerState> remainingPlayers,
            IList<ISetAsideCardState> removedFromRound,
            IList<IDrawableCardState> drawDeck,
            int roundIndex = 0,
            IRoundPlayerState winningPlayerState = null,
            IRoundPlayerState currentPlayerState = null,
            IDrawableTurnState drawableTurnState = null,
            IDiscardableTurnState discardableTurnState = null,
            IEnumerable<IShufflableCardState> shufflableDeck = null)
        {
            RoundIndex = roundIndex;
            var roundPlayers = players as IRoundPlayerState[] ?? players.ToArray();
            Players = roundPlayers;
            RemainingPlayers = remainingPlayers;
            WinningPlayerState = winningPlayerState;
            CurrentPlayerState = currentPlayerState;
            DrawableTurnState = drawableTurnState;
            DiscardableTurnState = discardableTurnState;
            ShufflableDeck = shufflableDeck;
            RemovedFromRound = removedFromRound;
            DrawDeck = drawDeck;
        }
    }
}