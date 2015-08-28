using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Push : Form
    {
        public Push(int pScore, int dScore)
        {
            InitializeComponent();

            string message = "";
            if (pScore > 21 && dScore > 21)
                message = "You both busted!";
            else if (dScore == pScore)
                message = "You tied the dealer!";
            else
                message = "Let's play again!";

            lblPushMessage.Text = "Looks like this round was a Push!\n"
                + "You had a score of: " + pScore + "\n"
                + "The dealer had a score of: " + dScore + "\n"
                + message;
        }
    }
}
