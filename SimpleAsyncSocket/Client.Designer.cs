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
            this.btnLotNR = new System.Windows.Forms.Button();
            this.listBoxColors = new System.Windows.Forms.ListBox();
            this.listBoxProximities = new System.Windows.Forms.ListBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(414, 389);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(12, 128);
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
            this.textBox.Size = new System.Drawing.Size(184, 33);
            this.textBox.TabIndex = 2;
            // 
            // btnLot
            // 
            this.btnLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLot.Location = new System.Drawing.Point(12, 99);
            this.btnLot.Name = "btnLot";
            this.btnLot.Size = new System.Drawing.Size(75, 23);
            this.btnLot.TabIndex = 3;
            this.btnLot.Text = "Lot";
            this.btnLot.UseVisualStyleBackColor = true;
            this.btnLot.Click += new System.EventHandler(this.btnLot_Click);
            // 
            // btnLot1
            // 
            this.btnLot1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLot1.Location = new System.Drawing.Point(13, 70);
            this.btnLot1.Name = "btnLot1";
            this.btnLot1.Size = new System.Drawing.Size(75, 23);
            this.btnLot1.TabIndex = 4;
            this.btnLot1.Text = "Lot 1";
            this.btnLot1.UseVisualStyleBackColor = true;
            this.btnLot1.Click += new System.EventHandler(this.btnLot1_Click);
            // 
            // btnLot2
            // 
            this.btnLot2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLot2.Location = new System.Drawing.Point(94, 70);
            this.btnLot2.Name = "btnLot2";
            this.btnLot2.Size = new System.Drawing.Size(75, 23);
            this.btnLot2.TabIndex = 5;
            this.btnLot2.Text = "Simple Lot";
            this.btnLot2.UseVisualStyleBackColor = true;
            this.btnLot2.Click += new System.EventHandler(this.btnLot2_Click);
            // 
            // btnLotNR
            // 
            this.btnLotNR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLotNR.Location = new System.Drawing.Point(94, 99);
            this.btnLotNR.Name = "btnLotNR";
            this.btnLotNR.Size = new System.Drawing.Size(75, 23);
            this.btnLotNR.TabIndex = 6;
            this.btnLotNR.Text = "LotNR";
            this.btnLotNR.UseVisualStyleBackColor = true;
            this.btnLotNR.Click += new System.EventHandler(this.btnLotNR_Click);
            // 
            // listBoxColors
            // 
            this.listBoxColors.FormattingEnabled = true;
            this.listBoxColors.Items.AddRange(new object[] {
            "ALL",
            "BLUE",
            "RED",
            "BROWN",
            "YELLOW",
            "PURPLE"});
            this.listBoxColors.Location = new System.Drawing.Point(12, 196);
            this.listBoxColors.Name = "listBoxColors";
            this.listBoxColors.Size = new System.Drawing.Size(120, 82);
            this.listBoxColors.TabIndex = 7;
            // 
            // listBoxProximities
            // 
            this.listBoxProximities.FormattingEnabled = true;
            this.listBoxProximities.Items.AddRange(new object[] {
            "ALL",
            "CURRIS",
            "DORMS",
            "QUAD"});
            this.listBoxProximities.Location = new System.Drawing.Point(12, 284);
            this.listBoxProximities.Name = "listBoxProximities";
            this.listBoxProximities.Size = new System.Drawing.Size(120, 56);
            this.listBoxProximities.TabIndex = 8;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(138, 317);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 10;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(235, 196);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(100, 144);
            this.txtResults.TabIndex = 11;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 424);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.listBoxProximities);
            this.Controls.Add(this.listBoxColors);
            this.Controls.Add(this.btnLotNR);
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
        private System.Windows.Forms.Button btnLotNR;
        private System.Windows.Forms.ListBox listBoxColors;
        private System.Windows.Forms.ListBox listBoxProximities;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtResults;
    }
}

