using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApp1;

namespace Pond_s_Shop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void Signup_Click(object sender, EventArgs e)
        {
            Signup su = new Signup();
            su.Show();
            this.Hide();
        }

        private void Call_Signin(object sender, EventArgs e)
        {
            string user = userfield.Text;
            string pass = passfield.Text;
            DBConnect db = new DBConnect();
            //db.OpenConnection();
            bool result = db.SearchCustomer(user,pass);
            if (!result)
            {
                MessageBox.Show("invalid username or password");
                userfield.Text = "";
                passfield.Text = "";
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Code
            Program.closeProgram();
        }
    }
}
