using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Player;

namespace ConsoleApp1.Card
{
    public class CardEffectService : ICardEffectService
    {
        private readonly ICardStateFactory _cardStateFactory;
        private readonly ICardRankService _cardRankService;

        public CardEffectService(ICardStateFactory cardStateFactory, ICardRankService cardRankService)
        {
            _cardStateFactory = cardStateFactory;
            _cardRankService = cardRankService;
        }

        public void Discard8(IRoundPlayerState player)
        {
            player.OutOfRound = true;
        }

        public void Discard6(IRoundPlayerState player, ITargetablePlayerState targetPlayer)
        {
            var temp = player.RoundHand;
            player.RoundHand = targetPlayer.RoundHand;
            targetPlayer.RoundHand = temp;
        }

        public void Discard5(ITargetablePlayerState targetPlayer, IList<IDrawableCardState> deck, IList<ISetAsideCardState> setAside)
        {
            var discarded = _cardStateFactory.Discard(targetPlayer.RoundHand);
            targetPlayer.RoundDiscard.Add(discarded);
            var newCard = deck.Any() ? 
                _cardStateFactory.Draw(deck) : 
                _cardStateFactory.Draw(setAside);
            targetPlayer.RoundHand = newCard;
        }

        public void Discard4(IRoundPlayerState player)
        {
            player.Protected = true;
        }

        public void Discard3(IRoundPlayerState player, ITargetablePlayerState targetPlayer)
        {
            var playerCard = player.RoundHand;
            var targetCard = targetPlayer.RoundHand;

            var playerIsGreater = _cardRankService.Compare(playerCard, targetCard);
            if (playerIsGreater.HasValue)
            {
                if (playerIsGreater.Value)
                {
                    targetPlayer.OutOfRound = true;
                }
                else
                {
                    player.OutOfRound = true;
                }
            }
        }

        public void Discard1(ITargetablePlayerState targetPlayer, int rank)
        {
            var matches = _cardRankService.Matches(targetPlayer.RoundHand, rank);
            if (matches)
            {
                targetPlayer.OutOfRound = true;
            }
        }
    }
}