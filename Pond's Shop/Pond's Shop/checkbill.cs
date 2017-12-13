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
            DBConnect dbc = new DBConnect();
            dbc.DeleteQuery("DELETE FROM was_buy WHERE status=0");
            Program.closeProgram();
        }
        private void checkbill_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=10.0.0.205;user id=ong;database=inventory_pond");
            oda = new MySqlDataAdapter("SELECT * FROM was_buy WHERE status=0", conn);
            dtb = new DataTable();
            oda.Fill(dtb);
            dtb.Columns.Add(new DataColumn("Item", typeof(string)));
            dtb.Columns[6].SetOrdinal(2);
            dataGridView1.DataSource = dtb;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //add product name
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                DBConnect db = new DBConnect();
                string name = db.selectOne("SELECT * FROM item WHERE id='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'", "name");
                //MessageBox.Show(name);
                dataGridView1.Rows[i].Cells[2].Value = name;
            }


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
            DBConnect dbc = new DBConnect();
            dbc.DeleteQuery("DELETE FROM was_buy WHERE status=0");
            Login newlogin = new Login();
            newlogin.Show();
            this.Hide();
        }
        //confirm
        private void button1_Click(object sender, EventArgs e)
        {
            DBConnect dbc = new DBConnect();
            dbc.UpdateQuery("UPDATE item,was_buy SET item.unit=item.unit-was_buy.total_unit WHERE item.id=was_buy.id and was_buy.status=0");
            dbc.UpdateQuery("UPDATE was_buy SET status=1 WHERE username='"+username+"' and "+"status=0");
            MessageBox.Show("Transaction successful");
            Shopping newsp = new Shopping(username);
            newsp.Show();
            this.Hide();
        }
    }
}
