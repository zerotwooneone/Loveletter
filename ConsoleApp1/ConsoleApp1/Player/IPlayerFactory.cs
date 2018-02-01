using System;
using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Player
{
    public interface IPlayerFactory
    {
        IRoundPlayerState CreateRoundPlayer(IGamePlayerState gameStatePlayerState);
        IList<IDiscardedCardState> CreateRoundDiscard();
        IDrawablePlayerState CreateTurnPlayer(IRoundPlayerState playerState);
        IDiscardablePlayerState GetDiscardable(Guid id, IList<IDiscardableCardState> turnHand);
        IRoundPlayerState EndTurn(IDiscardablePlayerState player, IList<IDiscardedCardState> roundDiscard, bool OutOfRound = false, bool IsProtected = false);
    }
}