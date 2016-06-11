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
    public partial class Loser : Form
    {
        public Loser(int pScore, int dScore, double b)
        {
            InitializeComponent();

            string message = "";
            if (pScore > 21) 
            {
                message = "You busted!";
            }
            else if (dScore == 21) 
            {
                message = "The dealer got BlackJack!";
            }
            else
            {
                message = "The dealer beat you by " + (dScore - pScore).ToString() + "!";
            }

            lblLoserMessage.Text = "Bust! You lost " + b.ToString("c") + "!\n"
                + "You had a score of: " + pScore + "\n"
                + "The dealer had a score of: " + dScore + "\n"
                + message;
        }

        private void Loser_Load(object sender, EventArgs e)
        {
            loserBox.Image = Image.FromFile("../cards/loser.jpg");
        }
    }
}
