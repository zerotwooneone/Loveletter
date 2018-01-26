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
        public IDrawableTurnState CreateTurn(IRoundPlayerState playerState)
        {
            var turnPlayer = _playerFactory.CreateTurnPlayer(playerState);
            var turnState = new TurnState(turnPlayer);
            return turnState;
        }
    }
}