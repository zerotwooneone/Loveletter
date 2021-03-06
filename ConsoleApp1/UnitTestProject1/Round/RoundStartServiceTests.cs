using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Card;
using ConsoleApp1.Deck;
using ConsoleApp1.Player;
using ConsoleApp1.Round;
using ConsoleApp1.Turn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1.Round
{
    [TestClass]
    public class RoundStartServiceTests
    {
        private MockRepository _mockRepository;

        private Mock<IDeckShuffleService> _deckShuffleService;
        private Mock<IRoundFactory> _roundFactory;
        private Mock<ITurnStateFactory> _turnStateFactory;
        private Mock<IDeckRemovalService> _deckRemovalService;
        private Mock<ICardStateFactory> _cardDrawService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _deckShuffleService = _mockRepository.Create<IDeckShuffleService>();
            _roundFactory = _mockRepository.Create<IRoundFactory>();
            _turnStateFactory = _mockRepository.Create<ITurnStateFactory>();
            _deckRemovalService = _mockRepository.Create<IDeckRemovalService>();
            _cardDrawService = _mockRepository.Create<ICardStateFactory>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void StartRound_RemainingPlayerCountIs2()
        {
            // Arrange
            var player1 = new PlayerState(Guid.Empty, null, null);
            IEnumerable<IRoundPlayerState> players = new[] { player1, new PlayerState(Guid.Empty, null, null) };
            var expected = players.Count();
            IShufflableCardState shufflableCard = new CardState(0, 0);
            IEnumerable<IShufflableCardState> shuffleableDeck = new[] { shufflableCard };
            IInitialRoundState initialRound = new RoundState(players, null, null, null, shufflableDeck: shuffleableDeck);

            var remainingPlayers = new List<IRoundPlayerState>();
            _roundFactory
                .Setup(rf => rf.CreateRemainingPlayers())
                .Returns(remainingPlayers);
            var removedFromRound = new List<ISetAsideCardState>();
            _roundFactory
                .Setup(rf => rf.CreateRemovedFromRound())
                .Returns(removedFromRound);

            IDrawableCardState drawableCardState = new CardState(0, 0);
            const int cardSetAsideCount = 1;
            const int turnDeckCount = 1;
            int drawableCardCount = players.Count() + cardSetAsideCount + turnDeckCount;
            var drawableDeck = Enumerable.Repeat(drawableCardState, drawableCardCount);
            _deckShuffleService
                .Setup(dss => dss.Shuffle(shuffleableDeck))
                .Returns(drawableDeck);

            IDiscardableCardState discardableCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.Draw(drawableCardState))
                .Returns(discardableCardState);
            ISetAsideCardState setAsideCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.SetAside(drawableCardState))
                .Returns(setAsideCardState);

            _deckRemovalService
                .Setup(drs => drs.GetCardsToRemoveCount(players.Count()))
                .Returns(cardSetAsideCount);

            IDrawableTurnState drawableTurnState = new TurnState(drawableCardState, player1);
            _turnStateFactory
                .Setup(tsf => tsf.CreateTurn(player1, drawableCardState))
                .Returns(drawableTurnState);

            // Act
            RoundStateFactory service = CreateService();
            var round = service.StartRound(initialRound);
            var actual = round.RemainingPlayers.Count;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StartRound_AllPlayersHaveHands()
        {
            // Arrange
            var player1 = new PlayerState(Guid.Empty, null, null);
            IEnumerable<IRoundPlayerState> players = new[] { player1, new PlayerState(Guid.Empty, null, null) };
            IShufflableCardState shufflableCard = new CardState(0, 0);
            IEnumerable<IShufflableCardState> shuffleableDeck = new[] { shufflableCard };
            IInitialRoundState initialRound = new RoundState(players, null, null, null, shufflableDeck: shuffleableDeck);

            var remainingPlayers = new List<IRoundPlayerState>();
            _roundFactory
                .Setup(rf => rf.CreateRemainingPlayers())
                .Returns(remainingPlayers);
            var removedFromRound = new List<ISetAsideCardState>();
            _roundFactory
                .Setup(rf => rf.CreateRemovedFromRound())
                .Returns(removedFromRound);

            IDrawableCardState drawableCardState = new CardState(0, 0);
            const int cardSetAsideCount = 1;
            const int turnDeckCount = 1;
            int drawableCardCount = players.Count() + cardSetAsideCount + turnDeckCount;
            var drawableDeck = Enumerable.Repeat(drawableCardState, drawableCardCount);
            _deckShuffleService
                .Setup(dss => dss.Shuffle(shuffleableDeck))
                .Returns(drawableDeck);

            IDiscardableCardState discardableCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.Draw(drawableCardState))
                .Returns(discardableCardState);
            ISetAsideCardState setAsideCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.SetAside(drawableCardState))
                .Returns(setAsideCardState);

            _deckRemovalService
                .Setup(drs => drs.GetCardsToRemoveCount(players.Count()))
                .Returns(cardSetAsideCount);

            IDrawableTurnState drawableTurnState = new TurnState(drawableCardState, player1);
            _turnStateFactory
                .Setup(tsf => tsf.CreateTurn(player1, drawableCardState))
                .Returns(drawableTurnState);

            // Act
            RoundStateFactory service = CreateService();
            var round = service.StartRound(initialRound);

            // Assert
            Assert.IsTrue(round.RemainingPlayers.All(p => p.RoundHand != null));
        }

        [TestMethod]
        public void StartRound_AllPlayersInRound()
        {
            // Arrange
            var player1 = new PlayerState(Guid.Empty, null, null, outOfRound: true);
            IEnumerable<IRoundPlayerState> players = new[] { player1, new PlayerState(Guid.Empty, null, null) };
            IShufflableCardState shufflableCard = new CardState(0, 0);
            IEnumerable<IShufflableCardState> shuffleableDeck = new[] { shufflableCard };
            IInitialRoundState initialRound = new RoundState(players, null, null, null, shufflableDeck: shuffleableDeck);

            var remainingPlayers = new List<IRoundPlayerState>();
            _roundFactory
                .Setup(rf => rf.CreateRemainingPlayers())
                .Returns(remainingPlayers);
            var removedFromRound = new List<ISetAsideCardState>();
            _roundFactory
                .Setup(rf => rf.CreateRemovedFromRound())
                .Returns(removedFromRound);

            IDrawableCardState drawableCardState = new CardState(0, 0);
            const int cardSetAsideCount = 1;
            const int turnDeckCount = 1;
            int drawableCardCount = players.Count() + cardSetAsideCount + turnDeckCount;
            var drawableDeck = Enumerable.Repeat(drawableCardState, drawableCardCount);
            _deckShuffleService
                .Setup(dss => dss.Shuffle(shuffleableDeck))
                .Returns(drawableDeck);

            IDiscardableCardState discardableCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.Draw(drawableCardState))
                .Returns(discardableCardState);
            ISetAsideCardState setAsideCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.SetAside(drawableCardState))
                .Returns(setAsideCardState);

            _deckRemovalService
                .Setup(drs => drs.GetCardsToRemoveCount(players.Count()))
                .Returns(cardSetAsideCount);

            IDrawableTurnState drawableTurnState = new TurnState(drawableCardState, player1);
            _turnStateFactory
                .Setup(tsf => tsf.CreateTurn(player1, drawableCardState))
                .Returns(drawableTurnState);

            // Act
            RoundStateFactory service = CreateService();
            var round = service.StartRound(initialRound);

            // Assert
            Assert.IsTrue(round.RemainingPlayers.All(p => p.OutOfRound == false));
        }

        [TestMethod]
        public void StartRound_DrawDeckHasOneCard()
        {
            // Arrange
            var player1 = new PlayerState(Guid.Empty, null, null, outOfRound: true);
            IEnumerable<IRoundPlayerState> players = new[] { player1, new PlayerState(Guid.Empty, null, null) };
            IShufflableCardState shufflableCard = new CardState(0, 0);
            IEnumerable<IShufflableCardState> shuffleableDeck = new[] { shufflableCard };
            IInitialRoundState initialRound = new RoundState(players, null, null, null, shufflableDeck: shuffleableDeck);

            var remainingPlayers = new List<IRoundPlayerState>();
            _roundFactory
                .Setup(rf => rf.CreateRemainingPlayers())
                .Returns(remainingPlayers);
            var removedFromRound = new List<ISetAsideCardState>();
            _roundFactory
                .Setup(rf => rf.CreateRemovedFromRound())
                .Returns(removedFromRound);

            IDrawableCardState drawableCardState = new CardState(0, 0);
            const int cardSetAsideCount = 1;
            var expected = new IDrawableCardState[] { new CardState(99, 108) };
            int drawableCardCount = players.Count() + cardSetAsideCount;
            IDrawableCardState turnDeck = new CardState(88, 112);
            var drawableDeck = expected.Concat(new[] { turnDeck }).Concat(Enumerable.Repeat(drawableCardState, drawableCardCount));
            _deckShuffleService
                .Setup(dss => dss.Shuffle(shuffleableDeck))
                .Returns(drawableDeck);

            IDiscardableCardState discardableCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.Draw(drawableCardState))
                .Returns(discardableCardState);
            ISetAsideCardState setAsideCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.SetAside(drawableCardState))
                .Returns(setAsideCardState);

            _deckRemovalService
                .Setup(drs => drs.GetCardsToRemoveCount(players.Count()))
                .Returns(cardSetAsideCount);

            IDrawableTurnState drawableTurnState = new TurnState(turnDeck, player1);
            _turnStateFactory
                .Setup(tsf => tsf.CreateTurn(player1, turnDeck))
                .Returns(drawableTurnState);

            // Act
            RoundStateFactory service = CreateService();
            var round = service.StartRound(initialRound);
            var actual = round.DrawDeck;

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void StartRound_CurrentPlayerSet()
        {
            // Arrange
            var player1 = new PlayerState(Guid.Empty, null, null, outOfRound: true);
            var expected = player1;
            IEnumerable<IRoundPlayerState> players = new[] { player1, new PlayerState(Guid.Empty, null, null) };
            IShufflableCardState shufflableCard = new CardState(0, 0);
            IEnumerable<IShufflableCardState> shuffleableDeck = new[] { shufflableCard };
            IInitialRoundState initialRound = new RoundState(players, null, null, null, shufflableDeck: shuffleableDeck);

            var remainingPlayers = new List<IRoundPlayerState>();
            _roundFactory
                .Setup(rf => rf.CreateRemainingPlayers())
                .Returns(remainingPlayers);
            var removedFromRound = new List<ISetAsideCardState>();
            _roundFactory
                .Setup(rf => rf.CreateRemovedFromRound())
                .Returns(removedFromRound);

            IDrawableCardState drawableCardState = new CardState(0, 0);
            const int cardSetAsideCount = 1;
            const int turnDeckCount = 1;
            int drawableCardCount = players.Count() + cardSetAsideCount + turnDeckCount;
            var drawableDeck = Enumerable.Repeat(drawableCardState, drawableCardCount);
            _deckShuffleService
                .Setup(dss => dss.Shuffle(shuffleableDeck))
                .Returns(drawableDeck);

            IDiscardableCardState discardableCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.Draw(drawableCardState))
                .Returns(discardableCardState);
            ISetAsideCardState setAsideCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.SetAside(drawableCardState))
                .Returns(setAsideCardState);

            _deckRemovalService
                .Setup(drs => drs.GetCardsToRemoveCount(players.Count()))
                .Returns(cardSetAsideCount);

            IDrawableTurnState drawableTurnState = new TurnState(drawableCardState, player1);
            _turnStateFactory
                .Setup(tsf => tsf.CreateTurn(player1, drawableCardState))
                .Returns(drawableTurnState);

            // Act
            RoundStateFactory service = CreateService();
            var round = service.StartRound(initialRound);
            var actual = round.CurrentPlayerState;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StartRound_DiscardableStateNull()
        {
            // Arrange
            var player1 = new PlayerState(Guid.Empty, null, null, outOfRound: true);
            IDiscardableTurnState expected = null;
            IEnumerable<IRoundPlayerState> players = new[] { player1, new PlayerState(Guid.Empty, null, null) };
            IShufflableCardState shufflableCard = new CardState(0, 0);
            IEnumerable<IShufflableCardState> shuffleableDeck = new[] { shufflableCard };
            IInitialRoundState initialRound = new RoundState(players, null, null, null, shufflableDeck: shuffleableDeck);

            var remainingPlayers = new List<IRoundPlayerState>();
            _roundFactory
                .Setup(rf => rf.CreateRemainingPlayers())
                .Returns(remainingPlayers);
            var removedFromRound = new List<ISetAsideCardState>();
            _roundFactory
                .Setup(rf => rf.CreateRemovedFromRound())
                .Returns(removedFromRound);

            IDrawableCardState drawableCardState = new CardState(0, 0);
            const int cardSetAsideCount = 1;
            const int turnDeckCount = 1;
            int drawableCardCount = players.Count() + cardSetAsideCount + turnDeckCount;
            var drawableDeck = Enumerable.Repeat(drawableCardState, drawableCardCount);
            _deckShuffleService
                .Setup(dss => dss.Shuffle(shuffleableDeck))
                .Returns(drawableDeck);

            IDiscardableCardState discardableCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.Draw(drawableCardState))
                .Returns(discardableCardState);
            ISetAsideCardState setAsideCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.SetAside(drawableCardState))
                .Returns(setAsideCardState);

            _deckRemovalService
                .Setup(drs => drs.GetCardsToRemoveCount(players.Count()))
                .Returns(cardSetAsideCount);

            IDrawableTurnState drawableTurnState = new TurnState(drawableCardState, player1);
            _turnStateFactory
                .Setup(tsf => tsf.CreateTurn(player1, drawableCardState))
                .Returns(drawableTurnState);

            // Act
            RoundStateFactory service = CreateService();
            var round = service.StartRound(initialRound);
            var actual = round.DiscardableTurnState;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StartRound_DrawableStateSet()
        {
            // Arrange
            var player1 = new PlayerState(Guid.Empty, null, null, outOfRound: true);
            IEnumerable<IRoundPlayerState> players = new[] { player1, new PlayerState(Guid.Empty, null, null) };
            IShufflableCardState shufflableCard = new CardState(0, 0);
            IEnumerable<IShufflableCardState> shuffleableDeck = new[] { shufflableCard };
            IInitialRoundState initialRound = new RoundState(players, null, null, null, shufflableDeck: shuffleableDeck);

            var remainingPlayers = new List<IRoundPlayerState>();
            _roundFactory
                .Setup(rf => rf.CreateRemainingPlayers())
                .Returns(remainingPlayers);
            var removedFromRound = new List<ISetAsideCardState>();
            _roundFactory
                .Setup(rf => rf.CreateRemovedFromRound())
                .Returns(removedFromRound);

            IDrawableCardState drawableCardState = new CardState(0, 0);
            const int cardSetAsideCount = 1;
            const int turnDeckCount = 1;
            int drawableCardCount = players.Count() + cardSetAsideCount + turnDeckCount;
            var drawableDeck = Enumerable.Repeat(drawableCardState, drawableCardCount);
            _deckShuffleService
                .Setup(dss => dss.Shuffle(shuffleableDeck))
                .Returns(drawableDeck);

            IDiscardableCardState discardableCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.Draw(drawableCardState))
                .Returns(discardableCardState);
            ISetAsideCardState setAsideCardState = new CardState(0, 0);
            _cardDrawService
                .Setup(cds => cds.SetAside(drawableCardState))
                .Returns(setAsideCardState);

            _deckRemovalService
                .Setup(drs => drs.GetCardsToRemoveCount(players.Count()))
                .Returns(cardSetAsideCount);

            IDrawableTurnState drawableTurnState = new TurnState(drawableCardState, player1);
            var expected = drawableTurnState;
            _turnStateFactory
                .Setup(tsf => tsf.CreateTurn(player1, drawableCardState))
                .Returns(drawableTurnState);

            // Act
            RoundStateFactory service = CreateService();
            var round = service.StartRound(initialRound);
            var actual = round.DrawableTurnState;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private RoundStateFactory CreateService()
        {
            return new RoundStateFactory(
                _deckShuffleService.Object,
                _roundFactory.Object,
                _turnStateFactory.Object,
                _deckRemovalService.Object,
                _cardDrawService.Object);
        }
    }
}
