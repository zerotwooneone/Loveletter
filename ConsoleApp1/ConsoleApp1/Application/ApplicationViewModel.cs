using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using ConsoleApp1.Game;
using ConsoleApp1.Lobby;

namespace ConsoleApp1.Application
{
    public class ApplicationViewModel
    {
        private readonly ILobbyFactory _lobbyFactory;
        private readonly IGameFactory _gameFactory;
        private readonly ILobbyService _lobbyService;
        private readonly IGameService _gameService;
        private readonly IConsoleService _consoleService;
        private bool _doContinue;
        
        public ApplicationViewModel(ILobbyFactory lobbyFactory,
            IGameFactory gameFactory,
            ILobbyService lobbyService,
            IGameService gameService,
            IConsoleService consoleService)
        {
            _lobbyFactory = lobbyFactory;
            _gameFactory = gameFactory;
            _lobbyService = lobbyService;
            _gameService = gameService;
            _consoleService = consoleService;
        }

        public Lobby.Lobby Lobby { get; private set; }
        public Game.Game Game { get; private set; }

        public void Start()
        {
            _doContinue = true;
            LobbyParams lobbyParams=null;
            CreateLobby(lobbyParams);
        }

        private void CreateLobby(LobbyParams lobbyParams)
        {
            Lobby = _lobbyFactory.CreateLobby(lobbyParams);
            Lobby
                .GameObservable
                .Take(1)
                .Subscribe(CreateGame);
        }

        private void CreateGame(GameParams gameParams)
        {
            Game = _gameFactory.CreateGame(gameParams);
            Game
                .LobbyObservable
                .Take(1)
                .Subscribe(CreateLobby);
        }

        public async Task<bool> DoContinue()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1));
            return _doContinue;
        }
    }
}
