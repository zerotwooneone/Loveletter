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
            IRoundPlayerState playerState = new PlayerState(playerId, roundDiscard, null, outOfRound: false, roundHand: roundHand);

            IList<IDiscardableCardState> turnHand = new[] { roundHand };
            IDiscardedCardState turnDiscard = null;
            IDrawablePlayerState initialTurnPlayerState = new PlayerState(playerId, null, turnHand, turnDiscard: turnDiscard);
            var expected = initialTurnPlayerState;
            _playerFactory
                .Setup(pf => pf.CreateTurnPlayer(playerState))
                .Returns(initialTurnPlayerState);
            IDrawableCardState turnDeck = new CardState(44, 55);

            // Act
            TurnStateFactory factory = this.CreateFactory();


            var turn = factory.CreateTurn(playerState, turnDeck);
            var actual = turn.DrawablePlayerState;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDiscardable_SetsPlayer()
        {
            //arrange
            Guid playerId = Guid.Empty;

            IList<IDiscardableCardState> turnHand = new[] { new CardState(0, 1), };
            var drawablePlayerState = new PlayerState(playerId, null, turnHand);
            var discardablePlayerState = new PlayerState(playerId, null, turnHand);
            var expected = discardablePlayerState;
            _playerFactory
                .Setup(pf => pf.GetDiscardable(playerId, turnHand))
                .Returns(discardablePlayerState);

            //act
            var factory = CreateFactory();
            var actual = factory.GetDiscardable(drawablePlayerState).DiscardablePlayer;

            //assert
            Assert.AreEqual(expected, actual);
        }

        private TurnStateFactory CreateFactory()
        {
            return new TurnStateFactory(
                this._playerFactory.Object);
        }
    }
}
