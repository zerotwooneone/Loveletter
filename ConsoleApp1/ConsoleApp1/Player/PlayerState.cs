using System;
using System.Collections.Generic;
using ConsoleApp1.Card;

namespace ConsoleApp1.Player
{
    public class PlayerState : IGamePlayerState, IRoundPlayerState, ITargetablePlayerState, IDiscardablePlayerState, IDrawablePlayerState
    {
        public Guid Id { get; }
        public bool OutOfRound { get; set; }
        public IDiscardableCardState RoundHand { get; set; }
        public IList<IDiscardedCardState> RoundDiscard { get; }
        public int Points { get; set; }
        public IList<IDiscardableCardState> TurnHand { get; }
        public IDiscardedCardState TurnDiscard { get; set; }
        
        public PlayerState(Guid id,
            IList<IDiscardedCardState> roundDiscard,
            IList<IDiscardableCardState> turnHand,
            bool outOfRound = false,
            int points = 0,
            IDiscardableCardState roundHand = null,
            IDiscardedCardState turnDiscard = null)
        {
            Id = id;
            OutOfRound = outOfRound;
            Points = points;
            RoundDiscard = roundDiscard;
            RoundHand = roundHand;
            TurnDiscard = turnDiscard;
            TurnHand = turnHand;
        }
    }
}
