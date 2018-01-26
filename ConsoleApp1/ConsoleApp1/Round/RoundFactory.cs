using System;
using System.Collections.Generic;
using ConsoleApp1.Card;
using ConsoleApp1.Deck;
using ConsoleApp1.Player;

namespace ConsoleApp1.Round
{
    public class RoundFactory : IRoundFactory
    {
        private readonly IDeckFactory _deckFactory;

        public RoundFactory(IDeckFactory deckFactory)
        {
            _deckFactory = deckFactory;
        }
        public IInitialRoundState CreateRound(IEnumerable<IRoundPlayerState> players)
        {
            IList<IRoundPlayerState> remainingPlayers = null;
            IList<ISetAsideCardState> removedFromRound = CreateRemovedFromRound();
            IList<IDrawableCardState> drawDeck = null;
            IEnumerable<IShufflableCardState> shufflableDeck = _deckFactory.Create();
            var round = new RoundState(players,
                remainingPlayers,
                removedFromRound,
                drawDeck,
                roundIndex: 0,
                winningPlayerState: null,
                currentPlayerState: null,
                drawableTurnState: null,
                discardableTurnState: null,
                shufflableDeck: shufflableDeck);
            return round;
        }

        public IList<IRoundPlayerState> CreateRemainingPlayers()
        {
            return new List<IRoundPlayerState>();
        }

        public IList<ISetAsideCardState> CreateRemovedFromRound()
        {
            return new List<ISetAsideCardState>();
        }
    }
}