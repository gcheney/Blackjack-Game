using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Form1 : Form
    {
        private double currentMoney = 1000;
        private int dealerScore = 0;
        private int hiddenDealerScore = 0;
        private int playerScore = 0;
        private int splitScore = 0;
        private int playerWins = 0;
        private int dealerWins = 0;
        private int pushGames = 0;
        private double currentBet = 0;
        private double splitBet = 0;
        private int dealerHandIdx = 0;
        private int playerHandIdx = 0;
        private int splitIndex = 0;
        private Deck cardDeck;
        private Card[] playersHand = new Card[5];
        private Card[] dealersHand = new Card[5];
        private Card[] splitHand = new Card[5];
        private bool playedHand = true;
        private bool btnSplitStandWasClicked = false;
        private bool btnStandWasClicked = false;
        private bool waitForSplit = false;
        private bool btnSplitWasClicked = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnReset_Click(sender, e);
            infoBox.Image = Image.FromFile("../cards/info.png");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            newHand();
            playedHand = true;
            playerWins = 0;
            dealerWins = 0;
            pushGames = 0;
            txtPlayerWin.Text = "0";
            txtDealerWin.Text = "0";
            currentMoney = 1000;
            txtMoney.Text = currentMoney.ToString("c");
        }

        private void btnBet_Click(object sender, EventArgs e)
        {
            try
            {
                double bet = double.Parse(txtBet.Text);
                if (bet < 0) 
                {
                    throw new FormatException();
                }
                if (bet > currentMoney) 
                {
                    throw new Exception("You dont have enough money! " + 
                            "Would you like to 'borrow' some more money?");
                }
                currentBet += bet;
                txtCurrentBet.Text = currentBet.ToString("c");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please enter a valid amount. " + ex.Message, 
                "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBet.SelectAll();
                txtBet.Focus();
            }
            catch (Exception exp)
            {
                DialogResult result = MessageBox.Show(exp.Message, "Invalid Amount",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    currentMoney += 1000;
                    txtMoney.Text = currentMoney.ToString("c");
                }
                txtBet.SelectAll();
                txtBet.Focus();
            }
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            if (playedHand == false)
            {
                MessageBox.Show("You have to play your hand first!", "Play Your Hand!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                newHand();
                for (playerHandIdx = 0; playerHandIdx < 2; playerHandIdx++)
                {
                    playersHand[playerHandIdx] = cardDeck.dealCard();
                    int value = playersHand[playerHandIdx].getValue();
                    if (value == 1 && playerScore < 11)
                        value = 11;
                    playerScore += value;
                    txtPlayerScore.Text = playerScore.ToString();
                    char s = playersHand[playerHandIdx].getSuit();
                    int n = playersHand[playerHandIdx].getNumber();
                    createPlayerCards(playerHandIdx, s, n);
                }

                for (dealerHandIdx = 0; dealerHandIdx < 2; dealerHandIdx++)
                {
                    dealersHand[dealerHandIdx] = cardDeck.dealCard();
                    char s = dealersHand[dealerHandIdx].getSuit();
                    int n = dealersHand[dealerHandIdx].getNumber();
                    hiddenDealerScore += dealersHand[dealerHandIdx].getValue();

                    if (dealerHandIdx > 0)
                    {
                        createDealerCards(dealerHandIdx, s, n);
                        int value = dealersHand[dealerHandIdx].getValue();
                        if (value == 1 && (dealerScore += 11) < 21) 
                        {
                            value = 11;
                        }
                        dealerScore += value;
                        txtDealerScore.Text = dealerScore.ToString();
                    }
                }

                playedHand = false;
            }
        }

        private void btnHit_Click(object sender, EventArgs e)
        {
            if (playedHand == true)
            {
                MessageBox.Show("Please press the 'Deal' button to get a new hand", "Invalid Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (btnStandWasClicked == true)
            {
                MessageBox.Show("Sorry, but you have to stand!", "No Backing Out!",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (waitForSplit)
            {
                MessageBox.Show("Please make a choice for the split.", "Attention",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (!waitForSplit)
            {
                if (hiddenDealerScore < 17)
                {
                    dealersHand[dealerHandIdx] = cardDeck.dealCard();
                    char s = dealersHand[dealerHandIdx].getSuit();
                    int n = dealersHand[dealerHandIdx].getNumber();
                    int value = dealersHand[dealerHandIdx].getValue();
                    if (value == 1 && dealerScore < 11) 
                    {
                        value = 11;
                    }
                    dealerScore += value;
                    hiddenDealerScore += value;
                    txtDealerScore.Text = dealerScore.ToString();
                    createDealerCards(dealerHandIdx, s, n);
                    dealerHandIdx++;
                }

                if (playerScore < 21)
                {
                    playersHand[playerHandIdx] = cardDeck.dealCard();
                    char s = playersHand[playerHandIdx].getSuit();
                    int n = playersHand[playerHandIdx].getNumber();
                    int value = playersHand[playerHandIdx].getValue();
                    if (value == 1 && playerScore < 11)
                        value = 11;
                    playerScore += value;
                    txtPlayerScore.Text = playerScore.ToString();
                    createPlayerCards(playerHandIdx, s, n);
                    playerHandIdx++;
                }

                if (playerScore >= 21 || dealerScore > 21)
                {
                    btnStand_Click(sender, e);
                }
            }
            if (btnSplitWasClicked && !btnSplitStandWasClicked)
                waitForSplit = true;
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
            btnStandWasClicked = true;
            if (playedHand == true)
            {
                MessageBox.Show("Please press the 'Deal' button to get a new hand", "Invalid Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (btnSplitStandWasClicked == true)
            {
                playDealerHand();
                declareWinner(playerScore, hiddenDealerScore, currentBet);
                declareWinner(splitScore, hiddenDealerScore, splitBet);
            }
            else if (btnSplitWasClicked == false)
            {
                playDealerHand();
                declareWinner(playerScore, hiddenDealerScore, currentBet);
            }
        }

        private void createPlayerCards(int index, char suit, int num)
        {
            PictureBox[] playerCards = {
                userBox1, userBox2, userBox3, userBox4
            };
            for (int i = 0; i < playerCards.Length; i++)
            {
                if(i == index) 
                {
                    playerCards[i].Image = Image.FromFile("../cards/" + suit + num + ".png");
                }
            }
        }

        private void createDealerCards(int index, char suit, int num)
        {
            PictureBox[] dealerCards = { dealerBox1, dealerBox2, dealerBox3, dealerBox4 };
            for (int i = 0; i < dealerCards.Length; i++)
            {
                if (i == index) 
                {
                    dealerCards[i].Image = Image.FromFile("../cards/" + suit + num + ".png");
                }
            }
        }

        private void playDealerHand()
        {
            while (hiddenDealerScore < 17)
            {
                dealersHand[dealerHandIdx] = cardDeck.dealCard();
                char s = dealersHand[dealerHandIdx].getSuit();
                int n = dealersHand[dealerHandIdx].getNumber();
                int value = dealersHand[dealerHandIdx].getValue();
                if (value == 1 && dealerScore < 11) 
                {
                    value = 11;
                }
                dealerScore += value;
                hiddenDealerScore += value;
                createDealerCards(dealerHandIdx, s, n);
                dealerHandIdx++;
            }
        }

        private void declareWinner(int player, int dealer, double bet)
        {
            playedHand = true;
            char s = dealersHand[0].getSuit();
            int n = dealersHand[0].getNumber();
            createDealerCards(0, s, n);
            txtDealerScore.Text = hiddenDealerScore.ToString();

            if (dealer > 21 && player <= 21)
            {
                 playerWin(bet);
            }
            else if (dealer > 21 && player > 21)
            {
                push();
            }
            else if ((player > 21 && dealer <= 21) || (player < 21 && dealer == 21))
            {
                dealerWin(bet);
            }
            else if (player < 21 && dealer < 21)
            {
                if (player > dealer)
                {
                    playerWin(bet);
                }
                else if (player == dealer)
                {
                    push();
                }
                else if (player < dealer)
                {
                    dealerWin(bet);
                }
            }
        }

        private void playerWin(double bet)
        {
            double winnings = bet * 1.5;
            Winner winner = new Winner(playerScore, hiddenDealerScore, winnings);
            winner.StartPosition = FormStartPosition.Manual;
            winner.Location = new Point(700, 100);
            winner.ShowDialog(this);
            playerWins += 1;
            txtPlayerWin.Text = playerWins.ToString();
            currentMoney += winnings;
            txtMoney.Text = currentMoney.ToString("c");
        }

        private void dealerWin(double bet)
        {
            Loser loser = new Loser(playerScore, hiddenDealerScore, bet);
            loser.StartPosition = FormStartPosition.Manual;
            loser.Location = new Point(700, 100);
            loser.ShowDialog(this);
            dealerWins += 1;
            txtDealerWin.Text = dealerWins.ToString();
            currentMoney -= bet;
            txtMoney.Text = currentMoney.ToString("c");
        }

        private void push()
        {
            Push push = new Push(playerScore, hiddenDealerScore);
            push.StartPosition = FormStartPosition.Manual;
            push.Location = new Point(700, 100);
            push.ShowDialog(this);
            pushGames += 1;
            txtPush.Text = pushGames.ToString();
        }

        private void newHand()
        {
            cardDeck = new Deck();
            userBox1.Image = Image.FromFile("../cards/back.png");
            userBox2.Image = Image.FromFile("../cards/back.png");
            userBox3.Image = null;
            userBox4.Image = null;
            dealerBox1.Image = Image.FromFile("../cards/back.png");
            dealerBox2.Image = Image.FromFile("../cards/back.png");
            dealerBox3.Image = null;
            dealerBox4.Image = null;
            splitBox1.Image = null;
            splitBox2.Image = null;
            splitBox3.Image = null;
            splitBox4.Image = null;
            dealerScore = 0;
            hiddenDealerScore = 0;
            playerScore = 0;
            splitScore = 0;
            currentBet = 0;
            splitBet = 0;
            dealerHandIdx = 0;
            playerHandIdx = 0;
            splitIndex = 0;
            txtDealerScore.Text = "0";
            txtPlayerScore.Text = "0";
            txtPush.Text = "0";
            txtCurrentBet.Text = "$0";
            Array.Clear(playersHand, 0, playersHand.Length);
            Array.Clear(dealersHand, 0, dealersHand.Length);
            Array.Clear(splitHand, 0, splitHand.Length);
            lblSplitScore.Visible = false;
            txtSplitScore.Visible = false;
            btnSplitHit.Visible = false;
            splitBox1.Visible = false;
            btnSplitBet.Visible = false;
            btnSplitStand.Visible = false;
            lblSplitBet.Visible = false;
            txtSplitBet.Visible = false;
            btnSplitStandWasClicked = false;
            btnStandWasClicked = false;
            waitForSplit = false;
            btnSplitWasClicked = false;
            txtGetSplitBet.Visible = false;
            lblBetTheSplit.Visible = false;
            txtBet.Clear();
            txtBet.Focus();
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            bool isValid = false;
            if (playersHand[0].getNumber() == playersHand[1].getNumber()) 
            {
                isValid = true;
            }
            if (isValid)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to split your hand?",
                     "Proceed with Split?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                  if (result == DialogResult.Yes)
                  {
                      btnSplitWasClicked = true;
                      lblSplitScore.Visible = true;
                      txtSplitScore.Visible = true;
                      btnSplitHit.Visible = true;
                      btnSplitBet.Visible = true;
                      btnSplitStand.Visible = true;
                      lblSplitBet.Visible = true;
                      txtSplitBet.Visible = true;
                      txtGetSplitBet.Visible = true;
                      lblBetTheSplit.Visible = true;
                      splitBox1.Visible = true;
                      userBox2.Image = null;
                      playerScore -= playersHand[1].getValue();
                      txtPlayerScore.Text = playerScore.ToString();
                      splitHand[0] = playersHand[1];
                      playersHand[1] = null;
                      playerHandIdx--;
                      char s = splitHand[0].getSuit();
                      int n = splitHand[0].getNumber();
                      createSplitCards(splitIndex, s, n);
                      splitScore += splitHand[0].getValue();
                      txtSplitScore.Text = splitScore.ToString();
                      splitIndex++;
                 }
           }
           else
           {
                MessageBox.Show("You cannot split the cards unless they have the same value!",
                     "Invalid Split", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           } 
        }

        private void createSplitCards(int index, char suit, int num)
        {
            PictureBox[] splitCards = {
                splitBox1, splitBox2, splitBox3, splitBox4
            };
            for (int i = 0; i < splitCards.Length; i++)
            {
                if (i == index)
                    splitCards[i].Image = Image.FromFile("../cards/" + suit + num + ".png");
            }
        }

        private void btnSplitBet_Click(object sender, EventArgs e)
        {
            try
            {
                double bet = double.Parse(txtGetSplitBet.Text);
                if (bet < 0)
                {
                    throw new FormatException();
                }
                if (bet > currentMoney)
                {
                    throw new Exception("You dont have enough money! " +
                            "Would you like to 'borrow' some more money?");
                }
                splitBet += bet;
                txtSplitBet.Text = splitBet.ToString("c");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please enter a valid amount. " + ex.Message, "Invalid Amount",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSplitBet.SelectAll();
                txtSplitBet.Focus();
            }
            catch (Exception exp)
            {
                DialogResult result = MessageBox.Show(exp.Message, "Invalid Amount",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    currentMoney += 1000;
                    txtMoney.Text = currentMoney.ToString("c");
                }
                txtSplitBet.SelectAll();
                txtSplitBet.Focus();
            }
        }

        private void btnSplitHit_Click(object sender, EventArgs e)
        {
            if (playedHand == true)
            {
                MessageBox.Show("Please press the 'Deal' button to get a new hand", "Invalid Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (btnSplitStandWasClicked == true)
            {
                MessageBox.Show("Sorry, but you have to stand!", "No Backing Out!",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                if (splitScore < 21)
                {
                    splitHand[splitIndex] = cardDeck.dealCard();
                    char s = splitHand[splitIndex].getSuit();
                    int n = splitHand[splitIndex].getNumber();
                    int value = splitHand[splitIndex].getValue();
                    if (value == 1 && splitScore < 11) 
                    {
                        value = 11;
                    }
                    splitScore += value;
                    txtSplitScore.Text = splitScore.ToString();
                    createSplitCards(splitIndex, s, n);
                    splitIndex++;
                }
                if (splitScore >= 21)
                {
                    btnSplitStand_Click(sender, e);
                    btnSplitStandWasClicked = true;
                }
            }
            waitForSplit = false;
        }

        private void btnSplitStand_Click(object sender, EventArgs e)
        {
            btnSplitStandWasClicked = true;
            waitForSplit = false;
            if (btnStandWasClicked == true) 
            {
                btnStand_Click(sender, e);
            }
        }

        private void infoBox_Click(object sender, EventArgs e)
        {
            Info info = new Info();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

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
