using System;
using System.Windows.Forms;

namespace BlackJack
{
    /// <summary>
    /// A form box that displays when the round ends in a push
    /// </summary>
    public partial class PushMessage : Form
    {
        public PushMessage(int pScore, int dScore)
        {
            InitializeComponent();

            string message = "";
            if (pScore > 21 && dScore > 21)
            {
                message = "You both busted!";
            }
            else if (dScore == pScore)
            {
                message = "You tied the dealer!";
            }
            else
            {
                message = "Let's play again!";
            }

            lblPushMessage.Text = "Looks like this round was a Push!\n"
                + "You had a score of: " + pScore + "\n"
                + "The dealer had a score of: " + dScore + "\n"
                + message;
        }
    }
}
