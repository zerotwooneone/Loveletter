using System;
using ConsoleApp1.Deck;
using ConsoleApp1.Round;

namespace ConsoleApp1.Game
{
    public class GameStartService : IGameStartService
    {
        private readonly IDeckFactory _deckFactory;
        private readonly IDeckShuffleService _deckShuffleService;
        private readonly IRoundFactory _roundFactory;
        private readonly IRoundStartService _roundStartService;

        public GameStartService(IDeckFactory deckFactory,
            IDeckShuffleService deckShuffleService,
            IRoundFactory roundFactory,
            IRoundStartService roundStartService)
        {
            _deckFactory = deckFactory;
            _deckShuffleService = deckShuffleService;
            _roundFactory = roundFactory;
            _roundStartService = roundStartService;
        }
        public IRunningGameState StartGame(IInitialGameState gameState)
        {
            var running = (IRunningGameState)gameState;
            var deck = _deckFactory.Create();
            var shuffledDeck = _deckShuffleService.Shuffle(deck);
            foreach (var cardState in shuffledDeck)
            {
                running.DrawDeck.Add(cardState);
            }
            var round = _roundFactory.CreateRound();
            var runningRound = _roundStartService.StartRound(round);
            running.RoundState = runningRound;
            return running;
        }
    }
}