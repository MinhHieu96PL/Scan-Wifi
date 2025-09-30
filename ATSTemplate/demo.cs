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
    public partial class demo : Form
    {
        public demo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.ShowDialog();
            loginForm.Focus();
            loginForm.TopMost = true;

            // Kiểm tra nếu Form đăng nhập đã đóng và có thông tin đăng nhập
            if (loginForm.DialogResult == DialogResult.OK)
            {
                string loggedInUser = loginForm.LoggedInUsername;
                if (!string.IsNullOrEmpty(loggedInUser))
                {
                    // Xử lý thông tin đăng nhập ở đây (loggedInUser chứa tên người dùng)
                    MessageBox.Show($"Đăng nhập thành công! Tên người dùng: {loggedInUser}");
                }
                else
                {
                    MessageBox.Show("Đăng nhập không thành công!");
                }
            }
        }
    }
}
