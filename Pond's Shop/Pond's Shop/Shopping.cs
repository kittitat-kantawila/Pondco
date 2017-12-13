using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using MySql.Data.MySqlClient;
using WpfApp1;

namespace Pond_s_Shop
{
    public partial class Shopping : Form
    {
        
        MySqlDataAdapter oda;
        DataTable dt;
        private string username;
        private bool canAdd = false;

        public Shopping(string user)
        {
            InitializeComponent();
            this.username = user;
        }

        private void Shopping_Load(object sender, EventArgs e)


        {
            MySqlConnection conn = new MySqlConnection("server=10.0.0.205;user id=ong;database=inventory_pond");
            oda = new MySqlDataAdapter("SELECT * FROM item", conn);
            dt = new DataTable();
            oda.Fill(dt);
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
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
            dataGridView1.Columns[5].ReadOnly = true;
        }
         //go to cart
        private void button1_Click(object sender, EventArgs e)
        {
            //pull selected (true) item
            int n = dataGridView1.RowCount;
            for(int i=0;i<n;i++)
            {
                //check don't empty
                if (!dataGridView1.Rows[i].Cells[5].Value.ToString().Equals(""))
                {
                    int amount = int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    int max = int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    // not enough
                    if (amount > max)
                    {
                        MessageBox.Show("Not enough item");
                        canAdd = false;
                        return ;
                    }
                    //can add to cart
                    else
                    {
                        canAdd = true;
                        continue;

                    }
                }
                else if(dataGridView1.Rows[i].Cells[6].Value != null && dataGridView1.Rows[i].Cells[6].Value.ToString().Equals("True"))
                {
                    MessageBox.Show("please enter amount");
                    canAdd = false;
                    return;
                }
            }
            if(canAdd)
            {
                for(int i=0;i<n;i++)
                {
                    if (!dataGridView1.Rows[i].Cells[5].Value.ToString().Equals(""))
                    {
                        int amount = int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                        //can add to cart
                        DateTime time = DateTime.Now;
                        string day = "" + time.Year + "-" + time.Month + "-" + time.Day;
                        string pid = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        float total = amount * int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        DBConnect dbc = new DBConnect();
                        dbc.InsertQuery("INSERT INTO was_buy (id,username,date,total_unit,total_price_per_item,status) VALUES('" + pid + "','" + this.username + "','" + day + "'," + amount + "," + total +","+ "0)");
                    }
                }
                canAdd = false;
                MessageBox.Show("Add to cart successful");
                checkbill cb = new checkbill(this,username);
                cb.Show();
                this.Hide();
            }

            //check amount

            //add to database and goto next page
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Code
            Program.closeProgram();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value != null && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString().Equals("True"))
                {
                    //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "True");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                }
                else
                {
                    //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "False");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                }
            }
            else if(e.ColumnIndex == 6)
            {
                if (!dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex-1].Value.ToString().Equals(""))
                {
                    //MessageBox.Show("sDasdsad");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 5) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
