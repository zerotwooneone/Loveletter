using System;
using System.Linq;
using ConsoleApp1.Card;
using ConsoleApp1.Deck;
using ConsoleApp1.Extensions;
using ConsoleApp1.Turn;

namespace ConsoleApp1.Round
{
    public class RoundStateFactory : IRoundStateFactory
    {
        private readonly IDeckShuffleService _deckShuffleService;
        private readonly IRoundFactory _roundFactory;
        private readonly ITurnStateFactory _turnStateFactory;
        private readonly IDeckRemovalService _deckRemovalService;
        private readonly ICardStateFactory _cardStateFactory;

        public RoundStateFactory(IDeckShuffleService deckShuffleService,
            IRoundFactory roundFactory,
            ITurnStateFactory turnStateFactory,
            IDeckRemovalService deckRemovalService,
            ICardStateFactory cardStateFactory)
        {
            _deckShuffleService = deckShuffleService;
            _roundFactory = roundFactory;
            _turnStateFactory = turnStateFactory;
            _deckRemovalService = deckRemovalService;
            _cardStateFactory = cardStateFactory;
        }

        public IRunningRoundState StartRound(IInitialRoundState round)
        {
            var shuffledDeck = _deckShuffleService.Shuffle(round.ShufflableDeck).ToList();
            var remainingPlayers = _roundFactory.CreateRemainingPlayers();
            var roundPlayers = round.Players.ToArray();
            foreach (var roundPlayer in roundPlayers)
            {
                roundPlayer.OutOfRound = false;
                roundPlayer.RoundHand = _cardStateFactory.Draw(shuffledDeck);
                remainingPlayers.Add(roundPlayer);
            }
            var removedFromRound = _roundFactory.CreateRemovedFromRound();
            var cardsToRemoveCount = _deckRemovalService.GetCardsToRemoveCount(roundPlayers.Count());
            for (int removedCardIndex = 0; removedCardIndex < cardsToRemoveCount; removedCardIndex++)
            {
                var removedCard = _cardStateFactory.SetAside(shuffledDeck);
                removedFromRound.Add(removedCard);
            }
            int roundIndex = round.RoundIndex;
            var currentPlayer = roundPlayers.First();

            var turnDeck = shuffledDeck.RemoveLast();

            var drawableTurnState = _turnStateFactory.CreateTurn(currentPlayer, turnDeck);
            var runningRound = new RoundState(roundPlayers,
                remainingPlayers,
                removedFromRound,
                shuffledDeck,
                roundIndex,
                winningPlayerState: null,
                currentPlayerState: currentPlayer,
                drawableTurnState: drawableTurnState,
                discardableTurnState: null,
                shufflableDeck: null);
            return runningRound;
        }
    }
}