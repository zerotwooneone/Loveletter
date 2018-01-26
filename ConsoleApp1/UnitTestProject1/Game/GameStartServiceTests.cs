using System;
using System.Collections.Generic;
using ConsoleApp1.Card;
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

        private Mock<IRoundFactory> _RoundFactory;
        private Mock<IRoundStartService> _RoundStartService;
        private readonly Guid _gameId;
        private Mock<IPlayerFactory> _PlayerFactory;

        public GameStartServiceTests()
        {
            _gameId = Guid.Parse("6c90711e-e13f-407b-9ce1-f1dd292e597d");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _RoundFactory = _mockRepository.Create<IRoundFactory>();
            _RoundStartService = _mockRepository.Create<IRoundStartService>();
            _PlayerFactory = _mockRepository.Create<IPlayerFactory>();
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
            var player1Guid = Guid.Parse("115e08ed-510e-4b79-acbc-a40b2ee40d08");
            var gamePlayer1 = new PlayerState(player1Guid,null,null);
            var players = new Dictionary<Guid, IGamePlayerState>{{player1Guid, gamePlayer1}};
            IRunningRoundState roundState=null;
            var initialGameState = new GameState(_gameId, players, roundState);
            
            var roundPlayer1 = new PlayerState(player1Guid, new List<IDiscardedCardState>(), new List<IDiscardableCardState>());
            _PlayerFactory
                .Setup(pf => pf.CreateRoundPlayer(gamePlayer1))
                .Returns(roundPlayer1);

            var initialRoundState = GetRoundState();

            
            IEnumerable<IRoundPlayerState> roundPlayers=new []
            {
                roundPlayer1,
            };
            _RoundFactory
                .Setup(rf => rf.CreateRound(roundPlayers))
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
        
        public static RoundState GetRoundState()
        {
            var roundPlayers = new IRoundPlayerState[0];
            var remainingPlayers = new List<IRoundPlayerState>();
            var shufflableDeck = new List<IShufflableCardState>();
            var removedFromRound = new List<ISetAsideCardState>();
            var roundDrawDeck = new List<IDrawableCardState>();
            RoundState roundState =
                new RoundState(roundPlayers, remainingPlayers, removedFromRound, roundDrawDeck);
            return roundState;
        }

        private GameStartService CreateService()
        {
            return new GameStartService(
                _RoundFactory.Object,
                _RoundStartService.Object,
                _PlayerFactory.Object);
        }
    }
}
