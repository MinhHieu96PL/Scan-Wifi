using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sona;

namespace ATSTemplate
{
    public partial class LockScreen : Form
    {
        private database db;
        private Form1 ats;
        public LockScreen(Form1 _ats)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            groupBox1.Anchor = AnchorStyles.None; // Không gắn GroupBox vào bất kỳ vị trí nào
            groupBox1.Location = new Point((this.ClientSize.Width - groupBox1.Width) / 2,
                                            (this.ClientSize.Height - groupBox1.Height) / 2);
            this.ats = _ats;
        }
        string ftpLog_IP = IO_ini.ReadIniFile("Setting", "FTPLOG", "FTP_IP", "");
        string ftpLog_User = IO_ini.ReadIniFile("Setting", "FTPLOG", "FTP_User", "");
        string ftpLog_Password = IO_ini.ReadIniFile("Setting", "FTPLOG", "FTP_Pass", "");

        private string ftpServerIP = IO_ini.ReadIniFile("AutoDL_Setting", "Setting", "Server_IP", "10.90.10.168");

        private FtpClient ftpLog = null;

        public class UserCredentials
        {
            public string User { get; set; }
            public string Password { get; set; }
        }
        private void LockScreen_Load(object sender, EventArgs e)
        {
            label1.Text = ats.lock_status;
            db = new database(ftpServerIP);
            this.WindowState = FormWindowState.Maximized;
            lblerror.Visible = false;
            ftpLog = new FtpClient(ftpLog_IP, ftpLog_User, ftpLog_Password);
            this.TopMost = true;
            this.Focus();
            txtuser.Focus();
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            //if (txtuser.Text == "")
            //{
            //    txtuser.Text = "Username";
            //    txtuser.ForeColor = Color.Gray;
            //}
        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            //if (txtuser.Text == "Username")
            //{
            //    txtuser.Text = "";
            //    txtuser.ForeColor = Color.Black;
            //}
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            //if (txtpass.Text == "")
            //{
            //    txtpass.UseSystemPasswordChar = false;
            //    txtpass.Text = "Password";
            //    txtpass.ForeColor = Color.Gray;
            //}
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            //if (txtpass.Text == "Password")
            //{
            //    txtpass.UseSystemPasswordChar = true;
            //    txtpass.Text = "";
            //    txtpass.ForeColor = Color.Black;
            //}
        }

        private void WriteToFile(string file, string message, bool append)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(file));
                }
                if (!message.EndsWith("\r\n"))
                {
                    message += "\r\n";
                }
                if (append)
                {
                    File.AppendAllText(file, message, Encoding.UTF8);
                }
                else
                {

                    File.WriteAllText(file, message, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[ERR] Write exception:" + ex.Message);
            }
        }
        
        string _station = IO_ini.ReadIniFile("Setting", "Setting", "Station", "");
        string _Product = IO_ini.ReadIniFile("Setting", "Setting", "Product", "");
        string _PN = IO_ini.ReadIniFile("Setting", "Setting", "PN", "");
        private string current_directory = Application.StartupPath;
        private void SaveToCSV()
        {
            try
            {
                string serverFile = "/Data/" + _Product + "/Unlock_History/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + _station + "_" + Environment.MachineName + "_" + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + ".csv";
                
                string localFile = current_directory + "\\Unlock_History.csv";
                string error = "";
                string title = "Employee ID,PC Name,Unlock Time";
                string content = txtuser.Text.Trim() + "," + Environment.MachineName + "," + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                ftpLog.ConnectServer();
                
                if (!ftpLog.FileExits(serverFile, ref error))
                {
                    if (error != "")
                    {
                        return;
                    }

                    WriteToFile(localFile, title + "\r\n" + content, false);
                }
                else
                {
                    WriteToFile(localFile, content, false);
                }

                ftpLog.UploadToFTP(localFile, serverFile, true, ref error);
            }
            catch
            {
                
            }
            finally
            {
                ftpLog.Close();
            }
        }
        private bool allowClose = false;
        private void button1_Click(object sender, EventArgs e)
        {
            allowClose = true;
            //List<UserCredentials> userCredentialsList = new List<UserCredentials>();

            //// Thêm các tài khoản vào danh sách
            //userCredentialsList.Add(new UserCredentials { User = "V0975020", Password = "123" });
            //userCredentialsList.Add(new UserCredentials { User = "V0979808", Password = "123" });
            //userCredentialsList.Add(new UserCredentials { User = "V0944096", Password = "123" });
            //userCredentialsList.Add(new UserCredentials { User = "V0948322", Password = "123" });
            //userCredentialsList.Add(new UserCredentials { User = "V0952303", Password = "123" });
            //userCredentialsList.Add(new UserCredentials { User = "V0963404", Password = "123" });
            //userCredentialsList.Add(new UserCredentials { User = "V0991302", Password = "123" });
            //userCredentialsList.Add(new UserCredentials { User = "V0999457", Password = "123" });
            //userCredentialsList.Add(new UserCredentials { User = "V1015812", Password = "123" });
            //userCredentialsList.Add(new UserCredentials { User = "V0951854", Password = "12345" });
            //userCredentialsList.Add(new UserCredentials { User = "PRO", Password = "123" });

            //foreach (var user in userCredentialsList)
            //{
            //    if (txtuser.Text.Trim().ToUpper().Equals(user.User) && txtpass.Text.Trim().Equals(user.Password))
            //    {
            //        SaveToCSV();
            //        this.Close();
            //    }
            //}

            int user_c = db.Login(txtuser.Text.Trim().ToLower(), txtpass.Text.Trim().ToLower());
            if (user_c == 1)
            {
                SaveToCSV();
                this.Close();
            }
            else
            {
                user_c = db.Login(txtuser.Text.Trim().ToLower(), txtpass.Text.Trim().ToLower());
                if (user_c == 1)
                {
                    SaveToCSV();
                    this.Close();
                }
            }    

            allowClose = false;
            lblerror.Visible = true;
        }

        private void LockScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!admin_close)
            {
                if (!allowClose && e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true; // Hủy bỏ sự kiện đóng form
                }
            }
        }

        private void LockScreen_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        bool admin_close = false;
        bool ctrl = false;
        bool shift = false;
        private void LockScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                ctrl = true;
            }
            else if (e.KeyCode == Keys.ShiftKey)
            {
                shift = true;
            }
            else if (e.KeyCode == Keys.Q)
            {
                if (ctrl && shift)
                {
                    admin_close = true;
                    this.Close();
                }
            }
        }

        private void LockScreen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                ctrl = false;
            }
            else if (e.KeyCode == Keys.ShiftKey)
            {
                shift = false;
            }
        }
    }
}
