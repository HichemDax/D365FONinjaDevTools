namespace D365FONinjaDevAddins.SetIndex
{
    partial class CreateIndex
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.IndexTxt = new System.Windows.Forms.TextBox();
            this.AllowDuplicate = new System.Windows.Forms.CheckBox();
            this.AlternateKey = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(210, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Index name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // IndexTxt
            // 
            this.IndexTxt.Location = new System.Drawing.Point(91, 20);
            this.IndexTxt.Name = "IndexTxt";
            this.IndexTxt.Size = new System.Drawing.Size(100, 20);
            this.IndexTxt.TabIndex = 2;
            // 
            // AllowDuplicate
            // 
            this.AllowDuplicate.AutoSize = true;
            this.AllowDuplicate.Location = new System.Drawing.Point(91, 47);
            this.AllowDuplicate.Name = "AllowDuplicate";
            this.AllowDuplicate.Size = new System.Drawing.Size(104, 17);
            this.AllowDuplicate.TabIndex = 3;
            this.AllowDuplicate.Text = "Allow Duplicates";
            this.AllowDuplicate.UseVisualStyleBackColor = true;
            // 
            // AlternateKey
            // 
            this.AlternateKey.AutoSize = true;
            this.AlternateKey.Location = new System.Drawing.Point(91, 70);
            this.AlternateKey.Name = "AlternateKey";
            this.AlternateKey.Size = new System.Drawing.Size(89, 17);
            this.AlternateKey.TabIndex = 4;
            this.AlternateKey.Text = "Alternate Key";
            this.AlternateKey.UseVisualStyleBackColor = true;
            // 
            // CreateIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 122);
            this.Controls.Add(this.AlternateKey);
            this.Controls.Add(this.AllowDuplicate);
            this.Controls.Add(this.IndexTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "CreateIndex";
            this.Text = "Create Index";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IndexTxt;
        private System.Windows.Forms.CheckBox AllowDuplicate;
        private System.Windows.Forms.CheckBox AlternateKey;
    }
}