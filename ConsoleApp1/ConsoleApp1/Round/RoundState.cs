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
        public IEnumerable<IRoundPlayer> Players { get; } //
        public IEnumerable<IShufflableCardState> ShufflableDeck { get; }
        public IList<ISetAsideCardState> RemovedFromRound { get; }
        public IRoundPlayer WinningPlayer { get; set; } //
        public IList<IRoundPlayer> RemainingPlayers { get; } //
        public IList<IDrawableCardState> DrawDeck { get; }
        public IRoundPlayer CurrentPlayer { get; set; }
        public IDrawableTurnState DrawableTurnState { get; set; }
        public IDiscardableTurnState DiscardableTurnState { get; set; }

        public RoundState(IEnumerable<IRoundPlayer> players,
            IList<IRoundPlayer> remainingPlayers,
            IList<ISetAsideCardState> removedFromRound,
            IList<IDrawableCardState> drawDeck,
            int roundIndex = 0,
            IRoundPlayer winningPlayer = null,
            IRoundPlayer currentPlayer = null,
            IDrawableTurnState drawableTurnState = null,
            IDiscardableTurnState discardableTurnState = null,
            IEnumerable<IShufflableCardState> shufflableDeck = null)
        {
            RoundIndex = roundIndex;
            var roundPlayers = players as IRoundPlayer[] ?? players.ToArray();
            Players = roundPlayers;
            RemainingPlayers = remainingPlayers;
            WinningPlayer = winningPlayer;
            CurrentPlayer = currentPlayer;
            DrawableTurnState = drawableTurnState;
            DiscardableTurnState = discardableTurnState;
            ShufflableDeck = shufflableDeck;
            RemovedFromRound = removedFromRound;
            DrawDeck = drawDeck;
        }
    }
}