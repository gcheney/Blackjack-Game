using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlackJack
{
    /// <summary>
    /// The main UI class for the BlackJack Game
    /// </summary>
    public partial class BlackJackUI : Form
    {
        private double currentMoney;
        private int dealerScore;
        private int hiddenDealerScore;
        private int playerScore;
        private int splitScore;
        private int playerWins;
        private int dealerWins;
        private int pushGames;
        private double currentBet;
        private double splitBet;
        private int dealerHandIdx;
        private int playerHandIdx;
        private int splitIndex;
        private Deck cardDeck;
        private Card[] playersHand;
        private Card[] dealersHand;
        private Card[] splitHand;
        private bool playedHand;
        private bool btnSplitStandWasClicked;
        private bool btnStandWasClicked;
        private bool waitForSplit;
        private bool btnSplitWasClicked;

        /// <summary>
        /// Initializes a new BlackJack UI component
        /// </summary>
        public BlackJackUI()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            currentMoney = 1000;
            dealerScore = 0;
            hiddenDealerScore = 0;
            playerScore = 0;
            splitScore = 0;
            playerWins = 0;
            dealerWins = 0;
            pushGames = 0;
            currentBet = 0;
            splitBet = 0;
            dealerHandIdx = 0;
            playerHandIdx = 0;
            splitIndex = 0;
            cardDeck = new Deck();
            playersHand = new Card[5];
            dealersHand = new Card[5];
            splitHand = new Card[5];
            playedHand = true;
            btnSplitStandWasClicked = false;
            btnStandWasClicked = false;
            waitForSplit = false;
            btnSplitWasClicked = false;
            txtMoney.Text = currentMoney.ToString("c");
            DealNewHand();
        }

        private void BlackJackUI_Load(object sender, EventArgs e)
        {
            var infoImage = "../cards/info.png";
            infoBox.Image = Image.FromFile(infoImage);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Reset the game? All current progress will be lost.",
                     "Reset Game?",
                     MessageBoxButtons.YesNoCancel,
                     MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DealNewHand();
                playedHand = true;
                playerWins = 0;
                dealerWins = 0;
                pushGames = 0;
                txtPlayerWin.Text = "0";
                txtDealerWin.Text = "0";
                currentMoney = 1000;
                txtMoney.Text = currentMoney.ToString("c");
            }
        }

        private void btnBet_Click(object sender, EventArgs e)
        {
            try
            {
                double bet = double.Parse(txtBet.Text);
                if (bet < 0)
                {
                    throw new FormatException("Your bet must be a positive number.");
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
                MessageBox.Show("Please enter a valid amount. " + ex.Message, "Invalid Amount",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DealNewHand();
                for (playerHandIdx = 0; playerHandIdx < 2; playerHandIdx++)
                {
                    playersHand[playerHandIdx] = cardDeck.dealCard();
                    int value = playersHand[playerHandIdx].getValue();
                    if (value == 1 && playerScore < 11)
                    {
                        value = 11;
                    }
                    playerScore += value;
                    txtPlayerScore.Text = playerScore.ToString();
                    char s = playersHand[playerHandIdx].Suit;
                    int r = playersHand[playerHandIdx].Rank;
                    CreatePlayerCards(playerHandIdx, s, r);
                }

                for (dealerHandIdx = 0; dealerHandIdx < 2; dealerHandIdx++)
                {
                    dealersHand[dealerHandIdx] = cardDeck.dealCard();
                    char s = dealersHand[dealerHandIdx].Suit;
                    int r = dealersHand[dealerHandIdx].Rank;
                    hiddenDealerScore += dealersHand[dealerHandIdx].getValue();

                    if (dealerHandIdx > 0)
                    {
                        CreateDealerCards(dealerHandIdx, s, r);
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
                    char s = dealersHand[dealerHandIdx].Suit;
                    int r = dealersHand[dealerHandIdx].Rank;
                    int value = dealersHand[dealerHandIdx].getValue();
                    if (value == 1 && dealerScore < 11)
                    {
                        value = 11;
                    }

                    dealerScore += value;
                    hiddenDealerScore += value;
                    txtDealerScore.Text = dealerScore.ToString();
                    CreateDealerCards(dealerHandIdx, s, r);
                    dealerHandIdx++;
                }

                if (playerScore < 21)
                {
                    playersHand[playerHandIdx] = cardDeck.dealCard();
                    char s = playersHand[playerHandIdx].Suit;
                    int r = playersHand[playerHandIdx].Rank;
                    int value = playersHand[playerHandIdx].getValue();
                    if (value == 1 && playerScore < 11)
                    {
                        value = 11;
                    }
                    playerScore += value;
                    txtPlayerScore.Text = playerScore.ToString();
                    CreatePlayerCards(playerHandIdx, s, r);
                    playerHandIdx++;
                }

                if (playerScore >= 21 || dealerScore > 21)
                {
                    btnStand_Click(sender, e);
                }
            }
            if (btnSplitWasClicked && !btnSplitStandWasClicked)
            {
                waitForSplit = true;
            }
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
                PlayDealerHand();
                DeclareWinner(playerScore, hiddenDealerScore, currentBet);
                DeclareWinner(splitScore, hiddenDealerScore, splitBet);
            }
            else if (btnSplitWasClicked == false)
            {
                PlayDealerHand();
                DeclareWinner(playerScore, hiddenDealerScore, currentBet);
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            bool isValidSplit = false;
            if (playersHand[0].Rank == playersHand[1].Rank)
            {
                isValidSplit = true;
            }
            if (isValidSplit)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to split your hand?",
                     "Proceed with Split?", 
                     MessageBoxButtons.YesNo, 
                     MessageBoxIcon.Information);
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
                    char s = splitHand[0].Suit;
                    int r = splitHand[0].Rank;
                    CreateSplitCards(splitIndex, s, r);
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
                    char s = splitHand[splitIndex].Suit;
                    int r = splitHand[splitIndex].Rank;
                    int value = splitHand[splitIndex].getValue();
                    if (value == 1 && splitScore < 11)
                    {
                        value = 11;
                    }
                    splitScore += value;
                    txtSplitScore.Text = splitScore.ToString();
                    CreateSplitCards(splitIndex, s, r);
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

        private void CreatePlayerCards(int index, char suit, int num)
        {
            PictureBox[] playerCards = {
                userBox1, userBox2, userBox3, userBox4
            };
            for (int i = 0; i < playerCards.Length; i++)
            {
                if (i == index)
                {
                    var imgFile = "../cards/" + suit + num + ".png";
                    playerCards[i].Image = Image.FromFile(imgFile);
                }
            }
        }

        private void CreateDealerCards(int index, char suit, int num)
        {
            PictureBox[] dealerCards = {
                dealerBox1, dealerBox2, dealerBox3, dealerBox4
            };
            for (int i = 0; i < dealerCards.Length; i++)
            {
                if (i == index)
                {
                    var imgFile = "../cards/" + suit + num + ".png";
                    dealerCards[i].Image = Image.FromFile(imgFile);
                }
            }
        }

        private void PlayDealerHand()
        {
            while (hiddenDealerScore < 17)
            {
                dealersHand[dealerHandIdx] = cardDeck.dealCard();
                char s = dealersHand[dealerHandIdx].Suit;
                int r = dealersHand[dealerHandIdx].Rank;
                int value = dealersHand[dealerHandIdx].getValue();
                if (value == 1 && dealerScore < 11)
                {
                    value = 11;
                }
                dealerScore += value;
                hiddenDealerScore += value;
                CreateDealerCards(dealerHandIdx, s, r);
                dealerHandIdx++;
            }
        }

        private void CreateSplitCards(int index, char suit, int num)
        {
            PictureBox[] splitCards = {
                splitBox1, splitBox2, splitBox3, splitBox4
            };
            for (int i = 0; i < splitCards.Length; i++)
            {
                if (i == index)
                {
                    var imgFile = "../cards/" + suit + num + ".png";
                    splitCards[i].Image = Image.FromFile(imgFile);
                }
            }
        }

        private void DeclareWinner(int player, int dealer, double bet)
        {
            playedHand = true;
            char s = dealersHand[0].Suit;
            int r = dealersHand[0].Rank;
            CreateDealerCards(0, s, r);
            txtDealerScore.Text = hiddenDealerScore.ToString();

            if (dealer > 21 && player <= 21)
            {
                PlayerWin(bet);
            }
            else if (dealer > 21 && player > 21)
            {
                Push();
            }
            else if ((player > 21 && dealer <= 21) || (player < 21 && dealer == 21))
            {
                DealerWin(bet);
            }
            else if (player < 21 && dealer < 21)
            {
                if (player > dealer)
                {
                    PlayerWin(bet);
                }
                else if (player == dealer)
                {
                    Push();
                }
                else if (player < dealer)
                {
                    DealerWin(bet);
                }
            }
        }

        private void PlayerWin(double bet)
        {
            double winnings = bet * 1.5;
            var winMessage = new WinnerMessage(playerScore, hiddenDealerScore, winnings);
            winMessage.StartPosition = FormStartPosition.Manual;
            winMessage.Location = new Point(700, 100);
            winMessage.ShowDialog(this);
            playerWins += 1;
            txtPlayerWin.Text = playerWins.ToString();
            currentMoney += winnings;
            txtMoney.Text = currentMoney.ToString("c");
        }

        private void DealerWin(double bet)
        {
            var loseMessage = new LoserMessage(playerScore, hiddenDealerScore, bet);
            loseMessage.StartPosition = FormStartPosition.Manual;
            loseMessage.Location = new Point(700, 100);
            loseMessage.ShowDialog(this);
            dealerWins += 1;
            txtDealerWin.Text = dealerWins.ToString();
            currentMoney -= bet;
            txtMoney.Text = currentMoney.ToString("c");
        }

        private void Push()
        {
            PushMessage push = new PushMessage(playerScore, hiddenDealerScore);
            push.StartPosition = FormStartPosition.Manual;
            push.Location = new Point(700, 100);
            push.ShowDialog(this);
            pushGames += 1;
            txtPush.Text = pushGames.ToString();
        }

        private void DealNewHand()
        {
            var backImage = "../cards/back.png";
            cardDeck = new Deck();
            userBox1.Image = Image.FromFile(backImage);
            userBox2.Image = Image.FromFile(backImage);
            userBox3.Image = null;
            userBox4.Image = null;
            dealerBox1.Image = Image.FromFile(backImage);
            dealerBox2.Image = Image.FromFile(backImage);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Are you sure you want to exit the game?",
                     "Exit Game?",
                     MessageBoxButtons.YesNoCancel,
                     MessageBoxIcon.Stop);

            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
