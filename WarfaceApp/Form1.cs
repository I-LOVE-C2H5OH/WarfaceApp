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
                listBox_Log.Items.Add($"��� -{wf.GetName()}");
                var nickname = wf.GetNickNames();
                foreach (string nick in nickname)
                {
                    listBox_Log.Items.Add($"\n�������-{nick}");
                }
                buttonGetPromo.Enabled = true;
                buttonGetVIP.Enabled = true;
                bool? Hide = wf.Hidden();
                if (Hide != null)
                    checkBox1.Checked = Hide.Value;
                else
                    listBox_Log.Items.Add("�������� n_js_t � n_js_d");
                checkBox1.Enabled = true;
            }
            catch(Exception ee)
            {
                listBox_Log.Items.Add($"{ee.Message}");
            }
        }

        private void buttonGetPromo_Click(object sender, EventArgs e)
        {
            
            int clas = int.Parse(Interaction.InputBox("�������� �����\n1-���������\n2-�����\n3-�������\n4-�������", "PROMO"));
            listBox_Log.Items.Add($"\n�����-{this.wf.Promo(clas)}");
        }

        private void buttonGetVIP_Click(object sender, EventArgs e)
        {
            listBox_Log.Items.Add($"\n���-{wf.GetAccVip(1)}");
            listBox_Log.Items.Add($"\n���-{wf.GetAccVip(2)}");
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            wf.SwichHidden();
            listBox_Log.Items.Add($"��������");
        }
    }
}