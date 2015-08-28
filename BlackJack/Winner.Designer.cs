namespace BlackJack
{
    partial class Winner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.lblWinnerMessage = new System.Windows.Forms.Label();
            this.winnerBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.winnerBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(175, 520);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(172, 37);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Awesome!";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblWinnerMessage
            // 
            this.lblWinnerMessage.AutoSize = true;
            this.lblWinnerMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWinnerMessage.Location = new System.Drawing.Point(90, 9);
            this.lblWinnerMessage.Name = "lblWinnerMessage";
            this.lblWinnerMessage.Size = new System.Drawing.Size(402, 25);
            this.lblWinnerMessage.TabIndex = 1;
            this.lblWinnerMessage.Text = "Congratulations! You won this round!";
            // 
            // winnerBox
            // 
            this.winnerBox.Location = new System.Drawing.Point(123, 164);
            this.winnerBox.Name = "winnerBox";
            this.winnerBox.Size = new System.Drawing.Size(306, 350);
            this.winnerBox.TabIndex = 2;
            this.winnerBox.TabStop = false;
            // 
            // Winner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 592);
            this.Controls.Add(this.winnerBox);
            this.Controls.Add(this.lblWinnerMessage);
            this.Controls.Add(this.btnOK);
            this.Name = "Winner";
            this.Text = "Winner";
            this.Load += new System.EventHandler(this.Winner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.winnerBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblWinnerMessage;
        private System.Windows.Forms.PictureBox winnerBox;
    }
}