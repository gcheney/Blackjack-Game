namespace BlackJack
{
    partial class LoserMessage
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
            this.lblLoserMessage = new System.Windows.Forms.Label();
            this.loserBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.loserBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLoserMessage
            // 
            this.lblLoserMessage.AutoSize = true;
            this.lblLoserMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoserMessage.Location = new System.Drawing.Point(85, 9);
            this.lblLoserMessage.Name = "lblLoserMessage";
            this.lblLoserMessage.Size = new System.Drawing.Size(288, 25);
            this.lblLoserMessage.TabIndex = 0;
            this.lblLoserMessage.Text = "Sorry! You lost this round!";
            // 
            // loserBox
            // 
            this.loserBox.Location = new System.Drawing.Point(73, 187);
            this.loserBox.Name = "loserBox";
            this.loserBox.Size = new System.Drawing.Size(325, 300);
            this.loserBox.TabIndex = 1;
            this.loserBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(128, 512);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(204, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "Dang it!";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Loser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 578);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loserBox);
            this.Controls.Add(this.lblLoserMessage);
            this.Name = "Loser";
            this.Text = "Loser";
            this.Load += new System.EventHandler(this.Loser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loserBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLoserMessage;
        private System.Windows.Forms.PictureBox loserBox;
        private System.Windows.Forms.Button button1;
    }
}