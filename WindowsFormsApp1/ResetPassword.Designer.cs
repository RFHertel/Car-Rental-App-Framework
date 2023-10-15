namespace CarRentalApp
{
    partial class ResetPassword
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
            this.lblEnterNewPassword = new System.Windows.Forms.Label();
            this.tbEnterNewPassword = new System.Windows.Forms.TextBox();
            this.tbConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEnterNewPassword
            // 
            this.lblEnterNewPassword.AutoSize = true;
            this.lblEnterNewPassword.Location = new System.Drawing.Point(111, 35);
            this.lblEnterNewPassword.Name = "lblEnterNewPassword";
            this.lblEnterNewPassword.Size = new System.Drawing.Size(106, 13);
            this.lblEnterNewPassword.TabIndex = 0;
            this.lblEnterNewPassword.Text = "Enter New Password";
            // 
            // tbEnterNewPassword
            // 
            this.tbEnterNewPassword.Location = new System.Drawing.Point(109, 67);
            this.tbEnterNewPassword.Name = "tbEnterNewPassword";
            this.tbEnterNewPassword.PasswordChar = '*';
            this.tbEnterNewPassword.Size = new System.Drawing.Size(118, 20);
            this.tbEnterNewPassword.TabIndex = 1;
            // 
            // tbConfirmPassword
            // 
            this.tbConfirmPassword.Location = new System.Drawing.Point(109, 136);
            this.tbConfirmPassword.Name = "tbConfirmPassword";
            this.tbConfirmPassword.PasswordChar = '*';
            this.tbConfirmPassword.Size = new System.Drawing.Size(118, 20);
            this.tbConfirmPassword.TabIndex = 3;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(121, 107);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(91, 13);
            this.lblConfirmPassword.TabIndex = 2;
            this.lblConfirmPassword.Text = "Confirm Password";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(119, 174);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(98, 53);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ResetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 275);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tbConfirmPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.tbEnterNewPassword);
            this.Controls.Add(this.lblEnterNewPassword);
            this.Name = "ResetPassword";
            this.Text = "Reset Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEnterNewPassword;
        private System.Windows.Forms.TextBox tbEnterNewPassword;
        private System.Windows.Forms.TextBox tbConfirmPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.Button btnReset;
    }
}