namespace BlackJack
{
    public class Card
    {
        private char cardSuit;
        private int cardNumber;

        public Card(int suit, int number)
        {
            if (suit == 1) 
            {
                cardSuit = 'c';
            }
            else if (suit == 2)
            {
                cardSuit = 'd';
            }
            else if (suit == 3)
            {
                cardSuit = 'h';
            }
            else
            {
                cardSuit = 's';
            }

            cardNumber = number;
        }

        public char getSuit()
        {
            return cardSuit;
        }

        public int getNumber()
        {
            return cardNumber;
        }

        public int getValue()
        {
            if (cardNumber > 10)
            {
                return 10;
            }
            else if (cardNumber < 2)
            {
                return 11;
            }
            else
            {
                return cardNumber;
            }
        }
    }
}
