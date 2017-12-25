namespace D365FONinjaDevExtensions.Parameters
{
    partial class Parameters
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
            this.SaveParameters = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ProjectExtensionTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SaveParameters
            // 
            this.SaveParameters.Location = new System.Drawing.Point(247, 154);
            this.SaveParameters.Name = "SaveParameters";
            this.SaveParameters.Size = new System.Drawing.Size(75, 23);
            this.SaveParameters.TabIndex = 0;
            this.SaveParameters.Text = "OK";
            this.SaveParameters.UseVisualStyleBackColor = true;
            this.SaveParameters.Click += new System.EventHandler(this.SaveParameters_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Project extension:";
            // 
            // ProjectExtensionTB
            // 
            this.ProjectExtensionTB.Location = new System.Drawing.Point(117, 20);
            this.ProjectExtensionTB.Name = "ProjectExtensionTB";
            this.ProjectExtensionTB.Size = new System.Drawing.Size(76, 20);
            this.ProjectExtensionTB.TabIndex = 2;
            // 
            // Parameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 189);
            this.Controls.Add(this.ProjectExtensionTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveParameters);
            this.Name = "Parameters";
            this.Text = "Parameters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveParameters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ProjectExtensionTB;
    }
}