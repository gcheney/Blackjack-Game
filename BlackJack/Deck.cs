using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    /// <summary>
    /// The Deck class represents full deck of 52 cards
    /// </summary>
    public class Deck
    {
        private IList<Card> _deck = new List<Card>();
        private int _currentCard = 0;

        /// <summary>
        /// Initializes a new deck of 52 cards 
        /// in shuffled, random order
        /// </summary>
        public Deck()
        {
            for (var suit = 1; suit < 5; suit++)
            {
                for (var rank = 1; rank < 14; rank++)
                {
                    Card card = new Card(suit, rank);
                    _deck.Add(card);
                }
            }

            shuffleDeck(_deck);
        }

        /// <summary>
        /// Shuffled the provided Deck of Cards
        /// </summary>
        /// <param name="deck">The Deck of Cards to shuffle</param>
        public void shuffleDeck(IList<Card> deck)
        {
            var random = new Random();
            for (var i = 0; i < deck.Count; i++)
            {
                Card tempCard = deck[i];
                var randomIndex = random.Next(deck.Count - i) + i;
                deck[i] = deck[randomIndex];
                deck[randomIndex] = tempCard;
            }
        }

        /// <summary>
        /// Returns a new card form the top of the Deck
        /// </summary>
        /// <returns>A Card object taken from the top of the deck</returns>
        public Card dealCard()
        {
            Card dealtCard = _deck.ElementAt(_currentCard);
            _currentCard += 1;
            return dealtCard;
        }
    }
}
