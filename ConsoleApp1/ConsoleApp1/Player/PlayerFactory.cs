using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        public IRoundPlayer CreateRoundPlayer(IGamePlayer gameStatePlayer)
        {
            IList<IDiscardedCardState> roundDiscard = CreateRoundDiscard();
            IList<IDiscardableCardState> turnHand = null;
            var roundPlayer = new Player(gameStatePlayer.Id, roundDiscard, turnHand, outOfRound: false, points: 0, roundHand: null, turnDiscard: null);
            return roundPlayer;
        }

        public IList<IDiscardedCardState> CreateRoundDiscard()
        {
            return new List<IDiscardedCardState>();
        }

        public ITurnPlayer CreateTurnPlayer(IRoundPlayer player)
        {
            var turnPlayer = new Player(player.Id,null,turnHand,outOfRound:false,points:0);
            return turnPlayer;
        }
    }
}