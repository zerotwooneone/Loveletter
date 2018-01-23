using System.Collections.Generic;

namespace ConsoleApp1.Player
{
    public static class Extensions
    {
        public static IEnumerable<IRoundPlayer> CreateRoundPlayers(this IPlayerFactory playerFactory, IEnumerable<IGamePlayer> gameStatePlayers)
        {
            foreach (var gameStatePlayer in gameStatePlayers)
            {
                yield return playerFactory.CreateRoundPlayer(gameStatePlayer);
            }
        }
    }
}