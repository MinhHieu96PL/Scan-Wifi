using Microsoft.Win32;
using Sona;
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

namespace ATSTemplate
{
    public partial class Cable : Form
    {
        public Cable()
        {
            InitializeComponent();
        }

        string power_limit = "";
        string ethernet_limit = "";
        string ant_limit = "";

        string usernane = "";

        string _station = IO_ini.ReadIniFile("Setting", "Setting", "Station", "");
        string _Product = IO_ini.ReadIniFile("Setting", "Setting", "Product", "");
        string _PN = IO_ini.ReadIniFile("Setting", "Setting", "PN", "");
        private string current_directory = Application.StartupPath;

        string ftpLog_IP = IO_ini.ReadIniFile("Setting", "FTPLOG", "FTP_IP", "");
        string ftpLog_User = IO_ini.ReadIniFile("Setting", "FTPLOG", "FTP_User", "");
        string ftpLog_Password = IO_ini.ReadIniFile("Setting", "FTPLOG", "FTP_Pass", "");

        private string ftpServerIP = IO_ini.ReadIniFile("AutoDL_Setting", "Setting", "Server_IP", "10.90.10.168");

        private FtpClient ftpLog = null;

        private void Cable_Load(object sender, EventArgs e)
        {
            usernane = "";
            ftpLog = new FtpClient(ftpLog_IP, ftpLog_User, ftpLog_Password);

            btnAnt1.Enabled = false;
            btnAnt2.Enabled = false;
            btnAnt3.Enabled = false;
            btnAnt4.Enabled = false;
            btnAnt5.Enabled = false;
            btnAnt6.Enabled = false;
            btnAnt7.Enabled = false;
            btnAnt8.Enabled = false;
            btnAnt9.Enabled = false;
            btnAnt10.Enabled = false;
            btnAnt11.Enabled = false;
            btnPower.Enabled = false;
            btnEth.Enabled = false;

            power_limit = IO_ini.ReadIniFile(".//Cable", "Cable_Limit", "Power","0");
            ethernet_limit = IO_ini.ReadIniFile(".//Cable", "Cable_Limit", "Ethernet", "0");
            ant_limit = IO_ini.ReadIniFile(".//Cable", "Cable_Limit", "Antenna", "0");

            lblPowerLimit.Text = power_limit;
            lblEthLimit.Text = ethernet_limit;
            if (_station == "MBFT")
            {
                lblAnt1Limit.Text = ant_limit;
                lblAnt2Limit.Text = ant_limit;
                lblAnt3Limit.Text = ant_limit;
                lblAnt4Limit.Text = ant_limit;
                lblAnt5Limit.Text = ant_limit;
                lblAnt6Limit.Text = ant_limit;
                lblAnt7Limit.Text = ant_limit;
                lblAnt8Limit.Text = ant_limit;
                lblAnt9Limit.Text = ant_limit;
                lblAnt10Limit.Text = ant_limit;
                lblAnt11Limit.Text = ant_limit;
            }

            string registryPath = @"Software\" + _Product + "\\" + _station;

            string power_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Power_Cable", "0") as string;
            if (power_ut.Length >= 4)
            {
                int power_ut_new = Convert.ToInt32(power_ut);
                string power = power_ut_new.ToString("N0");
                lblPowerUsedTime.Text = power;
            }
            else
            {
                lblPowerUsedTime.Text = power_ut;
            }

            string eth_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Ethernet_Cable", "0") as string;
            if (eth_ut.Length >= 4)
            {
                int eth_ut_new = Convert.ToInt32(eth_ut);
                string eth = eth_ut_new.ToString("N0");
                lblEthUsedTime.Text = eth;
            }
            else
            {
                lblEthUsedTime.Text = eth_ut;
            }

            if (_station == "MBFT")
            {
                string ant1_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna1_Cable", "0") as string;
                if (ant1_ut.Length >= 4)
                {
                    int ant1_ut_new = Convert.ToInt32(ant1_ut);
                    string ant1 = ant1_ut_new.ToString("N0");
                    lblAnt1UsedTime.Text = ant1;
                }
                else
                {
                    lblAnt1UsedTime.Text = ant1_ut;
                }

                string ant2_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna2_Cable", "0") as string;
                if (ant2_ut.Length >= 4)
                {
                    int ant2_ut_new = Convert.ToInt32(ant2_ut);
                    string ant2 = ant2_ut_new.ToString("N0");
                    lblAnt2UsedTime.Text = ant2;
                }
                else
                {
                    lblAnt2UsedTime.Text = ant2_ut;
                }

                string ant3_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna3_Cable", "0") as string;
                if (ant3_ut.Length >= 4)
                {
                    int ant3_ut_new = Convert.ToInt32(ant3_ut);
                    string ant3 = ant3_ut_new.ToString("N0");
                    lblAnt3UsedTime.Text = ant3;
                }
                else
                {
                    lblAnt3UsedTime.Text = ant3_ut;
                }

                string ant4_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna4_Cable", "0") as string;
                if (ant4_ut.Length >= 4)
                {
                    int ant4_ut_new = Convert.ToInt32(ant4_ut);
                    string ant4 = ant4_ut_new.ToString("N0");
                    lblAnt4UsedTime.Text = ant4;
                }
                else
                {
                    lblAnt4UsedTime.Text = ant4_ut;
                }

                string ant5_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna5_Cable", "0") as string;
                if (ant5_ut.Length >= 4)
                {
                    int ant5_ut_new = Convert.ToInt32(ant5_ut);
                    string ant5 = ant5_ut_new.ToString("N0");
                    lblAnt5UsedTime.Text = ant5;
                }
                else
                {
                    lblAnt5UsedTime.Text = ant5_ut;
                }

                string ant6_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna6_Cable", "0") as string;
                if (ant6_ut.Length >= 4)
                {
                    int ant6_ut_new = Convert.ToInt32(ant6_ut);
                    string ant6 = ant6_ut_new.ToString("N0");
                    lblAnt6UsedTime.Text = ant6;
                }
                else
                {
                    lblAnt6UsedTime.Text = ant6_ut;
                }

                string ant7_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna7_Cable", "0") as string;
                if (ant7_ut.Length >= 4)
                {
                    int ant7_ut_new = Convert.ToInt32(ant7_ut);
                    string ant7 = ant7_ut_new.ToString("N0");
                    lblAnt7UsedTime.Text = ant7;
                }
                else
                {
                    lblAnt7UsedTime.Text = ant7_ut;
                }

                string ant8_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna8_Cable", "0") as string;
                if (ant8_ut.Length >= 4)
                {
                    int ant8_ut_new = Convert.ToInt32(ant8_ut);
                    string ant8 = ant8_ut_new.ToString("N0");
                    lblAnt8UsedTime.Text = ant8;
                }
                else
                {
                    lblAnt8UsedTime.Text = ant8_ut;
                }

                string ant9_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna9_Cable", "0") as string;
                if (ant9_ut.Length >= 4)
                {
                    int ant9_ut_new = Convert.ToInt32(ant9_ut);
                    string ant9 = ant9_ut_new.ToString("N0");
                    lblAnt9UsedTime.Text = ant9;
                }
                else
                {
                    lblAnt9UsedTime.Text = ant9_ut;
                }

                string ant10_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna10_Cable", "0") as string;
                if (ant10_ut.Length >= 4)
                {
                    int ant10_ut_new = Convert.ToInt32(ant10_ut);
                    string ant10 = ant10_ut_new.ToString("N0");
                    lblAnt10UsedTime.Text = ant10;
                }
                else
                {
                    lblAnt10UsedTime.Text = ant10_ut;
                }

                string ant11_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna11_Cable", "0") as string;
                if (ant11_ut.Length >= 4)
                {
                    int ant11_ut_new = Convert.ToInt32(ant11_ut);
                    string ant11 = ant11_ut_new.ToString("N0");
                    lblAnt11UsedTime.Text = ant11;
                }
                else
                {
                    lblAnt11UsedTime.Text = ant11_ut;
                }
            }
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
                    if (_station == "MBFT")
                    {
                        btnAnt1.Enabled = true;
                        btnAnt2.Enabled = true;
                        btnAnt3.Enabled = true;
                        btnAnt4.Enabled = true;
                        btnAnt5.Enabled = true;
                        btnAnt6.Enabled = true;
                        btnAnt7.Enabled = true;
                        btnAnt8.Enabled = true;
                        btnAnt9.Enabled = true;
                        btnAnt10.Enabled = true;
                        btnAnt11.Enabled = true;
                    }
                    btnPower.Enabled = true;
                    btnEth.Enabled = true;
                    usernane = loggedInUser;
                }
                else
                {
                    MessageBox.Show("Đăng nhập không thành công!");
                }
            }
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

