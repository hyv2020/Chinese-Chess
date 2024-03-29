﻿
namespace ChineseChess
{
    partial class Game
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.QuitButton = new System.Windows.Forms.Button();
            this.TurnLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TurnBox = new System.Windows.Forms.ComboBox();
            this.RestartButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveFileNameTextBox = new System.Windows.Forms.TextBox();
            this.AutoSaveBox = new System.Windows.Forms.CheckBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // QuitButton
            // 
            this.QuitButton.Location = new System.Drawing.Point(937, 48);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(229, 64);
            this.QuitButton.TabIndex = 0;
            this.QuitButton.Text = "Quit Game";
            this.QuitButton.UseVisualStyleBackColor = true;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // TurnLabel
            // 
            this.TurnLabel.AutoSize = true;
            this.TurnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.TurnLabel.Location = new System.Drawing.Point(929, 144);
            this.TurnLabel.Name = "TurnLabel";
            this.TurnLabel.Size = new System.Drawing.Size(219, 48);
            this.TurnLabel.TabIndex = 1;
            this.TurnLabel.Text = "Turn Label";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(906, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Click on chess to show available moves.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(906, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(235, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Click on board to clear moves.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(906, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(288, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "Click on move square to move chess.";
            // 
            // TurnBox
            // 
            this.TurnBox.FormattingEnabled = true;
            this.TurnBox.Location = new System.Drawing.Point(910, 309);
            this.TurnBox.Name = "TurnBox";
            this.TurnBox.Size = new System.Drawing.Size(231, 24);
            this.TurnBox.TabIndex = 10;
            this.TurnBox.SelectedIndexChanged += new System.EventHandler(this.TurnBox_SelectedIndexChanged);
            // 
            // RestartButton
            // 
            this.RestartButton.Location = new System.Drawing.Point(912, 597);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(229, 64);
            this.RestartButton.TabIndex = 11;
            this.RestartButton.Text = "Restart";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(912, 457);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(229, 64);
            this.SaveButton.TabIndex = 12;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveFileNameTextBox
            // 
            this.SaveFileNameTextBox.Location = new System.Drawing.Point(910, 409);
            this.SaveFileNameTextBox.Name = "SaveFileNameTextBox";
            this.SaveFileNameTextBox.Size = new System.Drawing.Size(231, 22);
            this.SaveFileNameTextBox.TabIndex = 13;
            // 
            // AutoSaveBox
            // 
            this.AutoSaveBox.AutoSize = true;
            this.AutoSaveBox.Location = new System.Drawing.Point(910, 383);
            this.AutoSaveBox.Name = "AutoSaveBox";
            this.AutoSaveBox.Size = new System.Drawing.Size(91, 20);
            this.AutoSaveBox.TabIndex = 14;
            this.AutoSaveBox.Text = "Auto Save";
            this.AutoSaveBox.UseVisualStyleBackColor = true;
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(912, 527);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(229, 64);
            this.LoadButton.TabIndex = 15;
            this.LoadButton.Text = "Load Save";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.AutoSaveBox);
            this.Controls.Add(this.SaveFileNameTextBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.RestartButton);
            this.Controls.Add(this.TurnBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TurnLabel);
            this.Controls.Add(this.QuitButton);
            this.Name = "Game";
            this.Text = "Chinese Chess";
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button QuitButton;
        private System.Windows.Forms.Label TurnLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox TurnBox;
        private System.Windows.Forms.Button RestartButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox SaveFileNameTextBox;
        private System.Windows.Forms.CheckBox AutoSaveBox;
        private System.Windows.Forms.Button LoadButton;
    }
}