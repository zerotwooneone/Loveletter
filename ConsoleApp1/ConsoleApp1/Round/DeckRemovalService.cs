using System;

namespace ConsoleApp1.Round
{
    public class DeckRemovalService : IDeckRemovalService
    {
        public int GetCardsToRemoveCount(int playerCount)
        {
            int result;
            switch (playerCount)
            {
                case 2:
                    result = 4;
                    break;
                case 3:
                case 4:
                    result = 1;
                    break;
                default:
                    throw new ArgumentException("Invalid number of players");
            }
            return result;
        }
    }
}