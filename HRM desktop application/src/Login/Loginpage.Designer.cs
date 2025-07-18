namespace HRM_desktop_application
{
    partial class Loginpage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loginpage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.login_pictureBox = new System.Windows.Forms.PictureBox();
            this.lblEmployeeManagementSystem = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblloginAccount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.login_username = new System.Windows.Forms.TextBox();
            this.login_password = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.login_showPass = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.login_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(154)))), ((int)(((byte)(214)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.login_pictureBox);
            this.panel1.Controls.Add(this.lblEmployeeManagementSystem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 400);
            this.panel1.TabIndex = 0;
            // 
            // login_pictureBox
            // 
            this.login_pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("login_pictureBox.Image")));
            this.login_pictureBox.Location = new System.Drawing.Point(61, 134);
            this.login_pictureBox.Name = "login_pictureBox";
            this.login_pictureBox.Size = new System.Drawing.Size(121, 78);
            this.login_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.login_pictureBox.TabIndex = 0;
            this.login_pictureBox.TabStop = false;
            // 
            // lblEmployeeManagementSystem
            // 
            this.lblEmployeeManagementSystem.AutoSize = true;
            this.lblEmployeeManagementSystem.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblEmployeeManagementSystem.ForeColor = System.Drawing.Color.White;
            this.lblEmployeeManagementSystem.Location = new System.Drawing.Point(26, 216);
            this.lblEmployeeManagementSystem.Name = "lblEmployeeManagementSystem";
            this.lblEmployeeManagementSystem.Size = new System.Drawing.Size(201, 17);
            this.lblEmployeeManagementSystem.TabIndex = 10;
            this.lblEmployeeManagementSystem.Text = "Employee Management System";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(552, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblloginAccount
            // 
            this.lblloginAccount.AutoSize = true;
            this.lblloginAccount.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblloginAccount.Location = new System.Drawing.Point(274, 64);
            this.lblloginAccount.Name = "lblloginAccount";
            this.lblloginAccount.Size = new System.Drawing.Size(141, 22);
            this.lblloginAccount.TabIndex = 2;
            this.lblloginAccount.Text = "Login Account";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(280, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "UserName :";
            // 
            // login_username
            // 
            this.login_username.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_username.Location = new System.Drawing.Point(280, 136);
            this.login_username.Multiline = true;
            this.login_username.Name = "login_username";
            this.login_username.Size = new System.Drawing.Size(261, 30);
            this.login_username.TabIndex = 4;
            // 
            // login_password
            // 
            this.login_password.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_password.Location = new System.Drawing.Point(278, 204);
            this.login_password.Multiline = true;
            this.login_password.Name = "login_password";
            this.login_password.PasswordChar = '*';
            this.login_password.Size = new System.Drawing.Size(261, 30);
            this.login_password.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(277, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Password :";
            // 
            // login_showPass
            // 
            this.login_showPass.AutoSize = true;
            this.login_showPass.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_showPass.Location = new System.Drawing.Point(430, 241);
            this.login_showPass.Name = "login_showPass";
            this.login_showPass.Size = new System.Drawing.Size(112, 18);
            this.login_showPass.TabIndex = 7;
            this.login_showPass.Text = "Show Password";
            this.login_showPass.UseVisualStyleBackColor = true;
            this.login_showPass.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.SlateGray;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(154)))), ((int)(((byte)(214)))));
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(154)))), ((int)(((byte)(214)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(284, 292);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(84, 32);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Loginpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 400);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.login_showPass);
            this.Controls.Add(this.login_password);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.login_username);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblloginAccount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Loginpage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Password";
            this.Load += new System.EventHandler(this.Loginpage_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Loginpage_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.login_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblloginAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox login_username;
        private System.Windows.Forms.TextBox login_password;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox login_showPass;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblEmployeeManagementSystem;
        private System.Windows.Forms.PictureBox login_pictureBox;
    }
}

