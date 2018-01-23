using ConsoleApp1.Player;
using ConsoleApp1.Round;

namespace ConsoleApp1.Game
{
    public class GameStartService : IGameStartService
    {
        private readonly IRoundFactory _roundFactory;
        private readonly IRoundStartService _roundStartService;
        private readonly IPlayerFactory _playerFactory;

        public GameStartService(IRoundFactory roundFactory,
            IRoundStartService roundStartService,
            IPlayerFactory playerFactory)
        {
            _roundFactory = roundFactory;
            _roundStartService = roundStartService;
            _playerFactory = playerFactory;
        }
        public IRunningGameState StartGame(IInitialGameState gameState)
        {
            var running = (IRunningGameState)gameState;
            var roundPlayers = _playerFactory.CreateRoundPlayers(gameState.Players);
            var round = _roundFactory.CreateRound(roundPlayers);
            var runningRound = _roundStartService.StartRound(round);
            running.RoundState = runningRound;
            return running;
        }
    }
}