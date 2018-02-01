using ConsoleApp1.Card;
using ConsoleApp1.Player;
using ConsoleApp1.Turn;

namespace ConsoleApp1.Round
{
    public class RoundService : IRoundService
    {
        private readonly ITurnService _turnService;
        private readonly IRoundCardEffectService _roundCardEffectService;

        public RoundService(ITurnService turnService,
            IRoundCardEffectService roundCardEffectService)
        {
            _turnService = turnService;
            _roundCardEffectService = roundCardEffectService;
        }

        public void Draw(IRunningRoundState round)
        {
            throw new System.NotImplementedException();
        }

        public void Discard(IRunningRoundState round, IDiscardableCardState card, ITargetablePlayerState targetPlayer = null)
        {
            throw new System.NotImplementedException();
        }
    }
}