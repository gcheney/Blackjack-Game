using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace BlackJack
{
    /// <summary>
    /// A form that displays when the player wins the current round
    /// </summary>
    public partial class WinnerMessage : Form
    {
        /// <summary>
        /// Initializes the WinnerMessage form
        /// </summary>
        /// <param name="playerScore">The players score</param>
        /// <param name="dealerScore">The dealers score</param>
        /// <param name="bet">Teh current bet</param>
        public WinnerMessage(int playerScore, int dealerScore, double bet)
        {
            InitializeComponent();

            string message = "";
            if (dealerScore > 21)
            {
                message = "The dealer busted!";
            }
            else if (playerScore == 21)
            {
                message = "You got BlackJack!";
            }
            else
            {
                message = "You beat the dealer by "
                    + (playerScore - dealerScore).ToString() + "!";
            }

            lblWinnerMessage.Text = "Congratulations, you won " 
                + bet.ToString("c") + "!\n"
                + "You had a score of: " + playerScore + "\n"
                + "The dealer had a score of: " + dealerScore + "\n"
                + message;
        }

        private void Winner_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string imagePath = "BlackJack.Resources.winner.jpg";
            Stream stream = assembly.GetManifestResourceStream(imagePath);
            Image winnerImage = new Bitmap(stream);
            winnerBox.Image = winnerImage;
        }
    }
}
