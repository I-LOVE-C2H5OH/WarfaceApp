namespace WarfaceApp
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
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.Auth = new System.Windows.Forms.Button();
            this.label_n_js_t = new System.Windows.Forms.Label();
            this.textBox_n_js_t = new System.Windows.Forms.TextBox();
            this.textBox_n_js_d = new System.Windows.Forms.TextBox();
            this.label_n_js_d = new System.Windows.Forms.Label();
            this.labeName = new System.Windows.Forms.Label();
            this.buttonGetVIP = new System.Windows.Forms.Button();
            this.buttonGetPromo = new System.Windows.Forms.Button();
            this.listBox_Log = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(12, 43);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(52, 20);
            this.labelLogin.TabIndex = 0;
            this.labelLogin.Text = "Логин";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(12, 78);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(62, 20);
            this.labelPassword.TabIndex = 1;
            this.labelPassword.Text = "Пароль";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(103, 43);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(260, 27);
            this.textBoxLogin.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(103, 75);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(260, 27);
            this.textBoxPassword.TabIndex = 3;
            // 
            // Auth
            // 
            this.Auth.Location = new System.Drawing.Point(69, 195);
            this.Auth.Name = "Auth";
            this.Auth.Size = new System.Drawing.Size(238, 66);
            this.Auth.TabIndex = 4;
            this.Auth.Text = "Войти";
            this.Auth.UseVisualStyleBackColor = true;
            this.Auth.Click += new System.EventHandler(this.Auth_Click);
            // 
            // label_n_js_t
            // 
            this.label_n_js_t.AutoSize = true;
            this.label_n_js_t.Location = new System.Drawing.Point(14, 113);
            this.label_n_js_t.Name = "label_n_js_t";
            this.label_n_js_t.Size = new System.Drawing.Size(78, 20);
            this.label_n_js_t.TabIndex = 5;
            this.label_n_js_t.Text = "n_js_t куки";
            // 
            // textBox_n_js_t
            // 
            this.textBox_n_js_t.Location = new System.Drawing.Point(103, 110);
            this.textBox_n_js_t.Name = "textBox_n_js_t";
            this.textBox_n_js_t.Size = new System.Drawing.Size(260, 27);
            this.textBox_n_js_t.TabIndex = 6;
            // 
            // textBox_n_js_d
            // 
            this.textBox_n_js_d.Location = new System.Drawing.Point(103, 143);
            this.textBox_n_js_d.Name = "textBox_n_js_d";
            this.textBox_n_js_d.Size = new System.Drawing.Size(260, 27);
            this.textBox_n_js_d.TabIndex = 8;
            // 
            // label_n_js_d
            // 
            this.label_n_js_d.AutoSize = true;
            this.label_n_js_d.Location = new System.Drawing.Point(14, 146);
            this.label_n_js_d.Name = "label_n_js_d";
            this.label_n_js_d.Size = new System.Drawing.Size(82, 20);
            this.label_n_js_d.TabIndex = 7;
            this.label_n_js_d.Text = "n_js_d куки";
            // 
            // labeName
            // 
            this.labeName.AutoSize = true;
            this.labeName.Location = new System.Drawing.Point(133, 9);
            this.labeName.Name = "labeName";
            this.labeName.Size = new System.Drawing.Size(39, 20);
            this.labeName.TabIndex = 9;
            this.labeName.Text = "Имя";
            // 
            // buttonGetVIP
            // 
            this.buttonGetVIP.Enabled = false;
            this.buttonGetVIP.Location = new System.Drawing.Point(69, 267);
            this.buttonGetVIP.Name = "buttonGetVIP";
            this.buttonGetVIP.Size = new System.Drawing.Size(238, 29);
            this.buttonGetVIP.TabIndex = 10;
            this.buttonGetVIP.Text = "Получить ВИП";
            this.buttonGetVIP.UseVisualStyleBackColor = true;
            this.buttonGetVIP.Click += new System.EventHandler(this.buttonGetVIP_Click);
            // 
            // buttonGetPromo
            // 
            this.buttonGetPromo.Enabled = false;
            this.buttonGetPromo.Location = new System.Drawing.Point(69, 302);
            this.buttonGetPromo.Name = "buttonGetPromo";
            this.buttonGetPromo.Size = new System.Drawing.Size(238, 29);
            this.buttonGetPromo.TabIndex = 11;
            this.buttonGetPromo.Text = "Получить ПРОМО";
            this.buttonGetPromo.UseVisualStyleBackColor = true;
            this.buttonGetPromo.Click += new System.EventHandler(this.buttonGetPromo_Click);
            // 
            // listBox_Log
            // 
            this.listBox_Log.FormattingEnabled = true;
            this.listBox_Log.ItemHeight = 20;
            this.listBox_Log.Items.AddRange(new object[] {
            "Логи"});
            this.listBox_Log.Location = new System.Drawing.Point(11, 359);
            this.listBox_Log.Name = "listBox_Log";
            this.listBox_Log.Size = new System.Drawing.Size(359, 124);
            this.listBox_Log.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 490);
            this.Controls.Add(this.listBox_Log);
            this.Controls.Add(this.buttonGetPromo);
            this.Controls.Add(this.buttonGetVIP);
            this.Controls.Add(this.labeName);
            this.Controls.Add(this.textBox_n_js_d);
            this.Controls.Add(this.label_n_js_d);
            this.Controls.Add(this.textBox_n_js_t);
            this.Controls.Add(this.label_n_js_t);
            this.Controls.Add(this.Auth);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelLogin);
            this.MaximumSize = new System.Drawing.Size(393, 537);
            this.MinimumSize = new System.Drawing.Size(393, 537);
            this.Name = "Form1";
            this.Text = "WarfaceApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelLogin;
        private Label labelPassword;
        private TextBox textBoxLogin;
        private TextBox textBoxPassword;
        private Button Auth;
        private Label label_n_js_t;
        private TextBox textBox_n_js_t;
        private TextBox textBox_n_js_d;
        private Label label_n_js_d;
        private Label labeName;
        private Button buttonGetVIP;
        private Button buttonGetPromo;
        private ListBox listBox_Log;
    }
}