﻿
namespace BingImageRipper
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
            button1 = new Button();
            label1 = new Label();
            buttonSetBackground = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(298, 12);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(144, 54);
            button1.TabIndex = 0;
            button1.Text = "Get Image";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 93);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(76, 20);
            label1.TabIndex = 1;
            label1.Text = "File Name";
            // 
            // buttonSetBackground
            // 
            buttonSetBackground.Location = new System.Drawing.Point(490, 12);
            buttonSetBackground.Name = "buttonSetBackground";
            buttonSetBackground.Size = new System.Drawing.Size(183, 54);
            buttonSetBackground.TabIndex = 2;
            buttonSetBackground.Text = "Set Background";
            buttonSetBackground.UseVisualStyleBackColor = true;
            buttonSetBackground.Click += ButtonSetBackground_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(920, 150);
            Controls.Add(buttonSetBackground);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button buttonSetBackground;
    }
}