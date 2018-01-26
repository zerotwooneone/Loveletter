using System;
using System.Collections.Generic;
using ConsoleApp1.Card;
using ConsoleApp1.Player;
using ConsoleApp1.Round;

namespace ConsoleApp1.Game
{
    public class GameState : IInitialGameState, IRunningGameState
    {
        private readonly IDictionary<Guid, IGamePlayerState> _players;
        public IRunningRoundState RoundState { get; set; }
        public Guid Id { get; }
        public IEnumerable<IGamePlayerState> Players => _players.Values;
        
        public GameState(Guid id,
            IDictionary<Guid, 
                IGamePlayerState> players,
            IRunningRoundState roundState = null)
        {
            Id = id;
            _players = players;
            RoundState = roundState;
        }
    }
}