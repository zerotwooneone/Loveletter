using System.Collections.Generic;
using ConsoleApp1.Player;

namespace ConsoleApp1.Card
{
    public interface ICardEffectService
    {
        void Discard8(IRoundPlayerState player);
        //void On7();
        void Discard6(IRoundPlayerState player, ITargetablePlayerState targetPlayer);
        void Discard5(ITargetablePlayerState targetPlayer, 
            IList<IDrawableCardState> deck,
            IList<ISetAsideCardState> setAside);
        void Discard4(IRoundPlayerState player);
        void Discard3(IRoundPlayerState player, ITargetablePlayerState targetPlayer);
        //void Discard2(ITargetablePlayerState targetPlayer);
        void Discard1(ITargetablePlayerState targetPlayer, int rank);
    }
}