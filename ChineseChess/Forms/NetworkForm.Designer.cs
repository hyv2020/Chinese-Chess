namespace ChineseChess
{
    partial class NetworkForm
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
            this.HostGameButton = new System.Windows.Forms.Button();
            this.JoinGameButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ServerIPTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // HostGameButton
            // 
            this.HostGameButton.Location = new System.Drawing.Point(72, 285);
            this.HostGameButton.Name = "HostGameButton";
            this.HostGameButton.Size = new System.Drawing.Size(169, 96);
            this.HostGameButton.TabIndex = 0;
            this.HostGameButton.Text = "Host Game";
            this.HostGameButton.UseVisualStyleBackColor = true;
            this.HostGameButton.Click += new System.EventHandler(this.HostGameButton_Click);
            // 
            // JoinGameButton
            // 
            this.JoinGameButton.Location = new System.Drawing.Point(298, 285);
            this.JoinGameButton.Name = "JoinGameButton";
            this.JoinGameButton.Size = new System.Drawing.Size(169, 96);
            this.JoinGameButton.TabIndex = 1;
            this.JoinGameButton.Text = "Join Game";
            this.JoinGameButton.UseVisualStyleBackColor = true;
            this.JoinGameButton.Click += new System.EventHandler(this.JoinGameButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(517, 285);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(169, 96);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ServerIPTextBox
            // 
            this.ServerIPTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerIPTextBox.Location = new System.Drawing.Point(225, 127);
            this.ServerIPTextBox.Name = "ServerIPTextBox";
            this.ServerIPTextBox.Size = new System.Drawing.Size(265, 27);
            this.ServerIPTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Server IP Address";
            // 
            // NetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerIPTextBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.JoinGameButton);
            this.Controls.Add(this.HostGameButton);
            this.Name = "NetworkForm";
            this.Text = "Chinese Chess";
            this.Load += new System.EventHandler(this.NetworkForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button HostGameButton;
        private System.Windows.Forms.Button JoinGameButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TextBox ServerIPTextBox;
        private System.Windows.Forms.Label label1;
    }
}