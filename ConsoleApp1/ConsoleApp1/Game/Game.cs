using System;
using System.Reactive.Subjects;
using ConsoleApp1.Lobby;

namespace ConsoleApp1.Game
{
    public class Game
    {
        private readonly ISubject<LobbyParams> _lobbySubject;
        public Guid Id { get; set; }
        public IObservable<LobbyParams> LobbyObservable => _lobbySubject;

        public Game(ISubject<LobbyParams> lobbySubject)
        {
            _lobbySubject = lobbySubject;
        }
    }
}