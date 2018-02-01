using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Card;
using ConsoleApp1.Player;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1.Card
{
    [TestClass]
    public class CardEffectServiceTests
    {
        private MockRepository _mockRepository;
        private Mock<ICardStateFactory> _cardStateFactory;
        private Mock<ICardRankService> _cardRankService;

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);
            _cardStateFactory = _mockRepository.Create<ICardStateFactory>();
            _cardRankService = _mockRepository.Create<ICardRankService>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this._mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Discard8_OutOfRound()
        {
            // Arrange
            IRoundPlayerState player = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, outOfRound: false);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard8(player);

            // Assert
            Assert.IsTrue(player.OutOfRound);
        }

        [TestMethod]
        public void Discard6_TakesTargetHand()
        {
            // Arrange
            IDiscardableCardState roundHand = new CardState(0, 1);
            IRoundPlayerState player = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: roundHand);
            IDiscardableCardState targetRoundHand = new CardState(1, 2);
            var expected = targetRoundHand;
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: targetRoundHand);
            // Act
            CardEffectService service = this.CreateService();

            service.Discard6(player, targetPlayer);
            var actual = player.RoundHand;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Discard6_GivesTargetHand()
        {
            // Arrange
            IDiscardableCardState roundHand = new CardState(0, 1);
            var expected = roundHand;
            IRoundPlayerState player = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: roundHand);
            IDiscardableCardState targetRoundHand = new CardState(1, 2);
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: targetRoundHand);
            // Act
            CardEffectService service = this.CreateService();
            service.Discard6(player, targetPlayer);
            var actual = targetPlayer.RoundHand;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Discard5_TargetDiscards()
        {
            // Arrange
            IDiscardableCardState roundHand = new CardState(1, 2);
            IList<IDiscardedCardState> roundDiscard = new List<IDiscardedCardState>();
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard, turnHand: null, roundHand: roundHand);
            IDrawableCardState deckCard = new CardState(0, 1);
            IList<IDrawableCardState> deck = new List<IDrawableCardState>(new[] { deckCard });
            IList<ISetAsideCardState> setAside = null;

            IDiscardableCardState discardableDeckCard=new CardState(2,3);
            _cardStateFactory
                .Setup(csf => csf.Draw(deckCard))
                .Returns(discardableDeckCard);
            IDiscardedCardState discardedRoundCard=new CardState(3,4);
            var expected = discardedRoundCard;
            _cardStateFactory
                .Setup(csf => csf.Discard(roundHand))
                .Returns(discardedRoundCard);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard5(targetPlayer, deck, setAside);
            var actual = targetPlayer.RoundDiscard.First();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Discard5_TargetDraws()
        {
            // Arrange
            IDiscardableCardState roundHand = new CardState(1, 2);
            IList<IDiscardedCardState> roundDiscard = new List<IDiscardedCardState>();
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard, turnHand: null, roundHand: roundHand);
            IDrawableCardState deckCard = new CardState(0, 1);
            IList<IDrawableCardState> deck = new List<IDrawableCardState>(new[] { deckCard });
            IList<ISetAsideCardState> setAside = null;

            IDiscardableCardState discardableDeckCard = new CardState(2, 3);
            _cardStateFactory
                .Setup(csf => csf.Draw(deckCard))
                .Returns(discardableDeckCard);
            IDiscardedCardState discardedRoundCard = new CardState(3, 4);
            _cardStateFactory
                .Setup(csf => csf.Discard(roundHand))
                .Returns(discardedRoundCard);

            IDiscardableCardState dicardableDeckCard = new CardState(3, 4);
            var expected = dicardableDeckCard;
            _cardStateFactory
                .Setup(csf => csf.Draw(deckCard))
                .Returns(dicardableDeckCard);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard5(targetPlayer, deck, setAside);
            var actual = targetPlayer.RoundHand;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Discard5_TargetDrawsSetAside()
        {
            // Arrange
            IDiscardableCardState roundHand = new CardState(1, 2);
            IList<IDiscardedCardState> roundDiscard = new List<IDiscardedCardState>();
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard, turnHand: null, roundHand: roundHand);
            ISetAsideCardState setAsideCard = new CardState(0, 1);
            IList<IDrawableCardState> deck = new List<IDrawableCardState>();
            IList<ISetAsideCardState> setAside = new List<ISetAsideCardState>(new[] { setAsideCard });

            IDiscardableCardState discardableDeckCard = new CardState(2, 3);
            _cardStateFactory
                .Setup(csf => csf.Draw(setAsideCard))
                .Returns(discardableDeckCard);
            IDiscardedCardState discardedRoundCard = new CardState(3, 4);
            _cardStateFactory
                .Setup(csf => csf.Discard(roundHand))
                .Returns(discardedRoundCard);

            IDiscardableCardState dicardableSetAsideCard = new CardState(3, 4);
            var expected = dicardableSetAsideCard;
            _cardStateFactory
                .Setup(csf => csf.Draw(setAsideCard))
                .Returns(dicardableSetAsideCard);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard5(targetPlayer, deck, setAside);
            var actual = targetPlayer.RoundHand;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Discard4_Protected()
        {
            // Arrange
            IRoundPlayerState player = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, isProtected: false);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard4(player);
            var actual = player.Protected;

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Discard3_PlayerOutOfRound()
        {
            // Arrange
            IDiscardableCardState roundHand = new CardState(0, 1);
            IRoundPlayerState player = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: roundHand, outOfRound: false);
            IDiscardableCardState targetRoundHand = new CardState(1, 2);
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: targetRoundHand, outOfRound: false);

            _cardRankService
                .Setup(crs => crs.Compare(roundHand, targetRoundHand))
                .Returns(false);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard3(player, targetPlayer);
            var actual = player.OutOfRound;

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Discard3_TargetInRound()
        {
            // Arrange
            IDiscardableCardState roundHand = new CardState(0, 1);
            IRoundPlayerState player = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: roundHand, outOfRound: false);
            IDiscardableCardState targetRoundHand = new CardState(1, 2);
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: targetRoundHand, outOfRound: false);

            _cardRankService
                .Setup(crs => crs.Compare(roundHand, targetRoundHand))
                .Returns(false);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard3(player, targetPlayer);
            var actual = targetPlayer.OutOfRound;

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Discard3_TargetOutOfRound()
        {
            // Arrange
            IDiscardableCardState roundHand = new CardState(0, 1);
            IRoundPlayerState player = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: roundHand, outOfRound: false);
            IDiscardableCardState targetRoundHand = new CardState(1, 2);
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: targetRoundHand, outOfRound: false);

            _cardRankService
                .Setup(crs => crs.Compare(roundHand, targetRoundHand))
                .Returns(true);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard3(player, targetPlayer);
            var actual = targetPlayer.OutOfRound;

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Discard3_PlayerInRound()
        {
            // Arrange
            IDiscardableCardState roundHand = new CardState(0, 1);
            IRoundPlayerState player = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: roundHand, outOfRound: false);
            IDiscardableCardState targetRoundHand = new CardState(1, 2);
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: targetRoundHand, outOfRound: false);

            _cardRankService
                .Setup(crs => crs.Compare(roundHand, targetRoundHand))
                .Returns(true);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard3(player, targetPlayer);
            var actual = player.OutOfRound;

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void Discard1_TargetOutOfRound()
        {
            // Arrange
            IDiscardableCardState targetRoundHand = new CardState(1, 2);
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: targetRoundHand, outOfRound: false);

            int guessedRank = 0;
            _cardRankService
                .Setup(crs => crs.Matches(targetRoundHand, guessedRank))
                .Returns(true);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard1(targetPlayer, guessedRank);
            var actual = targetPlayer.OutOfRound;

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Discard1_TargetInRound()
        {
            // Arrange
            IDiscardableCardState targetRoundHand = new CardState(1, 2);
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Empty, roundDiscard: null, turnHand: null, roundHand: targetRoundHand, outOfRound: false);

            int guessedRank = 0;
            _cardRankService
                .Setup(crs => crs.Matches(targetRoundHand, guessedRank))
                .Returns(false);

            // Act
            CardEffectService service = this.CreateService();
            service.Discard1(targetPlayer, guessedRank);
            var actual = targetPlayer.OutOfRound;

            // Assert
            Assert.IsFalse(actual);
        }

        private CardEffectService CreateService()
        {
            return new CardEffectService(_cardStateFactory.Object,
                _cardRankService.Object);
        }
    }
}
