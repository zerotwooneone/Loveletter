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
        public IDrawableTurnState CreateTurn(IRoundPlayer player)
        {
            var turnPlayer = _playerFactory.CreateTurnPlayer(player);
            var hand = player.RoundHand;
            var turnState = new TurnState(turnPlayer, hand);
            return turnState;
        }
    }
}