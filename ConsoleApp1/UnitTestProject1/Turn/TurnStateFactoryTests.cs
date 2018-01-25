using System;
using System.Collections.Generic;
using ConsoleApp1.Card;
using ConsoleApp1.Player;
using ConsoleApp1.Turn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1.Turn
{
    [TestClass]
    public class TurnStateFactoryTests
    {
        private MockRepository _mockRepository;

        private Mock<IPlayerFactory> _playerFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);

            this._playerFactory = this._mockRepository.Create<IPlayerFactory>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this._mockRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateTurn_SetsPlayer()
        {
            // Arrange
            IList<IDiscardedCardState> roundDiscard = new List<IDiscardedCardState>();
            IDiscardableCardState roundHand = new CardState(0, 0);
            Guid playerId = Guid.Empty;
            IRoundPlayer player = new Player(playerId, roundDiscard, null, outOfRound: false, roundHand: roundHand);

            IList<IDiscardableCardState> turnHand = new[] { roundHand };
            IDiscardedCardState turnDiscard = null;
            ITurnPlayer turnPlayer = new Player(playerId, null, turnHand, turnDiscard: turnDiscard);
            var expected = turnPlayer;
            _playerFactory
                .Setup(pf => pf.CreateTurnPlayer(player))
                .Returns(turnPlayer);

            // Act
            TurnStateFactory factory = this.CreateFactory();

            var turn = factory.CreateTurn(player);
            var actual = turn.Player;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private TurnStateFactory CreateFactory()
        {
            return new TurnStateFactory(
                this._playerFactory.Object);
        }
    }
}
