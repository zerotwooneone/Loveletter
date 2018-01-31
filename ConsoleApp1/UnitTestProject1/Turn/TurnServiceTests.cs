using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Card;
using ConsoleApp1.Player;
using ConsoleApp1.Turn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1.Turn
{
    [TestClass]
    public class TurnServiceTests
    {
        private MockRepository _mockRepository;

        private Mock<ICardStateFactory> _cardStateFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);

            this._cardStateFactory = this._mockRepository.Create<ICardStateFactory>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this._mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Draw_SetsHand()
        {
            // Arrange
            const int deckCardIndex = 0;
            const int deckCardId = 1;
            IDrawableCardState turnDeck = new CardState(deckCardIndex, deckCardId);
            IList<IDiscardedCardState> roundDiscard = null;
            IDiscardableCardState handCard = new CardState(1, 2);
            IList<IDiscardableCardState> turnHand = new List<IDiscardableCardState>(new[] { handCard });
            IDrawablePlayerState drawablePlayerState = new PlayerState(Guid.Empty, roundDiscard, turnHand);
            IDrawableTurnState turn = new TurnState(turnDeck, drawablePlayerState);

            IDiscardableCardState discardableDeckCard = new CardState(deckCardIndex, deckCardId);
            IEnumerable<IDiscardableCardState> expected = new[] { handCard, discardableDeckCard };
            _cardStateFactory
                .Setup(cds => cds.Draw(turnDeck))
                .Returns(discardableDeckCard);

            // Act
            TurnService service = this.CreateService();
            service.Draw(turn);
            var actual = turn.DrawablePlayerState.TurnHand;

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void Discard_SetsDiscard()
        {
            // Arrange
            const int deckCardIndex = 0;
            const int deckCardId = 1;
            IDrawableCardState turnDeck = new CardState(deckCardIndex, deckCardId);
            IList<IDiscardedCardState> roundDiscard = null;
            IDiscardableCardState handCard = new CardState(1, 2);
            IDiscardableCardState discardCard = new CardState(deckCardIndex, deckCardId);
            IList<IDiscardableCardState> turnHand = new List<IDiscardableCardState>(new[] { handCard, discardCard });
            IDiscardablePlayerState discardablePlayerState = new PlayerState(Guid.Empty, roundDiscard, turnHand);
            IDiscardableTurnState turn = new TurnState(turnDeck, discardablePlayer: discardablePlayerState);

            IDiscardedCardState discardedCard= new CardState(deckCardIndex, deckCardId);
            var expected = discardedCard;
            _cardStateFactory
                .Setup(csf => csf.Discard(discardCard))
                .Returns(discardedCard);

            // Act
            TurnService service = this.CreateService();
            service.Discard(turn, discardCard);
            var actual = turn.DiscardablePlayer.TurnDiscard;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Discard_SetsTurnHand()
        {
            // Arrange
            const int deckCardIndex = 0;
            const int deckCardId = 1;
            IDrawableCardState turnDeck = new CardState(deckCardIndex, deckCardId);
            IList<IDiscardedCardState> roundDiscard = null;
            IDiscardableCardState handCard = new CardState(1, 2);
            IDiscardableCardState discardCard = new CardState(deckCardIndex, deckCardId);
            var expected = new[] { handCard };
            IList<IDiscardableCardState> turnHand = new List<IDiscardableCardState>(new[] { handCard, discardCard });
            IDiscardablePlayerState discardablePlayerState = new PlayerState(Guid.Empty, roundDiscard, turnHand);
            IDiscardableTurnState turn = new TurnState(turnDeck, discardablePlayer: discardablePlayerState);

            IDiscardedCardState discardedCard = new CardState(deckCardIndex, deckCardId);
            _cardStateFactory
                .Setup(csf => csf.Discard(discardCard))
                .Returns(discardedCard);
            // Act
            TurnService service = this.CreateService();
            service.Discard(turn, discardCard);
            var actual = turn.DiscardablePlayer.TurnHand;

            // Assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void Discard_SetsTargetPlayer()
        {
            // Arrange
            const int deckCardIndex = 0;
            const int deckCardId = 1;
            IDrawableCardState turnDeck = new CardState(deckCardIndex, deckCardId);
            IList<IDiscardedCardState> roundDiscard = null;
            IDiscardableCardState handCard = new CardState(1, 2);
            IDiscardableCardState discardCard = new CardState(deckCardIndex, deckCardId);
            IList<IDiscardableCardState> turnHand = new List<IDiscardableCardState>(new[] { handCard, discardCard });
            IDiscardablePlayerState discardablePlayerState = new PlayerState(Guid.Empty, roundDiscard, turnHand);
            IDiscardableTurnState turn = new TurnState(turnDeck, discardablePlayer: discardablePlayerState);
            IList<IDiscardableCardState> targetHand = null;
            IList<IDiscardedCardState> targetDiscard = null;
            ITargetablePlayerState targetPlayer = new PlayerState(Guid.Parse("076d24f5-c5a1-48b8-be65-7afdd3f64130"), targetDiscard, targetHand);
            var expected = targetPlayer;

            IDiscardedCardState discardedCard = new CardState(deckCardIndex, deckCardId);
            _cardStateFactory
                .Setup(csf => csf.Discard(discardCard))
                .Returns(discardedCard);
            // Act
            TurnService service = this.CreateService();
            service.Discard(turn, discardCard, targetPlayer);
            var actual = turn.TargetPlayer;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private TurnService CreateService()
        {
            return new TurnService(this._cardStateFactory.Object);
        }
    }
}
