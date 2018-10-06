namespace BraveNewWorld
{
    partial class Credits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Credits));
            this.labelProgrammers = new System.Windows.Forms.Label();
            this.labelTip = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelProgrammers
            // 
            this.labelProgrammers.AutoSize = true;
            this.labelProgrammers.Location = new System.Drawing.Point(13, 13);
            this.labelProgrammers.Name = "labelProgrammers";
            this.labelProgrammers.Size = new System.Drawing.Size(355, 13);
            this.labelProgrammers.TabIndex = 0;
            this.labelProgrammers.Text = "Brave New World was written by Jonah Posey and Seth G. R. Herendeen";
            // 
            // labelTip
            // 
            this.labelTip.AutoSize = true;
            this.labelTip.Location = new System.Drawing.Point(16, 37);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new System.Drawing.Size(248, 13);
            this.labelTip.TabIndex = 2;
            this.labelTip.Text = "*You can select an item on the list and press ctrl - c";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 54);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(348, 262);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // Credits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 328);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelTip);
            this.Controls.Add(this.labelProgrammers);
            this.Location = new System.Drawing.Point(389, 366);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Credits";
            this.Text = "Credits";
            this.Load += new System.EventHandler(this.Credits_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProgrammers;
        private System.Windows.Forms.Label labelTip;
        private System.Windows.Forms.TextBox textBox1;
    }
}