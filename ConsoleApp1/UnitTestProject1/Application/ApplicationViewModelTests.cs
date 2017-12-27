using System;
using System.Reactive.Subjects;
using ConsoleApp1.Application;
using ConsoleApp1.Game;
using ConsoleApp1.Lobby;
using ConsoleApp1.Player;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1.Application
{
    [TestClass]
    public class ApplicationViewModelTests
    {
        private MockRepository mockRepository;

        private Mock<ILobbyFactory> mockLobbyFactory;
        private Mock<IGameFactory> mockGameFactory;
        private Mock<ILobbyService> mockLobbyService;
        private Mock<IGameService> mockGameService;
        private Mock<IConsoleService> _consoleService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLobbyFactory = this.mockRepository.Create<ILobbyFactory>();
            this.mockGameFactory = this.mockRepository.Create<IGameFactory>();
            this.mockLobbyService = this.mockRepository.Create<ILobbyService>();
            this.mockGameService = this.mockRepository.Create<IGameService>();
            _consoleService = mockRepository.Create<IConsoleService>();

        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Start_ShouldCreateLobby()
        {
            // Arrange
            ISubject<GameParams> gameSubject = new Subject<GameParams>();
            var playerSubject = new Subject<PlayerParams>();
            var expected = new Lobby(gameSubject, playerSubject);
            mockLobbyFactory
                .Setup(lf => lf.CreateLobby(null))
                .Returns(expected);
            //_consoleService
            //    .Setup(cs => cs.WriteLine(It.IsAny<string>()))
            //    .Verifiable();

            // Act
            ApplicationViewModel viewModel = this.CreateViewModel();
            viewModel.Start();
            var actual = viewModel.Lobby;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Start_ShouldCreateGame()
        {
            // Arrange
            var gameParams = new GameParams();
            ISubject<GameParams> gameSubject = new BehaviorSubject<GameParams>(gameParams);
            var playerSubject = new Subject<PlayerParams>();
            var lobby = new Lobby(gameSubject, playerSubject);
            mockLobbyFactory
                .Setup(lf => lf.CreateLobby(null))
                .Returns(lobby);
            Game expected = new Game(new Subject<LobbyParams>());
            mockGameFactory
                .Setup(gf => gf.CreateGame(gameParams))
                .Returns(expected);
            //_consoleService
            //    .Setup(cs => cs.WriteLine(It.IsAny<string>()))
            //    .Verifiable();
            //_consoleService
            //    .Setup(cs => cs.ReadLine())
            //    .Returns("test");

            // Act
            ApplicationViewModel viewModel = this.CreateViewModel();
            viewModel.Start();
            var actual = viewModel.Game;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Start_ShouldReCreateLobby()
        {
            // Arrange
            var gameParams = new GameParams();
            ISubject<GameParams> gameSubject = new BehaviorSubject<GameParams>(gameParams);
            var playerSubject = new Subject<PlayerParams>();
            var lobby = new Lobby(gameSubject, playerSubject);
            mockLobbyFactory
                .Setup(lf => lf.CreateLobby(null))
                .Returns(lobby);
            var lobbyParams = new LobbyParams();

            ISubject<GameParams> gameSubject2 = new Subject<GameParams>();
            var expected = new Lobby(gameSubject2, playerSubject);
            mockLobbyFactory
                .Setup(lf => lf.CreateLobby(lobbyParams))
                .Returns(expected);
            var lobbySubject = new BehaviorSubject<LobbyParams>(lobbyParams);
            Game game = new Game(lobbySubject);
            mockGameFactory
                .Setup(gf => gf.CreateGame(gameParams))
                .Returns(game);

            // Act
            ApplicationViewModel viewModel = this.CreateViewModel();
            viewModel.Start();
            var actual = viewModel.Lobby;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private ApplicationViewModel CreateViewModel()
        {
            return new ApplicationViewModel(
                this.mockLobbyFactory.Object,
                this.mockGameFactory.Object,
                this.mockLobbyService.Object,
                this.mockGameService.Object,
                _consoleService.Object);
        }
    }
}
