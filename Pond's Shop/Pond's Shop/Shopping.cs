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

namespace Pond_s_Shop
{
    public partial class Shopping : Form
    {
        OleDbDataAdapter oda;
        OleDbCommandBuilder ocb;
        DataTable dt;
        public Shopping()
        {
            InitializeComponent();
        }

        private void Shopping_Load(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection("server=localhost;user id=root;database=inventory_pond");
            oda = new OleDbDataAdapter("SELECT (id, name, price, unit) FROM item", conn);
            dt = new DataTable();
            dataGridView1.DataSource = dt;
        }
    }
}
