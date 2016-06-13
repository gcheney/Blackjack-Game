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

        /// <summary>
        /// Initializes a new Card object with  
        /// the provided suit and rank
        /// </summary>
        /// <param name="suit">The Card suit</param>
        /// <param name="rank">The Card rank</param>
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

        /// <summary>
        /// Returns the value of the card as an integer
        /// </summary>
        /// <returns>The Card objects value</returns>
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
