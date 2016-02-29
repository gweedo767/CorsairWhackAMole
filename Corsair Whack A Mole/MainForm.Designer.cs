namespace Corsair_Whack_A_Mole
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.toggleButton = new System.Windows.Forms.Button();
            this.lblKeyboardType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Whack A Mole";
            // 
            // toggleButton
            // 
            this.toggleButton.Enabled = false;
            this.toggleButton.Location = new System.Drawing.Point(15, 64);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(75, 23);
            this.toggleButton.TabIndex = 2;
            this.toggleButton.Text = "Enable";
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.Click += new System.EventHandler(this.toggleButton_Click);
            // 
            // lblKeyboardType
            // 
            this.lblKeyboardType.AutoSize = true;
            this.lblKeyboardType.Location = new System.Drawing.Point(12, 22);
            this.lblKeyboardType.Name = "lblKeyboardType";
            this.lblKeyboardType.Size = new System.Drawing.Size(123, 13);
            this.lblKeyboardType.TabIndex = 3;
            this.lblKeyboardType.Text = "Keyboard Type: unkown";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 179);
            this.Controls.Add(this.lblKeyboardType);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.KeyDown += this.MainForm_KeyDown;
            this.Name = "MainForm";
            this.Text = "Whack A Mole";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button toggleButton;
        private System.Windows.Forms.Label lblKeyboardType;

    }
}

