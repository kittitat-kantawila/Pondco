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
            MySqlConnection conn = new MySqlConnection("server=10.0.0.205;user id=ong;database=inventory_pond");
            oda = new MySqlDataAdapter("SELECT * FROM item", conn);
            dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chk);
            chk.HeaderText = "Check Data";
            chk.Name = "chk";
            //dataGridView1.Rows[2].Cells[5].Value = true;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Code
            Program.closeProgram();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
