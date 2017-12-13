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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }


        private void ClickBack(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }

        private void CallAccept(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            string user = userfield.Text;
            if (userfield.Text.Equals("") || passfield.Text.Equals("") || confirmpass.Text.Equals("") ||
                namefield.Text.Equals("") || address.Text.Equals("") || phone.Text.Equals(""))
            {
                MessageBox.Show("please input all");
            }
            else if (db.SearchUserID(user))
            {
                MessageBox.Show("this username is already use.");
            }
            else if(!passfield.Text.Equals(confirmpass.Text))
            {
                MessageBox.Show("confirm pass doesn't match");
                passfield.Text = "";
                confirmpass.Text = "";
            }
            else
            {
                db.InsertQuery("INSERT INTO customer (username,password,name,address,contact_number) VALUES ('" +
                    user+"','"+passfield.Text+"','"+namefield.Text+"','"+address.Text+"','"+phone.Text+"')");
                MessageBox.Show("Sign up successful");
                Login lin = new Login();
                lin.Show();
                this.Hide();
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
