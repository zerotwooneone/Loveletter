using ConsoleApp1.Card;
using ConsoleApp1.Player;

namespace ConsoleApp1.Turn
{
    public class TurnStateFactory : ITurnStateFactory
    {
        private readonly IPlayerFactory _playerFactory;

        public TurnStateFactory(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }
        public IDrawableTurnState CreateTurn(IRoundPlayerState playerState, IDrawableCardState turnDeck)
        {
            var turnPlayer = _playerFactory.CreateTurnPlayer(playerState);
            IDiscardablePlayerState discardablePlayer = null;
            ITargetablePlayerState targetPlayer = null;
            var turnState = new TurnState(turnDeck, turnPlayer, discardablePlayer, targetPlayer);
            return turnState;
        }

        public IDiscardableTurnState GetDiscardable(IDrawablePlayerState drawablePlayerState)
        {
            var discardablePlayer = _playerFactory.GetDiscardable(drawablePlayerState);
            IDrawablePlayerState unusedPlayerState = null;
            IDrawableCardState turnDeck = null;
            ITargetablePlayerState targetPlayer = null;
            var turnState = new TurnState(turnDeck, unusedPlayerState, discardablePlayer, targetPlayer);
            return turnState;
        }
    }
}