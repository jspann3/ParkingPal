namespace SimpleAsyncSocket
{
    partial class Client
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.btnLot = new System.Windows.Forms.Button();
            this.btnLot1 = new System.Windows.Forms.Button();
            this.btnLot2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(197, 226);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(116, 226);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(13, 13);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(259, 85);
            this.textBox.TabIndex = 2;
            // 
            // btnLot
            // 
            this.btnLot.Location = new System.Drawing.Point(13, 226);
            this.btnLot.Name = "btnLot";
            this.btnLot.Size = new System.Drawing.Size(75, 23);
            this.btnLot.TabIndex = 3;
            this.btnLot.Text = "Lot";
            this.btnLot.UseVisualStyleBackColor = true;
            this.btnLot.Click += new System.EventHandler(this.btnLot_Click);
            // 
            // btnLot1
            // 
            this.btnLot1.Location = new System.Drawing.Point(13, 105);
            this.btnLot1.Name = "btnLot1";
            this.btnLot1.Size = new System.Drawing.Size(75, 23);
            this.btnLot1.TabIndex = 4;
            this.btnLot1.Text = "Lot 1";
            this.btnLot1.UseVisualStyleBackColor = true;
            this.btnLot1.Click += new System.EventHandler(this.btnLot1_Click);
            // 
            // btnLot2
            // 
            this.btnLot2.Location = new System.Drawing.Point(94, 105);
            this.btnLot2.Name = "btnLot2";
            this.btnLot2.Size = new System.Drawing.Size(75, 23);
            this.btnLot2.TabIndex = 5;
            this.btnLot2.Text = "Simple Lot";
            this.btnLot2.UseVisualStyleBackColor = true;
            this.btnLot2.Click += new System.EventHandler(this.btnLot2_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnLot2);
            this.Controls.Add(this.btnLot1);
            this.Controls.Add(this.btnLot);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnect);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button btnLot;
        private System.Windows.Forms.Button btnLot1;
        private System.Windows.Forms.Button btnLot2;
    }
}

