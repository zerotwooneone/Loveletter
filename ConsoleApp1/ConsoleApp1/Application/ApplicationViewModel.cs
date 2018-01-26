using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using ConsoleApp1.Game;
using ConsoleApp1.Lobby;
using ConsoleApp1.Player;
using ConsoleApp1.User;

namespace ConsoleApp1.Application
{
    public class ApplicationViewModel
    {
        private readonly ILobbyFactory _lobbyFactory;
        private readonly IGameFactory _gameFactory;
        private readonly ILobbyService _lobbyService;
        private readonly IConsoleService _consoleService;
        
        private readonly IUserService _userService;
        private bool _doContinue;
        
        
        public ApplicationViewModel(ILobbyFactory lobbyFactory, 
            IGameFactory gameFactory, 
            ILobbyService lobbyService, 
            IConsoleService consoleService, 
            IUserService userService)
        {
            _lobbyFactory = lobbyFactory;
            _gameFactory = gameFactory;
            _lobbyService = lobbyService;
            _consoleService = consoleService;
            _userService = userService;
        }

        public Lobby.Lobby Lobby { get; private set; }
        public Game.GameState GameState { get; private set; }

        public void Start()
        {
            _doContinue = true;
            UserPlayerParams = _userService.GetPlayerParams();
//UserPlayerState = _playerFactory.CreatePlayer(UserPlayerParams);
            LobbyParams lobbyParams=new LobbyParams
            {
                Players = new PlayerParams[]{UserPlayerParams}
            };
            CreateLobby(lobbyParams);
        }

        public Subject<bool> UserReadySubject { get; private set; }

        public PlayerParams UserPlayerParams { get; private set; }

        public Player.PlayerState UserPlayerState { get; private set; }

        private void CreateLobby(LobbyParams lobbyParams)
        {
            Lobby = _lobbyFactory.CreateLobby(lobbyParams);
            //Lobby
            //    .GameObservable
            //    .Take(1)
            //    .Subscribe(CreateGame);
        }
        
        public async Task<bool> DoContinue()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1));
            return _doContinue;
        }
    }
}
