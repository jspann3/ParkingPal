namespace TCPServer
{
    partial class Server
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnFillTestLots = new System.Windows.Forms.Button();
            this.btnErrorChk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.AcceptsReturn = true;
            this.textBox.AcceptsTab = true;
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(13, 13);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(259, 206);
            this.textBox.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(197, 223);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnFillTestLots
            // 
            this.btnFillTestLots.Location = new System.Drawing.Point(13, 223);
            this.btnFillTestLots.Name = "btnFillTestLots";
            this.btnFillTestLots.Size = new System.Drawing.Size(75, 23);
            this.btnFillTestLots.TabIndex = 2;
            this.btnFillTestLots.Text = "Fill Test Lots";
            this.btnFillTestLots.UseVisualStyleBackColor = true;
            this.btnFillTestLots.Click += new System.EventHandler(this.btnFillTestLots_Click);
            // 
            // btnErrorChk
            // 
            this.btnErrorChk.Location = new System.Drawing.Point(95, 222);
            this.btnErrorChk.Name = "btnErrorChk";
            this.btnErrorChk.Size = new System.Drawing.Size(96, 23);
            this.btnErrorChk.TabIndex = 3;
            this.btnErrorChk.Text = "Run Error Check";
            this.btnErrorChk.UseVisualStyleBackColor = true;
            this.btnErrorChk.Click += new System.EventHandler(this.btnErrorChk_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 258);
            this.Controls.Add(this.btnErrorChk);
            this.Controls.Add(this.btnFillTestLots);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.textBox);
            this.Name = "Server";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnFillTestLots;
        private System.Windows.Forms.Button btnErrorChk;
    }
}

