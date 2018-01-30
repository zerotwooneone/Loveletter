using ConsoleApp1.Player;
using ConsoleApp1.Round;

namespace ConsoleApp1.Game
{
    public class GameStartService : IGameStartService
    {
        private readonly IRoundFactory _roundFactory;
        private readonly IRoundStateFactory _roundStateFactory;
        private readonly IPlayerFactory _playerFactory;

        public GameStartService(IRoundFactory roundFactory,
            IRoundStateFactory roundStateFactory,
            IPlayerFactory playerFactory)
        {
            _roundFactory = roundFactory;
            _roundStateFactory = roundStateFactory;
            _playerFactory = playerFactory;
        }
        public IRunningGameState StartGame(IInitialGameState gameState)
        {
            var running = (IRunningGameState)gameState;
            var roundPlayers = _playerFactory.CreateRoundPlayers(gameState.Players);
            var round = _roundFactory.CreateRound(roundPlayers);
            var runningRound = _roundStateFactory.StartRound(round);
            running.RoundState = runningRound;
            return running;
        }
    }
}