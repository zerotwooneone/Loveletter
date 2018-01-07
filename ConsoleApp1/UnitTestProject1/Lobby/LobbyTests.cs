using ConsoleApp1.Lobby;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using ConsoleApp1.Player;

namespace UnitTestProject1.Lobby
{
    [TestClass]
    public class LobbyTests
    {
        private MockRepository mockRepository;

        private ISubject<Player> _playerObservable;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this._playerObservable = new Subject<Player>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public async Task TwoPlayersJoin_Player1IsReady()
        {
            // Arrange
            var player1 = new Player(new BehaviorSubject<bool>(true), Guid.Parse("7a5a0f96-e0b6-423f-a844-72b2e4113982"));
            
            // Act
            ConsoleApp1.Lobby.Lobby lobby = this.CreateLobby();
            bool? actual=null;
            _playerObservable.OnNext(player1);
            
            // Assert
            CollectionAssert.Contains(lobby.ReadyPlayers.ToList(),player1);
        }

        [TestMethod]
        public async Task TwoPlayersJoin_AllPlayersReady()
        {
            // Arrange
            var player1 = new Player(new BehaviorSubject<bool>(true), Guid.Parse("7a5a0f96-e0b6-423f-a844-72b2e4113982"));
            var player2 = new Player(new BehaviorSubject<bool>(true), Guid.Parse("cc8c27bd-eb50-4992-ae2e-587825d36e13"));
            const bool expected = true;

            // Act
            ConsoleApp1.Lobby.Lobby lobby = this.CreateLobby();
            bool? actual = null;
            lobby
                .AllPlayersReady
                .Subscribe(isReady =>
                {
                    actual = isReady;
                });
            _playerObservable.OnNext(player1);
            _playerObservable.OnNext(player2);

            // Assert
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void TwoPlayersJoin_Player2NotReady()
        {
            // Arrange
            var player1 = new Player(new BehaviorSubject<bool>(true), Guid.Parse("7a5a0f96-e0b6-423f-a844-72b2e4113982"));
            var player2Ready = new ReplaySubject<bool>();
            player2Ready.OnNext(true);
            player2Ready.OnNext(false);
            var player2 = new Player(player2Ready, Guid.Parse("cc8c27bd-eb50-4992-ae2e-587825d36e13"));
            const bool expected = false;

            // Act
            ConsoleApp1.Lobby.Lobby lobby = this.CreateLobby();
            bool? actual = null;
            lobby
                .AllPlayersReady
                .Subscribe(isReady =>
                {
                    actual = isReady;
                });
            _playerObservable.OnNext(player1);
            _playerObservable.OnNext(player2);
            

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private ConsoleApp1.Lobby.Lobby CreateLobby()
        {
            return new ConsoleApp1.Lobby.Lobby(
                this._playerObservable,
                Guid.Parse("f6d61255-afc4-4087-9940-088e1ef4498e"));
        }
    }
}
