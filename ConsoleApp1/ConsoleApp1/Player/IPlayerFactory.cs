using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Player
{
    public interface IPlayerFactory
    {
        IRoundPlayerState CreateRoundPlayer(IGamePlayerState gameStatePlayerState);
        IList<IDiscardedCardState> CreateRoundDiscard();
        ITurnPlayerState CreateTurnPlayer(IRoundPlayerState playerState);
    }
}