        private void SaveToCSV(string type)
        {
            try
            {
                string serverFile = "/Data/" + _Product + "/ChangeCable/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + _station + "_" + Environment.MachineName + "_" + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + ".csv";

                string localFile = current_directory + "\\ChangeCable.csv";
                string error = "";
                string title = "Employee ID,PC Name,Type,Change Time";
                string content = usernane + "," + Environment.MachineName + "," + type + "," + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
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

        private void btnPower_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Power cable?","Reset cable",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Power_Cable", "0");
                lblPowerUsedTime.Text = "0";
                SaveToCSV("Power");
            }
        }

        private void btnEth_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Ethernet cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Ethernet_Cable", "0");
                lblEthUsedTime.Text = "0";
                SaveToCSV("Ethernet");
            }
        }

        private void btnAnt1_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 1 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna1_Cable", "0");
                lblAnt1UsedTime.Text = "0";
                SaveToCSV("Antenna_1");
            }
        }

        private void btnAnt2_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 2 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna2_Cable", "0");
                lblAnt2UsedTime.Text = "0";
                SaveToCSV("Antenna_2");
            }
        }

        private void btnAnt3_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 3 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna3_Cable", "0");
                lblAnt3UsedTime.Text = "0";
                SaveToCSV("Antenna_3");
            }
        }

        private void btnAnt4_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 4 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna4_Cable", "0");
                lblAnt4UsedTime.Text = "0";
                SaveToCSV("Antenna_4");
            }
        }

        private void btnAnt5_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 5 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna5_Cable", "0");
                lblAnt5UsedTime.Text = "0";
                SaveToCSV("Antenna_5");
            }
        }

        private void btnAnt6_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 6 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna6_Cable", "0");
                lblAnt6UsedTime.Text = "0";
                SaveToCSV("Antenna_6");
            }
        }

        private void btnAnt7_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 7 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna7_Cable", "0");
                lblAnt7UsedTime.Text = "0";
                SaveToCSV("Antenna_7");
            }
        }

        private void btnAnt8_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 8 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna8_Cable", "0");
                lblAnt8UsedTime.Text = "0";
                SaveToCSV("Antenna_8");
            }
        }

        private void btnAnt9_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 9 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna9_Cable", "0");
                lblAnt9UsedTime.Text = "0";
                SaveToCSV("Antenna_9");
            }
        }

        private void btnAnt10_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 10 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna10_Cable", "0");
                lblAnt10UsedTime.Text = "0";
                SaveToCSV("Antenna_10");
            }
        }

        private void btnAnt11_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\" + _Product + "\\" + _station;
            if (MessageBox.Show("Bạn có chắc chắn muốn Reset Antenna 11 cable?", "Reset cable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna11_Cable", "0");
                lblAnt11UsedTime.Text = "0";
                SaveToCSV("Antenna_11");
            }
        }
    }
}
