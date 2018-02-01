namespace ConsoleApp1.Card
{
    public interface ICardRankService
    {
        /// <summary>
        /// Answers the question: is the player card's rank greater than the target card's rank
        /// </summary>
        /// <param name="playerCard"></param>
        /// <param name="targetCard"></param>
        /// <returns>true if player is greater than target, false if target is greater than player, and null if they have the same rank</returns>
        bool? Compare(IDiscardableCardState playerCard, IDiscardableCardState targetCard);

        /// <summary>
        /// Answers the question: does the given rank match the target card
        /// </summary>
        /// <param name="targetCard"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        bool Matches(IDiscardableCardState targetCard, int rank);
    }
}