namespace WinFormsAppCsh_net
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            buttonSendMsg = new Button();
            listMessages = new ListBox();
            textSendMsg = new TextBox();
            textIPadr = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textPort = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            buttonUDPstart = new Button();
            buttonTCPstart = new Button();
            buttonStop = new Button();
            SuspendLayout();
            // 
            // buttonSendMsg
            // 
            buttonSendMsg.Enabled = false;
            buttonSendMsg.Location = new Point(452, 573);
            buttonSendMsg.Name = "buttonSendMsg";
            buttonSendMsg.Size = new Size(126, 71);
            buttonSendMsg.TabIndex = 0;
            buttonSendMsg.Text = "buttonSendMsg";
            buttonSendMsg.UseVisualStyleBackColor = true;
            buttonSendMsg.Click += buttonSendMsg_Click;
            // 
            // listMessages
            // 
            listMessages.FormattingEnabled = true;
            listMessages.ItemHeight = 15;
            listMessages.Location = new Point(12, 12);
            listMessages.Name = "listMessages";
            listMessages.Size = new Size(566, 439);
            listMessages.TabIndex = 1;
            // 
            // textSendMsg
            // 
            textSendMsg.Location = new Point(12, 573);
            textSendMsg.Multiline = true;
            textSendMsg.Name = "textSendMsg";
            textSendMsg.Size = new Size(434, 71);
            textSendMsg.TabIndex = 2;
            // 
            // textIPadr
            // 
            textIPadr.Location = new Point(57, 457);
            textIPadr.Name = "textIPadr";
            textIPadr.Size = new Size(257, 23);
            textIPadr.TabIndex = 3;
            textIPadr.Text = "127.0.0.1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 460);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 4;
            label1.Text = "IP/adr:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(320, 460);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 5;
            label2.Text = "port:";
            // 
            // textPort
            // 
            textPort.Location = new Point(358, 457);
            textPort.Name = "textPort";
            textPort.Size = new Size(88, 23);
            textPort.TabIndex = 6;
            textPort.Text = "12345";
            // 
            // buttonUDPstart
            // 
            buttonUDPstart.Location = new Point(12, 486);
            buttonUDPstart.Name = "buttonUDPstart";
            buttonUDPstart.Size = new Size(138, 23);
            buttonUDPstart.TabIndex = 7;
            buttonUDPstart.Text = "buttonUDPstart";
            buttonUDPstart.UseVisualStyleBackColor = true;
            buttonUDPstart.Click += buttonUDPstart_Click;
            // 
            // buttonTCPstart
            // 
            buttonTCPstart.Location = new Point(156, 486);
            buttonTCPstart.Name = "buttonTCPstart";
            buttonTCPstart.Size = new Size(138, 23);
            buttonTCPstart.TabIndex = 8;
            buttonTCPstart.Text = "buttonTCPstart";
            buttonTCPstart.UseVisualStyleBackColor = true;
            buttonTCPstart.Click += buttonTCPstart_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(336, 486);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(110, 23);
            buttonStop.TabIndex = 9;
            buttonStop.Text = "buttonStop";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 742);
            Controls.Add(buttonStop);
            Controls.Add(buttonTCPstart);
            Controls.Add(buttonUDPstart);
            Controls.Add(textPort);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textIPadr);
            Controls.Add(textSendMsg);
            Controls.Add(listMessages);
            Controls.Add(buttonSendMsg);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonSendMsg;
        private ListBox listMessages;
        private TextBox textSendMsg;
        private TextBox textIPadr;
        private Label label1;
        private Label label2;
        private TextBox textPort;
        private System.Windows.Forms.Timer timer1;
        private Button buttonUDPstart;
        private Button buttonTCPstart;
        private Button buttonStop;
    }
}
