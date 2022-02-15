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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(10, 32);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(41, 15);
            this.labelLogin.TabIndex = 0;
            this.labelLogin.Text = "Логин";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(10, 58);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(49, 15);
            this.labelPassword.TabIndex = 1;
            this.labelPassword.Text = "Пароль";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(90, 32);
            this.textBoxLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(228, 23);
            this.textBoxLogin.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(90, 56);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(228, 23);
            this.textBoxPassword.TabIndex = 3;
            // 
            // Auth
            // 
            this.Auth.Location = new System.Drawing.Point(65, 169);
            this.Auth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Auth.Name = "Auth";
            this.Auth.Size = new System.Drawing.Size(208, 50);
            this.Auth.TabIndex = 4;
            this.Auth.Text = "Войти";
            this.Auth.UseVisualStyleBackColor = true;
            this.Auth.Click += new System.EventHandler(this.Auth_Click);
            // 
            // label_n_js_t
            // 
            this.label_n_js_t.AutoSize = true;
            this.label_n_js_t.Location = new System.Drawing.Point(12, 85);
            this.label_n_js_t.Name = "label_n_js_t";
            this.label_n_js_t.Size = new System.Drawing.Size(64, 15);
            this.label_n_js_t.TabIndex = 5;
            this.label_n_js_t.Text = "n_js_t куки";
            // 
            // textBox_n_js_t
            // 
            this.textBox_n_js_t.Location = new System.Drawing.Point(90, 82);
            this.textBox_n_js_t.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_n_js_t.Name = "textBox_n_js_t";
            this.textBox_n_js_t.Size = new System.Drawing.Size(228, 23);
            this.textBox_n_js_t.TabIndex = 6;
            // 
            // textBox_n_js_d
            // 
            this.textBox_n_js_d.Location = new System.Drawing.Point(90, 107);
            this.textBox_n_js_d.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_n_js_d.Name = "textBox_n_js_d";
            this.textBox_n_js_d.Size = new System.Drawing.Size(228, 23);
            this.textBox_n_js_d.TabIndex = 8;
            // 
            // label_n_js_d
            // 
            this.label_n_js_d.AutoSize = true;
            this.label_n_js_d.Location = new System.Drawing.Point(12, 110);
            this.label_n_js_d.Name = "label_n_js_d";
            this.label_n_js_d.Size = new System.Drawing.Size(67, 15);
            this.label_n_js_d.TabIndex = 7;
            this.label_n_js_d.Text = "n_js_d куки";
            // 
            // labeName
            // 
            this.labeName.AutoSize = true;
            this.labeName.Location = new System.Drawing.Point(116, 7);
            this.labeName.Name = "labeName";
            this.labeName.Size = new System.Drawing.Size(31, 15);
            this.labeName.TabIndex = 9;
            this.labeName.Text = "Имя";
            // 
            // buttonGetVIP
            // 
            this.buttonGetVIP.Enabled = false;
            this.buttonGetVIP.Location = new System.Drawing.Point(65, 223);
            this.buttonGetVIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGetVIP.Name = "buttonGetVIP";
            this.buttonGetVIP.Size = new System.Drawing.Size(208, 22);
            this.buttonGetVIP.TabIndex = 10;
            this.buttonGetVIP.Text = "Получить ВИП";
            this.buttonGetVIP.UseVisualStyleBackColor = true;
            this.buttonGetVIP.Click += new System.EventHandler(this.buttonGetVIP_Click);
            // 
            // buttonGetPromo
            // 
            this.buttonGetPromo.Enabled = false;
            this.buttonGetPromo.Location = new System.Drawing.Point(65, 249);
            this.buttonGetPromo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGetPromo.Name = "buttonGetPromo";
            this.buttonGetPromo.Size = new System.Drawing.Size(208, 22);
            this.buttonGetPromo.TabIndex = 11;
            this.buttonGetPromo.Text = "Получить ПРОМО";
            this.buttonGetPromo.UseVisualStyleBackColor = true;
            this.buttonGetPromo.Click += new System.EventHandler(this.buttonGetPromo_Click);
            // 
            // listBox_Log
            // 
            this.listBox_Log.FormattingEnabled = true;
            this.listBox_Log.ItemHeight = 15;
            this.listBox_Log.Items.AddRange(new object[] {
            "Логи"});
            this.listBox_Log.Location = new System.Drawing.Point(10, 296);
            this.listBox_Log.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox_Log.Name = "listBox_Log";
            this.listBox_Log.Size = new System.Drawing.Size(315, 94);
            this.listBox_Log.TabIndex = 12;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(12, 135);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(129, 19);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Персонаж \"скрыт\"";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 401);
            this.Controls.Add(this.checkBox1);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(346, 440);
            this.MinimumSize = new System.Drawing.Size(346, 440);
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
        private CheckBox checkBox1;
    }
}