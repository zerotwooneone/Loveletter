using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Card;
using ConsoleApp1.Deck;
using ConsoleApp1.Player;
using ConsoleApp1.Round;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1.Round
{
    [TestClass]
    public class RoundFactoryTests
    {
        private MockRepository _mockRepository;

        private Mock<IDeckFactory> _deckFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);

            this._deckFactory = this._mockRepository.Create<IDeckFactory>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this._mockRepository.VerifyAll();
        }

        [TestMethod]
        public void CreateRound_SetsTwoPlayers()
        {
            // Arrange
            IEnumerable<IRoundPlayer> players = new[] {
                new Player(Guid.Parse("b0049b58-73a3-49a8-8ba1-f5d3fd9701a9"), null, null),
                new Player(Guid.Parse("a7b9794d-a3f0-46ac-b69b-a2dbc3e12f18"), null, null) };
            var expected = players;

            IEnumerable<IShufflableCardState> shufflalbeDeck = new[] { new CardState(0, 1) };
            _deckFactory
                .Setup(df => df.Create())
                .Returns(shufflalbeDeck);

            // Act
            RoundFactory factory = this.CreateFactory();
            var round = factory.CreateRound(players);
            var actual = round.Players;

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void CreateRound_SetsShuffleDeck()
        {
            // Arrange
            IEnumerable<IRoundPlayer> players = new[] {
                new Player(Guid.Parse("b0049b58-73a3-49a8-8ba1-f5d3fd9701a9"), null, null),
                new Player(Guid.Parse("a7b9794d-a3f0-46ac-b69b-a2dbc3e12f18"), null, null) };

            IEnumerable<IShufflableCardState> shufflalbeDeck = new[] { new CardState(0, 1) };
            var expected = shufflalbeDeck;
            _deckFactory
                .Setup(df => df.Create())
                .Returns(shufflalbeDeck);

            // Act
            RoundFactory factory = this.CreateFactory();
            var round = factory.CreateRound(players);
            var actual = round.ShufflableDeck;

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void CreateRound_ZeroRoundIndex()
        {
            // Arrange
            IEnumerable<IRoundPlayer> players = new[] {
                new Player(Guid.Parse("b0049b58-73a3-49a8-8ba1-f5d3fd9701a9"), null, null),
                new Player(Guid.Parse("a7b9794d-a3f0-46ac-b69b-a2dbc3e12f18"), null, null) };

            IEnumerable<IShufflableCardState> shufflalbeDeck = new[] { new CardState(0, 1) };
            const int expected = 0;
            _deckFactory
                .Setup(df => df.Create())
                .Returns(shufflalbeDeck);

            // Act
            RoundFactory factory = this.CreateFactory();
            var round = factory.CreateRound(players);
            var actual = round.RoundIndex;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        private RoundFactory CreateFactory()
        {
            return new RoundFactory(
                this._deckFactory.Object);
        }
    }
}
