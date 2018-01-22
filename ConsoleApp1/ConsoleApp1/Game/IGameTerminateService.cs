using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1.Player;

namespace ConsoleApp1.Game
{
    public interface IGameTerminateService
    {
        
        Task TerminateGame(IEnumerable<IGamePlayer> players);
    }
}