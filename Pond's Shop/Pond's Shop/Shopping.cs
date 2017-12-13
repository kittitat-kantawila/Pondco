using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

using MySql.Data.MySqlClient;

namespace Pond_s_Shop
{
    public partial class Shopping : Form
    {
        
        MySqlDataAdapter oda;

        DataTable dt;
        public Shopping()
        {
            InitializeComponent();
        }

        private void Shopping_Load(object sender, EventArgs e)


        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=inventory_pond");
            oda = new MySqlDataAdapter("SELECT * FROM item", conn);
            dt = new DataTable();
            dt.
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
