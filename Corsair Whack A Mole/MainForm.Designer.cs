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
            this.radioWhackAMole = new System.Windows.Forms.RadioButton();
            this.radioLightsOut = new System.Windows.Forms.RadioButton();
            this.pauseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Keyboard Games";
            // 
            // toggleButton
            // 
            this.toggleButton.Enabled = false;
            this.toggleButton.Location = new System.Drawing.Point(12, 144);
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
            this.lblKeyboardType.Location = new System.Drawing.Point(9, 41);
            this.lblKeyboardType.Name = "lblKeyboardType";
            this.lblKeyboardType.Size = new System.Drawing.Size(123, 13);
            this.lblKeyboardType.TabIndex = 3;
            this.lblKeyboardType.Text = "Keyboard Type: unkown";
            // 
            // radioWhackAMole
            // 
            this.radioWhackAMole.AutoSize = true;
            this.radioWhackAMole.Checked = true;
            this.radioWhackAMole.Location = new System.Drawing.Point(12, 68);
            this.radioWhackAMole.Name = "radioWhackAMole";
            this.radioWhackAMole.Size = new System.Drawing.Size(96, 17);
            this.radioWhackAMole.TabIndex = 4;
            this.radioWhackAMole.TabStop = true;
            this.radioWhackAMole.Text = "Whack A Mole";
            this.radioWhackAMole.UseVisualStyleBackColor = true;
            this.radioWhackAMole.CheckedChanged += new System.EventHandler(this.gameSelect_Changed);
            // 
            // radioLightsOut
            // 
            this.radioLightsOut.AutoSize = true;
            this.radioLightsOut.Location = new System.Drawing.Point(12, 91);
            this.radioLightsOut.Name = "radioLightsOut";
            this.radioLightsOut.Size = new System.Drawing.Size(73, 17);
            this.radioLightsOut.TabIndex = 5;
            this.radioLightsOut.Text = "Lights Out";
            this.radioLightsOut.UseVisualStyleBackColor = true;
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(94, 144);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 6;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 179);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.radioLightsOut);
            this.Controls.Add(this.radioWhackAMole);
            this.Controls.Add(this.lblKeyboardType);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "RGB Keyboard Games";
            this.Activated += new System.EventHandler(this.formFocused);
            this.Deactivate += new System.EventHandler(this.formDefocused);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button toggleButton;
        private System.Windows.Forms.Label lblKeyboardType;
        private System.Windows.Forms.RadioButton radioWhackAMole;
        private System.Windows.Forms.RadioButton radioLightsOut;
        private System.Windows.Forms.Button pauseButton;
    }
}

