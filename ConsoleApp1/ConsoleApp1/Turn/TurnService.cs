using ConsoleApp1.Card;
using ConsoleApp1.Player;

namespace ConsoleApp1.Turn
{
    public class TurnService : ITurnService
    {
        private readonly ICardStateFactory _cardStateFactory;
        
        public TurnService(ICardStateFactory cardStateFactory)
        {
            _cardStateFactory = cardStateFactory;
        }

        public void Draw(IDrawableTurnState turn)
        {
            var card = _cardStateFactory.Draw(turn.TurnDeck);
            turn.DrawablePlayerState.TurnHand.Add(card);
        }

        public void Discard(IDiscardableTurnState turn, 
            IDiscardableCardState card,
            ITargetablePlayerState targetPlayer = null)
        {
            turn.DiscardablePlayer.TurnHand.Remove(card);
            var discarded = _cardStateFactory.Discard(card);
            turn.DiscardablePlayer.TurnDiscard = discarded;
            turn.TargetPlayer = targetPlayer;
        }
    }
}