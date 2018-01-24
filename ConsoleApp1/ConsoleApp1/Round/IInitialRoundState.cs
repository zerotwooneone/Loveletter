﻿using System.Collections.Generic;
using ConsoleApp1.Card;
using ConsoleApp1.Player;

namespace ConsoleApp1.Round
{
    public interface IInitialRoundState : IRoundState
    {
        IEnumerable<IRoundPlayer> Players { get; }
        IEnumerable<IShufflableCardState> ShufflableDeck { get; }
    }
}