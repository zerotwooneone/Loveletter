using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using ConsoleApp1.Lobby;
using ConsoleApp1.Player;

namespace ConsoleApp1.Game
{
    public class Game
    {
        public IEnumerable<Player.Player> Players { get; }
        private readonly ISubject<LobbyParams> _lobbySubject;
        public Guid Id { get; set; }
        public IObservable<LobbyParams> LobbyObservable => _lobbySubject;

        public Game(ISubject<LobbyParams> lobbySubject, IEnumerable<Player.Player> players)
        {
            Players = players;
            _lobbySubject = lobbySubject;
        }
    }
}