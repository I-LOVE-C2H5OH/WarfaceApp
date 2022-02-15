namespace WarfaceApp
{
    using Microsoft.VisualBasic;
    using MyGamesRegger.Data;
    public partial class Form1 : Form
    {
        WarfaceAccount? wf;
        public Form1()
        {
            InitializeComponent();
        }

        private void Auth_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBoxLogin.Text;
                string password = textBoxPassword.Text;
                string n_js_t = textBox_n_js_t.Text;
                string n_js_d = textBox_n_js_d.Text;
                wf = new WarfaceAccount(login, password, n_js_d, n_js_t);
                listBox_Log.Items.Add($"Имя -{wf.GetName()}");
                var nickname = wf.GetNickNames();
                foreach (string nick in nickname)
                {
                    listBox_Log.Items.Add($"\nНикнэйм-{nick}");
                }
                buttonGetPromo.Enabled = true;
                buttonGetVIP.Enabled = true;
                bool? Hide = wf.Hidden();
                if (Hide != null)
                    checkBox1.Checked = Hide.Value;
                else
                    listBox_Log.Items.Add("Обновите n_js_t и n_js_d");
                checkBox1.Enabled = true;
            }
            catch(Exception ee)
            {
                listBox_Log.Items.Add($"{ee.Message}");
            }
        }

        private void buttonGetPromo_Click(object sender, EventArgs e)
        {
            
            int clas = int.Parse(Interaction.InputBox("Выберите класс\n1-Штурмовик\n2-Медик\n3-Инженер\n4-Снайпер", "PROMO"));
            listBox_Log.Items.Add($"\nПромо-{this.wf.Promo(clas)}");
        }

        private void buttonGetVIP_Click(object sender, EventArgs e)
        {
            listBox_Log.Items.Add($"\nВип-{wf.GetAccVip(1)}");
            listBox_Log.Items.Add($"\nВип-{wf.GetAccVip(2)}");
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            wf.SwichHidden();
            listBox_Log.Items.Add($"Изменено");
        }
    }
}