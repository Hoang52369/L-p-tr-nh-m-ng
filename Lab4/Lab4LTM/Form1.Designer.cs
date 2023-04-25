namespace Lab4LTM
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
            this.btnStartServer = new System.Windows.Forms.Button();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(18, 18);
            this.btnStartServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(180, 35);
            this.btnStartServer.TabIndex = 0;
            this.btnStartServer.Text = "Khởi động máy chủ";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(18, 63);
            this.txtMessages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(665, 519);
            this.txtMessages.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(18, 594);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(665, 26);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(703, 585);
            this.btnSendMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(140, 35);
            this.btnSendMessage.TabIndex = 3;
            this.btnSendMessage.Text = "Gửi tin nhắn";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 636);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.btnStartServer);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Chat Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSendMessage;

        #endregion
    }
}

