using System.Collections.Generic;
using ConsoleApp1.Card;
using ConsoleApp1.Player;

namespace ConsoleApp1.Round
{
    public interface IRoundFactory
    {
        IInitialRoundState CreateRound(IEnumerable<IRoundPlayerState> players);
        IList<IRoundPlayerState> CreateRemainingPlayers();
        IList<ISetAsideCardState> CreateRemovedFromRound();
    }
}
