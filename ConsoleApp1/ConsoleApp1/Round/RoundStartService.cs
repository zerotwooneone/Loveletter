using System;
using System.Linq;
using ConsoleApp1.Deck;
using ConsoleApp1.Player;
using ConsoleApp1.Turn;

namespace ConsoleApp1.Round
{
    public class RoundStartService : IRoundStartService
    {
        private readonly IDeckShuffleService _deckShuffleService;
        private readonly IRoundFactory _roundFactory;
        private readonly ITurnStateFactory _turnStateFactory;
        private readonly IDeckRemovalService _deckRemovalService;

        public RoundStartService(IDeckShuffleService deckShuffleService,
            IRoundFactory roundFactory,
            ITurnStateFactory turnStateFactory,
            IDeckRemovalService deckRemovalService)
        {
            _deckShuffleService = deckShuffleService;
            _roundFactory = roundFactory;
            _turnStateFactory = turnStateFactory;
            _deckRemovalService = deckRemovalService;
        }

        public IRunningRoundState StartRound(IInitialRoundState round)
        {
            throw new NotImplementedException();
            var shuffledDeck = _deckShuffleService.Shuffle(round.ShufflableDeck).ToList();
            var remainingPlayers = _roundFactory.CreateRemainingPlayers();
            var roundPlayers = round.Players.ToArray();
            foreach (var roundPlayer in roundPlayers)
            {
                remainingPlayers.Add(roundPlayer);
            }
            var removedFromRound = _roundFactory.CreateRemovedFromRound();
            var toRemove =_deckRemovalService.RemoveFromDeck(shuffledDeck, roundPlayers.Count());
            foreach (var drawableCardState in toRemove)
            {
                removedFromRound.Add(drawableCardState);
            }
            int roundIndex = round.RoundIndex;
            var currentPlayer = roundPlayers.First();
            var drawableTurnState = _turnStateFactory.CreateTurn();
            var runningRound = new RoundState(roundPlayers,
                remainingPlayers,
                removedFromRound,
                shuffledDeck,
                roundIndex,
                winningPlayer: null,
                currentPlayer: currentPlayer,
                drawableTurnState: drawableTurnState,
                discardableTurnState: null,
                shufflableDeck: null);
            return runningRound;
        }
    }
}