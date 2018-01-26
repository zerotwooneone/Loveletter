using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        public IRoundPlayerState CreateRoundPlayer(IGamePlayerState gameStatePlayerState)
        {
            IList<IDiscardedCardState> roundDiscard = CreateRoundDiscard();
            IList<IDiscardableCardState> turnHand = null;
            var roundPlayer = new PlayerState(gameStatePlayerState.Id, roundDiscard, turnHand, outOfRound: false, points: 0, roundHand: null, turnDiscard: null);
            return roundPlayer;
        }

        public IList<IDiscardedCardState> CreateRoundDiscard()
        {
            return new List<IDiscardedCardState>(); 
        }

        public ITurnPlayerState CreateTurnPlayer(IRoundPlayerState playerState)
        {
            var turnHand=CreateTurnHand();
            turnHand.Add(playerState.RoundHand);
            var turnPlayer = new PlayerState(playerState.Id,null,turnHand,outOfRound:false,points:0);
            return turnPlayer;
        }

        public IList<IDiscardableCardState> CreateTurnHand()
        {
            return new List<IDiscardableCardState>();
        }
    }
}