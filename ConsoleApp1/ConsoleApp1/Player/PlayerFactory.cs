using System;
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

        public IDrawablePlayerState CreateTurnPlayer(IRoundPlayerState playerState)
        {
            IList<IDiscardableCardState> turnHand = null;
            var turnPlayer = new PlayerState(playerState.Id, null, turnHand, outOfRound: false, points: 0);
            return turnPlayer;
        }

        public IDiscardablePlayerState GetDiscardable(Guid id, IList<IDiscardableCardState> turnHand)
        {
            IList<IDiscardedCardState> roundHand=null;
            return new PlayerState(id, roundHand, turnHand, outOfRound: false, points: 0, roundHand: null, turnDiscard: null);
        }

        public IRoundPlayerState EndTurn(IDiscardablePlayerState player, IList<IDiscardedCardState> roundDiscard, bool OutOfRound = false, bool IsProtected = false)
        {
            roundDiscard.Add(player.TurnDiscard);
            IList<IDiscardableCardState> turnHand = null;
            var roundPlayer = new PlayerState(player.Id, roundDiscard, turnHand, outOfRound: false, points: 0, roundHand: null, turnDiscard: null);
            return roundPlayer;
        }

        public IList<IDiscardableCardState> CreateTurnHand()
        {
            return new List<IDiscardableCardState>();
        }
    }
}