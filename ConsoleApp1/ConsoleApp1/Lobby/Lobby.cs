﻿using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace ConsoleApp1.Lobby
{
    public class Lobby
    {
        private readonly IObservable<Player.Player> _playerObservable;
        public Guid Id { get; }
        public IEnumerable<Player.Player> LobbyPlayers => _lobbyPlayers;
        public IEnumerable<Player.Player> ReadyPlayers => _readyPlayers.Values;
        private readonly Dictionary<Guid, Player.Player> _readyPlayers;
        private readonly List<Player.Player> _lobbyPlayers;
        private readonly BehaviorSubject<bool> _allPlayersReady;
        public IObservable<bool> AllPlayersReady => _allPlayersReady;

        public Lobby(IObservable<Player.Player> playerObservable, 
            Guid id)
        {
            _lobbyPlayers = new List<Player.Player>();
            _readyPlayers = new Dictionary<Guid, Player.Player>();
            _allPlayersReady = new BehaviorSubject<bool>(false);
            _playerObservable = playerObservable;
            Id = id;

            _playerObservable
                .Subscribe(OnNewLobbyPlayer);
        }

        private void OnNewLobbyPlayer(Player.Player player)
        {
            _lobbyPlayers.Add(player);
            player
                .ReadyObservable
                .Subscribe(ready =>
                {
                    if (ready)
                    {
                        _readyPlayers.TryAdd(player.Id, player);
                    }
                    else
                    {
                        _readyPlayers.Remove(player.Id);
                    }
                    var allReady =ReadyCheck();
                    if (allReady != _allPlayersReady.Value)
                    {
                        _allPlayersReady.OnNext(allReady);
                    }
                });
        }

        private bool ReadyCheck()
        {
            return _readyPlayers.Count > 1 && _lobbyPlayers.Count == _readyPlayers.Count;
        }
    }
}
