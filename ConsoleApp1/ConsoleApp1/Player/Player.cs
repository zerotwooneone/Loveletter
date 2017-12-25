using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Player
{
    public class Player
    {
        public Guid Id { get; set; }
    }

    public interface IPlayerConnectionService
    {
        //IObservable<PlayerConnection>
    }
}
