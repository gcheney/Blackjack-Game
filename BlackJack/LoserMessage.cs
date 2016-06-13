using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlackJack
{
    /// <summary>
    /// The Form that displays when the player loses a round
    /// </summary>
    public partial class LoserMessage : Form
    {
        /// <summary>
        /// Initializes the LoserMessage form
        /// </summary>
        /// <param name="playerScore">The players score</param>
        /// <param name="dealerScore">The dealers score</param>
        /// <param name="bet">Teh current bet</param>
        public LoserMessage(int playerScore, int dealerScore, double bet)
        {
            InitializeComponent();

            var message = "";
            if (playerScore > 21)
            {
                message = "You busted!";
            }
            else if (dealerScore == 21)
            {
                message = "The dealer got BlackJack!";
            }
            else
            {
                message = "The dealer beat you by " 
                    + (dealerScore - playerScore).ToString() + "!";
            }

            lblLoserMessage.Text = "Bust! You lost " + bet.ToString("c") + "!\n"
                + "You had a score of: " + playerScore + "\n"
                + "The dealer had a score of: " + dealerScore + "\n"
                + message;
        }

        private void Loser_Load(object sender, EventArgs e)
        {
            var imgFile = "../cards/loser.jpg";
            loserBox.Image = Image.FromFile(imgFile);
        }
    }
}
