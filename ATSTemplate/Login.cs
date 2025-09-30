using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATSTemplate
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private database db;
        private string ftpServerIP = "10.90.10.168";
        private void Login_Load(object sender, EventArgs e)
        {
            db = new database(ftpServerIP);
            txtusername.Focus();
        }

        private void txtusername_Enter(object sender, EventArgs e)
        {
            if (txtusername.Text == "Tên đăng nhập")
            {
                txtusername.Text = "";
                txtusername.ForeColor = Color.Black;
            }
        }

        private void txtusername_Leave(object sender, EventArgs e)
        {
            if (txtusername.Text == "")
            {
                txtusername.Text = "Tên đăng nhập";
                txtusername.ForeColor = Color.Gray;
            }
        }

        private void txtpassword_Enter(object sender, EventArgs e)
        {
            if (txtpassword.Text == "Mật khẩu")
            {
                txtpassword.Text = "";
                txtpassword.ForeColor = Color.Black;
                txtpassword.UseSystemPasswordChar = true;
            }
        }

        private void txtpassword_Leave(object sender, EventArgs e)
        {
            if (txtpassword.Text == "")
            {
                txtpassword.Text = "Mật khẩu";
                txtpassword.ForeColor = Color.Gray;
                txtpassword.UseSystemPasswordChar = false;
            }
        }
        public string LoggedInUsername { get; private set; }
        private void btnLoginn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtusername.Text.Trim()) || txtusername.Text == "Tên đăng nhập")
            {
                MessageBox.Show("Tên đăng nhập không được bỏ trống.");
                return;  
            }
            if (string.IsNullOrEmpty(txtpassword.Text.Trim()) || txtpassword.Text == "Mật khẩu")
            {
                MessageBox.Show("Mật khẩu không được bỏ trống.");
                return;
            }
            int user_c = db.Login(txtusername.Text.Trim().ToLower(), txtpassword.Text.Trim());
            if (user_c == 1)
            {
                LoggedInUsername = txtusername.Text.Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.");
            }
        }
    }
}
