using System;

namespace ConsoleApp1.Player
{
    public class Player
    {
        public Guid Id { get; }

        public IObservable<bool> ReadyObservable { get; }

        public Player(IObservable<bool> readySubject, Guid id)
        {
            ReadyObservable = readySubject;
            Id = id;
        }
    }
}
