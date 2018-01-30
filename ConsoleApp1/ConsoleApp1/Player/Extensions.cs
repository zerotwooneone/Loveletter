using System.Collections.Generic;

namespace ConsoleApp1.Player
{
    public static class Extensions
    {
        public static IEnumerable<IRoundPlayerState> CreateRoundPlayers(this IPlayerFactory playerFactory, IEnumerable<IGamePlayerState> gameStatePlayers)
        {
            foreach (var gameStatePlayer in gameStatePlayers)
            {
                yield return playerFactory.CreateRoundPlayer(gameStatePlayer);
            }
        }

        public static IDiscardablePlayerState GetDiscardable(this IPlayerFactory playerFactory, IDrawablePlayerState drawablePlayerState)
        {
            return playerFactory.GetDiscardable(drawablePlayerState.Id, drawablePlayerState.TurnHand);
        }
    }
}