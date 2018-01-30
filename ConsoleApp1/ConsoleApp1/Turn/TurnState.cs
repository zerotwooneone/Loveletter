using ConsoleApp1.Card;
using ConsoleApp1.Player;

namespace ConsoleApp1.Turn
{
    public class TurnState: IDrawableTurnState, IDiscardableTurnState
    {
        public IDrawablePlayerState DrawablePlayerState { get; }
        public IDrawableCardState TurnDeck { get; set; }
        public IDiscardablePlayerState DiscardablePlayer { get; }
        public ITargetablePlayerState TargetPlayer { get; set; }

        public TurnState(IDrawableCardState turnDeck, 
            IDrawablePlayerState drawablePlayerState=null, 
            IDiscardablePlayerState discardablePlayer=null, 
            ITargetablePlayerState targetPlayer=null)
        {
            TurnDeck = turnDeck;
            DrawablePlayerState = drawablePlayerState;
            DiscardablePlayer = discardablePlayer;
            TargetPlayer = targetPlayer;
        }
    }
}
