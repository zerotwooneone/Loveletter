using System;
using System.Reactive.Subjects;
using ConsoleApp1.Game;

namespace ConsoleApp1.Lobby
{
    public class Lobby
    {
        private readonly ISubject<GameParams> _subject;
        public Guid Id { get; private set; }
        public IObservable<GameParams> GameObservable => _subject;

        public Lobby(ISubject<GameParams> gameSubject)
        {
            _subject = gameSubject;
        }
    }
}
