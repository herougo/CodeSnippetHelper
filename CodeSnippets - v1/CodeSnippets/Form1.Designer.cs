namespace CodeSnippets
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
            this.tbInput = new System.Windows.Forms.TextBox();
            this.btnAddSnippet = new System.Windows.Forms.Button();
            this.btnDisplaySnippet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(30, 60);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(220, 20);
            this.tbInput.TabIndex = 0;
            // 
            // btnAddSnippet
            // 
            this.btnAddSnippet.Location = new System.Drawing.Point(148, 12);
            this.btnAddSnippet.Name = "btnAddSnippet";
            this.btnAddSnippet.Size = new System.Drawing.Size(102, 27);
            this.btnAddSnippet.TabIndex = 1;
            this.btnAddSnippet.Text = "Add";
            this.btnAddSnippet.UseVisualStyleBackColor = true;
            this.btnAddSnippet.Click += new System.EventHandler(this.btnAddSnippet_Click);
            // 
            // btnDisplaySnippet
            // 
            this.btnDisplaySnippet.Location = new System.Drawing.Point(30, 12);
            this.btnDisplaySnippet.Name = "btnDisplaySnippet";
            this.btnDisplaySnippet.Size = new System.Drawing.Size(102, 27);
            this.btnDisplaySnippet.TabIndex = 2;
            this.btnDisplaySnippet.Text = "Display";
            this.btnDisplaySnippet.UseVisualStyleBackColor = true;
            this.btnDisplaySnippet.Click += new System.EventHandler(this.btnDisplaySnippet_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnDisplaySnippet);
            this.Controls.Add(this.btnAddSnippet);
            this.Controls.Add(this.tbInput);
            this.Name = "Form1";
            this.Text = "Code Snippets";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Button btnAddSnippet;
        private System.Windows.Forms.Button btnDisplaySnippet;
    }
}

