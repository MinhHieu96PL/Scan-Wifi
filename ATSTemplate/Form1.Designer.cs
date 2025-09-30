namespace ATSTemplate
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSFCS = new System.Windows.Forms.Button();
            this.rtbSFCS = new System.Windows.Forms.RichTextBox();
            this.btnTestMode = new System.Windows.Forms.Button();
            this.lbServerIP = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.lbPN = new System.Windows.Forms.Label();
            this.lbPCName = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.failedListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbBuildName = new System.Windows.Forms.Label();
            this.lblA1 = new System.Windows.Forms.Label();
            this.lblA2 = new System.Windows.Forms.Label();
            this.lblA3 = new System.Windows.Forms.Label();
            this.lblB1 = new System.Windows.Forms.Label();
            this.lblB2 = new System.Windows.Forms.Label();
            this.lblB3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnSFCS);
            this.panel1.Controls.Add(this.rtbSFCS);
            this.panel1.Controls.Add(this.btnTestMode);
            this.panel1.Controls.Add(this.lbServerIP);
            this.panel1.Controls.Add(this.lbType);
            this.panel1.Controls.Add(this.lbPN);
            this.panel1.Controls.Add(this.lbPCName);
            this.panel1.Location = new System.Drawing.Point(0, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1029, 133);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ATSTemplate.Properties.Resources.QR_Eric;
            this.pictureBox1.Location = new System.Drawing.Point(895, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 126);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnSFCS
            // 
            this.btnSFCS.BackColor = System.Drawing.Color.Lime;
            this.btnSFCS.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSFCS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnSFCS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnSFCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSFCS.Location = new System.Drawing.Point(463, 70);
            this.btnSFCS.Name = "btnSFCS";
            this.btnSFCS.Size = new System.Drawing.Size(100, 60);
            this.btnSFCS.TabIndex = 4;
            this.btnSFCS.Text = "SFCS ON";
            this.btnSFCS.UseVisualStyleBackColor = false;
            this.btnSFCS.Click += new System.EventHandler(this.btnSFCS_Click);
            // 
            // rtbSFCS
            // 
            this.rtbSFCS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbSFCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbSFCS.Location = new System.Drawing.Point(571, 4);
            this.rtbSFCS.Name = "rtbSFCS";
            this.rtbSFCS.Size = new System.Drawing.Size(318, 126);
            this.rtbSFCS.TabIndex = 3;
            this.rtbSFCS.Text = "";
            this.rtbSFCS.Click += new System.EventHandler(this.rtbSFCS_Click);
            this.rtbSFCS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbSFCS_KeyDown);
            // 
            // btnTestMode
            // 
            this.btnTestMode.BackColor = System.Drawing.Color.Tomato;
            this.btnTestMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestMode.ForeColor = System.Drawing.Color.Black;
            this.btnTestMode.Location = new System.Drawing.Point(463, 4);
            this.btnTestMode.Name = "btnTestMode";
            this.btnTestMode.Size = new System.Drawing.Size(100, 60);
            this.btnTestMode.TabIndex = 2;
            this.btnTestMode.Text = "DEBUG";
            this.btnTestMode.UseVisualStyleBackColor = false;
            this.btnTestMode.Click += new System.EventHandler(this.btnTestMode_Click);
            // 
            // lbServerIP
            // 
            this.lbServerIP.BackColor = System.Drawing.Color.Gray;
            this.lbServerIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbServerIP.ForeColor = System.Drawing.Color.White;
            this.lbServerIP.Location = new System.Drawing.Point(3, 70);
            this.lbServerIP.Name = "lbServerIP";
            this.lbServerIP.Size = new System.Drawing.Size(223, 60);
            this.lbServerIP.TabIndex = 1;
            this.lbServerIP.Text = "10.90.x.x";
            this.lbServerIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbType
            // 
            this.lbType.BackColor = System.Drawing.Color.Gray;
            this.lbType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbType.ForeColor = System.Drawing.Color.White;
            this.lbType.Location = new System.Drawing.Point(232, 70);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(223, 60);
            this.lbType.TabIndex = 1;
            this.lbType.Text = "Normal";
            this.lbType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPN
            // 
            this.lbPN.BackColor = System.Drawing.Color.Gray;
            this.lbPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPN.ForeColor = System.Drawing.Color.White;
            this.lbPN.Location = new System.Drawing.Point(232, 4);
            this.lbPN.Name = "lbPN";
            this.lbPN.Size = new System.Drawing.Size(223, 60);
            this.lbPN.TabIndex = 1;
            this.lbPN.Text = "820-01100";
            this.lbPN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPCName
            // 
            this.lbPCName.BackColor = System.Drawing.Color.Gray;
            this.lbPCName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPCName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPCName.ForeColor = System.Drawing.Color.White;
            this.lbPCName.Location = new System.Drawing.Point(3, 4);
            this.lbPCName.Name = "lbPCName";
            this.lbPCName.Size = new System.Drawing.Size(223, 60);
            this.lbPCName.TabIndex = 0;
            this.lbPCName.Text = "MBFT-7103";
            this.lbPCName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.failedListToolStripMenuItem,
            this.cableToolStripMenuItem,
            this.networkConnectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1029, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // failedListToolStripMenuItem
            // 
            this.failedListToolStripMenuItem.Name = "failedListToolStripMenuItem";
            this.failedListToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.failedListToolStripMenuItem.Text = "Failed List";
            this.failedListToolStripMenuItem.Click += new System.EventHandler(this.failedListToolStripMenuItem_Click);
            // 
            // cableToolStripMenuItem
            // 
            this.cableToolStripMenuItem.Name = "cableToolStripMenuItem";
            this.cableToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.cableToolStripMenuItem.Text = "Cable";
            this.cableToolStripMenuItem.Click += new System.EventHandler(this.cableToolStripMenuItem_Click);
            // 
            // networkConnectionToolStripMenuItem
            // 
            this.networkConnectionToolStripMenuItem.Name = "networkConnectionToolStripMenuItem";
            this.networkConnectionToolStripMenuItem.Size = new System.Drawing.Size(129, 20);
            this.networkConnectionToolStripMenuItem.Text = "Network Connection";
            this.networkConnectionToolStripMenuItem.Click += new System.EventHandler(this.networkConnectionToolStripMenuItem_Click);
            // 
            // lbBuildName
            // 
            this.lbBuildName.BackColor = System.Drawing.Color.Silver;
            this.lbBuildName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBuildName.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBuildName.ForeColor = System.Drawing.Color.Black;
            this.lbBuildName.Location = new System.Drawing.Point(0, 24);
            this.lbBuildName.Name = "lbBuildName";
            this.lbBuildName.Size = new System.Drawing.Size(1029, 86);
            this.lbBuildName.TabIndex = 1;
            this.lbBuildName.Text = "PVT";
            this.lbBuildName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblA1
            // 
            this.lblA1.BackColor = System.Drawing.Color.LightBlue;
            this.lblA1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblA1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblA1.Location = new System.Drawing.Point(5, 252);
            this.lblA1.Name = "lblA1";
            this.lblA1.Size = new System.Drawing.Size(335, 215);
            this.lblA1.TabIndex = 0;
            this.lblA1.Text = "A1";
            this.lblA1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblA2
            // 
            this.lblA2.BackColor = System.Drawing.Color.LightBlue;
            this.lblA2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblA2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblA2.Location = new System.Drawing.Point(346, 252);
            this.lblA2.Name = "lblA2";
            this.lblA2.Size = new System.Drawing.Size(335, 215);
            this.lblA2.TabIndex = 0;
            this.lblA2.Text = "A2";
            this.lblA2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblA3
            // 
            this.lblA3.BackColor = System.Drawing.Color.LightBlue;
            this.lblA3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblA3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblA3.Location = new System.Drawing.Point(687, 252);
            this.lblA3.Name = "lblA3";
            this.lblA3.Size = new System.Drawing.Size(335, 215);
            this.lblA3.TabIndex = 0;
            this.lblA3.Text = "A3";
            this.lblA3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblB1
            // 
            this.lblB1.BackColor = System.Drawing.Color.LightBlue;
            this.lblB1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblB1.Location = new System.Drawing.Point(5, 473);
            this.lblB1.Name = "lblB1";
            this.lblB1.Size = new System.Drawing.Size(335, 215);
            this.lblB1.TabIndex = 0;
            this.lblB1.Text = "B1";
            this.lblB1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblB2
            // 
            this.lblB2.BackColor = System.Drawing.Color.LightBlue;
            this.lblB2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblB2.Location = new System.Drawing.Point(346, 473);
            this.lblB2.Name = "lblB2";
            this.lblB2.Size = new System.Drawing.Size(335, 215);
            this.lblB2.TabIndex = 0;
            this.lblB2.Text = "B2";
            this.lblB2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblB3
            // 
            this.lblB3.BackColor = System.Drawing.Color.LightBlue;
            this.lblB3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblB3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblB3.Location = new System.Drawing.Point(687, 473);
            this.lblB3.Name = "lblB3";
            this.lblB3.Size = new System.Drawing.Size(335, 215);
            this.lblB3.TabIndex = 0;
            this.lblB3.Text = "B3";
            this.lblB3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1029, 694);
            this.Controls.Add(this.lbBuildName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblB3);
            this.Controls.Add(this.lblA3);
            this.Controls.Add(this.lblB2);
            this.Controls.Add(this.lblB1);
            this.Controls.Add(this.lblA2);
            this.Controls.Add(this.lblA1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSFCS;
        private System.Windows.Forms.RichTextBox rtbSFCS;
        private System.Windows.Forms.Button btnTestMode;
        private System.Windows.Forms.Label lbPN;
        private System.Windows.Forms.Label lbPCName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lbBuildName;
        private System.Windows.Forms.ToolStripMenuItem failedListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cableToolStripMenuItem;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.ToolStripMenuItem networkConnectionToolStripMenuItem;
        private System.Windows.Forms.Label lbServerIP;
        private System.Windows.Forms.Label lblA1;
        private System.Windows.Forms.Label lblA2;
        private System.Windows.Forms.Label lblA3;
        private System.Windows.Forms.Label lblB1;
        private System.Windows.Forms.Label lblB2;
        private System.Windows.Forms.Label lblB3;
    }
}

