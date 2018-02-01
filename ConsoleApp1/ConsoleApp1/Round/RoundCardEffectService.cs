using ConsoleApp1.Card;
using ConsoleApp1.Turn;

namespace ConsoleApp1.Round
{
    public class RoundCardEffectService : IRoundCardEffectService
    {
        private readonly ICardEffectService _cardEffectService;
        private readonly ITurnService _turnService;

        public RoundCardEffectService(ICardEffectService cardEffectService,
            ITurnService turnService)
        {
            _cardEffectService = cardEffectService;
            _turnService = turnService;
        }

        public void AfterDraw(IRunningRoundState round)
        {
            throw new System.NotImplementedException();
        }

        public void AfterDiscard(IRunningRoundState round)
        {
            throw new System.NotImplementedException();
        }
    }
}