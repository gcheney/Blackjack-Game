using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace BlackJack
{
    public partial class Winner : Form
    {
        public Winner(int pScore, int dScore, double b)
        {
            InitializeComponent();

            string message = "";
            if (dScore > 21)
                message = "The dealer busted!";
            else if (pScore == 21)
                message = "You got BlackJack!";
            else
                message = "You beat the dealer by " + (pScore - dScore).ToString() + "!";

            lblWinnerMessage.Text = "Congratulations, you won " + b.ToString("c") + "!\n"
                + "You had a score of: " + pScore + "\n"
                + "The dealer had a score of: " + dScore + "\n"
                + message;
        }

        private void Winner_Load(object sender, EventArgs e)
        {
            winnerBox.Image = Image.FromFile("../cards/winner.jpg");
        }
    }
}
