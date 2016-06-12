namespace BlackJack
{
    public class Deck
    {
        private ArrayList deck = new ArrayList();
        private int currentCard = 0;

        public Deck()
        {
            for (int x = 1; x < 5; x++)
            {
                for (int y = 1; y < 14; y++)
                {
                    Card card = new Card(x, y);
                    deck.Add(card);
                }
            }

            shuffleDeck(deck);
        }

        public void shuffleDeck(ArrayList deck)
        {
            Random random = new Random();
            for (int i = 0; i < deck.Count; i++)
            {
                object temp = deck[i];
                int randomIndex = random.Next(deck.Count - i) + i;
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }

        public Card dealCard()
        {
            Card dealtCard = (Card)deck[currentCard];
            currentCard += 1;
            return dealtCard;
        }
    }
}