
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
            labelFileName = new Label();
            buttonSetBackground = new Button();
            labelTitle = new Label();
            textBoxTitle = new TextBox();
            SuspendLayout();
            // 
            // labelFileName
            // 
            labelFileName.AutoSize = true;
            labelFileName.Location = new System.Drawing.Point(37, 68);
            labelFileName.Name = "labelFileName";
            labelFileName.Size = new System.Drawing.Size(76, 20);
            labelFileName.TabIndex = 1;
            labelFileName.Text = "File Name";
            // 
            // buttonSetBackground
            // 
            buttonSetBackground.Location = new System.Drawing.Point(370, 116);
            buttonSetBackground.Name = "buttonSetBackground";
            buttonSetBackground.Size = new System.Drawing.Size(183, 54);
            buttonSetBackground.TabIndex = 2;
            buttonSetBackground.Text = "Set Background";
            buttonSetBackground.UseVisualStyleBackColor = true;
            buttonSetBackground.Click += ButtonSetBackground_Click;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Location = new System.Drawing.Point(37, 32);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new System.Drawing.Size(38, 20);
            labelTitle.TabIndex = 3;
            labelTitle.Text = "Title";
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new System.Drawing.Point(137, 25);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new System.Drawing.Size(713, 27);
            textBoxTitle.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(920, 195);
            Controls.Add(textBoxTitle);
            Controls.Add(labelTitle);
            Controls.Add(buttonSetBackground);
            Controls.Add(labelFileName);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label labelFileName;
        private Button buttonSetBackground;
        private Label labelTitle;
        private TextBox textBoxTitle;
    }
}