using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Card;
using ConsoleApp1.Deck;
using ConsoleApp1.Game;
using ConsoleApp1.Player;
using ConsoleApp1.Round;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1.Game
{
    [TestClass]
    public class GameStartServiceTests
    {
        private MockRepository _mockRepository;

        private Mock<IDeckFactory> _DeckFactory;
        private Mock<IDeckShuffleService> _DeckShuffleService;
        private Mock<IRoundFactory> _RoundFactory;
        private Mock<IRoundStartService> _RoundStartService;
        private readonly Guid _gameId;

        public GameStartServiceTests()
        {
            _gameId = Guid.Parse("6c90711e-e13f-407b-9ce1-f1dd292e597d");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _DeckFactory = _mockRepository.Create<IDeckFactory>();
            _DeckShuffleService = _mockRepository.Create<IDeckShuffleService>();
            _RoundFactory = _mockRepository.Create<IRoundFactory>();
            _RoundStartService = _mockRepository.Create<IRoundStartService>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockRepository.VerifyAll();
        }

        [TestMethod]
        public void StartGame_SetsRound()
        {
            // Arrange
            var players = new Dictionary<Guid, IGamePlayer>();
            var drawDeck = new List<IDrawableCardState>();
            IRunningRoundState roundState=null;
            var initialGameState = new GameState(_gameId, players, drawDeck, roundState);

            IEnumerable<IShufflableCardState> deckFactoryDeck = null;
            _DeckFactory
                .Setup(df => df.Create())
                .Returns(deckFactoryDeck);

            var shuffledDeck = new List<IDrawableCardState>();
            _DeckShuffleService
                .Setup(dss => dss.Shuffle(deckFactoryDeck))
                .Returns(shuffledDeck);

            var initialRoundState = GetRoundState();
            _RoundFactory
                .Setup(rf => rf.CreateRound())
                .Returns(initialRoundState);

            var runningRoundState = GetRoundState();
            var expected = runningRoundState;
            _RoundStartService
                .Setup(rss => rss.StartRound(initialRoundState))
                .Returns(runningRoundState);

            // Act
            GameStartService service = CreateService();
            var gameState = service.StartGame(initialGameState);
            var actual = gameState.RoundState;

            // Assert
            Assert.AreEqual(actual,expected);
        }

        [TestMethod]
        public void StartGame_ShufflesDeck()
        {
            // Arrange
            var players = new Dictionary<Guid, IGamePlayer>();
            var drawDeck = new List<IDrawableCardState>();
            IRunningRoundState roundState = null;
            var initialGameState = new GameState(_gameId, players, drawDeck, roundState);

            IEnumerable<IShufflableCardState> deckFactoryDeck = null;
            _DeckFactory
                .Setup(df => df.Create())
                .Returns(deckFactoryDeck);

            var shuffledDeck = new List<IDrawableCardState>();
            var expected = shuffledDeck;
            _DeckShuffleService
                .Setup(dss => dss.Shuffle(deckFactoryDeck))
                .Returns(shuffledDeck);

            var initialRoundState = GetRoundState();
            _RoundFactory
                .Setup(rf => rf.CreateRound())
                .Returns(initialRoundState);

            var runningRoundState = GetRoundState();
            _RoundStartService
                .Setup(rss => rss.StartRound(initialRoundState))
                .Returns(runningRoundState);

            // Act
            GameStartService service = CreateService();
            var gameState = service.StartGame(initialGameState);
            var actual = gameState.DrawDeck;

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        public static RoundState GetRoundState()
        {
            var roundPlayers = new IRoundPlayer[0];
            var remainingPlayers = new List<IRoundPlayer>();
            var shufflableDeck = new List<IShufflableCardState>();
            var removedFromGame = new List<IDrawableCardState>();
            var roundDrawDeck = new List<IDrawableCardState>();
            RoundState roundState =
                new RoundState(roundPlayers, remainingPlayers, shufflableDeck, removedFromGame, roundDrawDeck);
            return roundState;
        }

        private GameStartService CreateService()
        {
            return new GameStartService(
                _DeckFactory.Object,
                _DeckShuffleService.Object,
                _RoundFactory.Object,
                _RoundStartService.Object);
        }
    }
}
