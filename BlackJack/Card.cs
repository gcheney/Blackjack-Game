using System;

namespace BlackJack
{
    /// <summary>
    /// The Card class represents a single card used
    /// during the Blackjack game. 
    /// The card has a suit and rank property.
    /// </summary>
    public class Card
    {
        public char Suit { get; set; }
        public int Rank { get; set; }

        public Card(int suit, int rank)
        {
            switch (suit)
            {
                case 1:
                    Suit = 'c';
                    break;
                case 2:
                    Suit = 'd';
                    break;
                case 3:
                    Suit = 'h';
                    break;
                default:
                    Suit = 's';
                    break;
            }

            Rank = rank;
        }

        public int getValue()
        {
            if (Rank > 10)
            {
                return 10;
            }
            else if (Rank < 2)
            {
                return 11;
            }
            else
            {
                return Rank;
            }
        }
    }
}
