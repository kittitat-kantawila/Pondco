using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WpfApp1;

namespace Pond_s_Shop
{
    public partial class checkbill : Form
    {
        MySqlDataAdapter oda;
        DataTable dtb;
        private Shopping sp;
        private string username;

        public checkbill(Shopping sp, string user)
        {
            
            InitializeComponent();
            this.sp = sp;
            this.username = user;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Code
            Program.closeProgram();
        }
        private void checkbill_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=10.0.0.205;user id=ong;database=inventory_pond");
            oda = new MySqlDataAdapter("SELECT * FROM was_buy", conn);
            dtb = new DataTable();
            oda.Fill(dtb);
            dataGridView1.DataSource = dtb;
        }
        //go back
        private void button2_Click(object sender, EventArgs e)
        {
            DBConnect dbc = new DBConnect();
            dbc.DeleteQuery("DELETE FROM was_buy WHERE status=0");
            sp.Show();
            this.Hide();
        }
        //cancel
        private void button3_Click(object sender, EventArgs e)
        {
            DBConnect dbc = new DBConnect();
            dbc.DeleteQuery("DELETE FROM was_buy WHERE status=0");
            Shopping newsp = new Shopping(username);
            newsp.Show();
            this.Hide();
        }
        //logout
        private void button4_Click(object sender, EventArgs e)
        {
            Login newlogin = new Login();
            newlogin.Show();
            this.Hide();
        }
    }
}
