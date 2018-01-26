using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace ConsoleApp1.Lobby
{
    public class Lobby
    {
        private readonly IObservable<Player.PlayerState> _playerObservable;
        public Guid Id { get; }
        public IEnumerable<Player.PlayerState> LobbyPlayers => _lobbyPlayers;
        public IEnumerable<Player.PlayerState> ReadyPlayers => _readyPlayers.Values;
        private readonly Dictionary<Guid, Player.PlayerState> _readyPlayers;
        private readonly List<Player.PlayerState> _lobbyPlayers;
        private readonly BehaviorSubject<bool> _allPlayersReady;
        public IObservable<bool> AllPlayersReady => _allPlayersReady;

        public Lobby(IObservable<Player.PlayerState> playerObservable, 
            Guid id)
        {
            _lobbyPlayers = new List<Player.PlayerState>();
            _readyPlayers = new Dictionary<Guid, Player.PlayerState>();
            _allPlayersReady = new BehaviorSubject<bool>(false);
            _playerObservable = playerObservable;
            Id = id;

            _playerObservable
                .Subscribe(OnNewLobbyPlayer);
        }

        private void OnNewLobbyPlayer(Player.PlayerState playerState)
        {
            _lobbyPlayers.Add(playerState);
            
        }

        private bool ReadyCheck()
        {
            const int minimumAllowedPlayers = 1;
            const int maxAllowedPlayers = 4;
            return _readyPlayers.Count >= minimumAllowedPlayers && _readyPlayers.Count <= maxAllowedPlayers && _lobbyPlayers.Count == _readyPlayers.Count;
        }
    }
}
