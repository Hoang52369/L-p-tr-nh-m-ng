namespace Lab4Client
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(728, 18);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(112, 35);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(18, 63);
            this.txtMessages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(769, 519);
            this.txtMessages.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(18, 594);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(769, 26);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(820, 585);
            this.btnSendMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(140, 35);
            this.btnSendMessage.TabIndex = 3;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(129, 18);
            this.txtServerIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(304, 26);
            this.txtServerIP.TabIndex = 4;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(513, 18);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(148, 26);
            this.txtPort.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Server IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(444, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Port:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 672);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.btnConnect);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Chat Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        #endregion
    }
}

