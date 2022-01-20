﻿namespace RestClient
{
    partial class ClientForm
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
			this.ExecuteButton = new System.Windows.Forms.Button();
			this.UrlTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.VerbComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ResponseTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.RequestBodyTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// ExecuteButton
			// 
			this.ExecuteButton.Location = new System.Drawing.Point(453, 25);
			this.ExecuteButton.Name = "ExecuteButton";
			this.ExecuteButton.Size = new System.Drawing.Size(75, 23);
			this.ExecuteButton.TabIndex = 2;
			this.ExecuteButton.Text = "Execute";
			this.ExecuteButton.UseVisualStyleBackColor = true;
			this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
			// 
			// UrlTextBox
			// 
			this.UrlTextBox.Location = new System.Drawing.Point(115, 27);
			this.UrlTextBox.Name = "UrlTextBox";
			this.UrlTextBox.Size = new System.Drawing.Size(317, 20);
			this.UrlTextBox.TabIndex = 1;
			this.UrlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UrlTextBox_KeyDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(113, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "URL:";
			// 
			// VerbComboBox
			// 
			this.VerbComboBox.FormattingEnabled = true;
			this.VerbComboBox.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT",
            "DELETE",
            "HEAD",
            "OPTIONS"});
			this.VerbComboBox.Location = new System.Drawing.Point(12, 27);
			this.VerbComboBox.Name = "VerbComboBox";
			this.VerbComboBox.Size = new System.Drawing.Size(95, 21);
			this.VerbComboBox.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Verb:";
			// 
			// ResponseTextBox
			// 
			this.ResponseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ResponseTextBox.Location = new System.Drawing.Point(12, 225);
			this.ResponseTextBox.Multiline = true;
			this.ResponseTextBox.Name = "ResponseTextBox";
			this.ResponseTextBox.ReadOnly = true;
			this.ResponseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.ResponseTextBox.Size = new System.Drawing.Size(509, 209);
			this.ResponseTextBox.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 209);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Response:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(123, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Request Body (optional):";
			// 
			// RequestBodyTextBox
			// 
			this.RequestBodyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.RequestBodyTextBox.Location = new System.Drawing.Point(12, 82);
			this.RequestBodyTextBox.Multiline = true;
			this.RequestBodyTextBox.Name = "RequestBodyTextBox";
			this.RequestBodyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.RequestBodyTextBox.Size = new System.Drawing.Size(509, 113);
			this.RequestBodyTextBox.TabIndex = 7;
			// 
			// ClientForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(537, 446);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.RequestBodyTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ResponseTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.VerbComboBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.UrlTextBox);
			this.Controls.Add(this.ExecuteButton);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.Name = "ClientForm";
			this.Text = "Rest Client";
			this.Load += new System.EventHandler(this.ClientForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.TextBox UrlTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox VerbComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ResponseTextBox;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox RequestBodyTextBox;
    }
}

