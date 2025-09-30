using EnterpriseDT.Net.Ftp;
using EnterpriseDT.Util;
using Microsoft.Win32;
using NetFwTypeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PTSC;
using SimpleTCP;
using Sona;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Timer = System.Windows.Forms.Timer;

namespace ATSTemplate
{
    public partial class Form1 : Form
    {
        private AutoResetEvent WriteLogEvent = new AutoResetEvent(true);
        public TestMode testmode;
        public SFCS_mode SFCSmode = SFCS_mode.ON;
        public string mo_1p = "";
        public string no_dhcp = "";
        public string Current_MO = "";
        public string _station = "";
        public string _PN = "";
        public string _Type = "";
        public int _SendAPI = 1;
        public int _parseSweepLog = 1;
        public string _MBLTPN = "";
        public string _MO = "";
        private string BuildName = "";
        public string _Product = "";
        //public List<ItemInfo> ListItems;
        public string LogFilePath = @"D:\AmbitLog\";
        public string LogFileName = "";
        public string log_time = "";
        private string version = "1.2.1";
        private string PC_NAME = "";
        private string sfis_log_path = @"D:\SFIS_LOG\";
        public string sfis_to_ui = "";
        public string ui_to_sfis = "";
        
        private string swarning = "";
        private string py_get_limit = "";
        private string py_send_api = "";
        public Limits limits;
        public string current_directory = Application.StartupPath;
        private TestMode config_testmode;
        private string Stage_mode = "";
        private string SNHead = "";
        private string finish_time = "";
        private int ItemTestTime = 0;
        private string LogAPIPath = "";

        private string csv_txtlog = "";
        private string csv_jsonlog = "";
        private string csv_ble = "";
        private string csv_wifi = "";
        private string csv_cali = "";

        public bool waive_6g = false;
        public int waive_6g_flag = Convert.ToInt32(IO_ini.ReadIniFile(".//Setting", "Setting", "Waive_6G_Flag", "0"));

        public bool retest_5gcal = false;
        public bool retest_Xtalcal = false;

        public string IQ_BLE_Path = "";
        public string IQ_Wifi_Path = "";
        public string IQ_BLE_File = "";
        public string IQ_Wifi_File = "";
        public string BLE_Testplan = "BLE_Script.txt";
        public string Wifi_TestPlan = "WIFI_Script.txt";
        public string PathLossBLEFileName = "";
        public string PathLossWifiFileName = "";
        public string IQ_Port = "";
        public string IQ_SN = "";
        public string IQ_model = "";
        private string BLE_Log = "";
        private string WIFI_Log = "";
        public string Cali_Log = "";
        public string RX_Cali_Log = "";
        public string Xtal_Cali_Log = "";
        private List<string> BlePort = new List<string> { "1A", "1B", "2A", "2B", "3A", "3B", "4A", "4B" };
        private List<string> WifiPort = new List<string> { "RF1A", "RF1B", "RF2A", "RF2B", "RF3A", "RF3B", "RF4A", "RF4B" };

        public string mydas_ip = "";

        private string sfis_connect = "http://10.90.0.40/sfcapi/api/connect";
        private string sfis_final = "http://10.90.0.40/sfcapi/api/connectfinal";

        private delegate void dSetLabelText(Label lb, string text);
        private delegate void dSetRtbText(RichTextBox rtb, string text);

        //private List<API_TestItem> test_items_json = new List<API_TestItem>();
        
        private Dictionary<string, Golden> GoldenList = new Dictionary<string, Golden>();
        public Dictionary<string, List<string>> IQLog = new Dictionary<string, List<string>>();
        
        public string IQFailKey = "";
        public string IQEndItem = "";
        private Loading loadform;

        private FtpClient ftpPathLoss = null;
        private string ftpPathLoss_IP = "";
        private string ftpPathLoss_User = "";
        private string ftpPathLoss_Password = "";

        private FtpClient ftpLog = null;
        private string ftpLog_IP = "";
        private string ftpLog_User = "";
        private string ftpLog_Password = "";
        private string sdebug = "";

        private bool DebugProgram = false;

        public bool GoldenTest = false;

        private DOS dos = new DOS();

        private string Tools = "";
        private string ToolPath = "";
        private string ToolName = "";

        private string loop = "0";
        private int loopCount = 0;
        private int loopDelay = 0;

        private string csv_ver = "";

        public string AutoScan_Flag = "";
        private string AutoScan_Signal = "";


        private string Main_info = "";
        private string Detail_info = "Still not config";
        private string Error_info = "";

        public SimpleTcpClient cl;
        public int Iperf_sv_flag = 0;
        public string Iperf_IP = "";

        private string MO = "";

        public double t_cpu_0 = 0;
        public double t_cpu_3 = 0;
        public int t_5g_0 = 0;
        public int t_5g_3 = 0;
        public int t_6g_0 = 0;
        public int t_6g_3 = 0;
        public int t_eth_0 = 0;
        public int t_eth_3 = 0;

        public string cal_md5_ipq5332 = "";
        public string cal_md5_qcn6432 = "";

        private double total_pass_qty = 0;
        private double total_fail_qty = 0;

        private string fail_value = "";

        public int fail_count = 0;
        public int fail_fixclose_count = 0;

        private string fail_list = "";

        private bool isLockScreenShowing = false;

        public string TPG_Checksum = "";

        public bool isRevert = false;
        private Dictionary<string, string> ErrorCodeList = new Dictionary<string, string>();

        private Dictionary<Label, Dictionary<string, string>> CSV = new Dictionary<Label, Dictionary<string, string>>();

        private Dictionary<Label, Dictionary<string, API_TestItem>> test_items_json = new Dictionary<Label, Dictionary<string, API_TestItem>>();

        private Dictionary<Label, Detail> _formDetails = new Dictionary<Label, Detail>();

        private Dictionary<Label, TestFunction> _testFunctions = new Dictionary<Label, TestFunction>();

        private Dictionary<Label, Timer> _timers = new Dictionary<Label, Timer>();

        private Dictionary<Label, Timer> _testTimers = new Dictionary<Label, Timer>();

        public Dictionary<Label, DUT> _dutInstances = new Dictionary<Label, DUT>();

        private Dictionary<Label, int> _itemTestTimes = new Dictionary<Label, int>();

        private Dictionary<Label, int> _testTimes = new Dictionary<Label, int>();

        public Dictionary<Label, List<ItemInfo>> _labelItemLists = new Dictionary<Label, List<ItemInfo>>();

        private Dictionary<Label, string> _currentItems = new Dictionary<Label, string>();

        private Dictionary<Label, string> _startTime = new Dictionary<Label, string>();

        private Dictionary<Label, string> error_code = new Dictionary<Label, string>();

        private Dictionary<Label, string> error_description = new Dictionary<Label, string>();

        private Dictionary<Label, string> customer_error_code = new Dictionary<Label, string>();

        private Dictionary<Label, string> customer_error_des = new Dictionary<Label, string>();

        public Dictionary<Label, Status> _Labelstatus = new Dictionary<Label, Status>();

        private Dictionary<Label, string> _testingSN = new Dictionary<Label, string>();

        public Form1()
        {
            //telnet.DataReceived += Telnet_DataReceived(position);
            Thread ShowLoadFormThread = new Thread(new ThreadStart(StartForm));
            ShowLoadFormThread.Start();
            InitializeComponent();
            InitializeLabels();
        }
        private void InitializeLabels()
        {
            foreach (Control ctrl in this.Controls) 
            {
                if (ctrl is Label lbl && lbl.Name.StartsWith("lbl"))
                {
                    string position = lbl.Name.Replace("lbl", "");
                    _formDetails[lbl] = new Detail(this, position);
                    _testFunctions[lbl] = new TestFunction(this, position);

                    _dutInstances[lbl] = new DUT();

                    _labelItemLists[lbl] = new List<ItemInfo>();

                    _testTimes[lbl] = 0;

                    _itemTestTimes[lbl] = 0;

                    _Labelstatus[lbl] = Status.READY;

                    // 🔹 Tạo Timer cho từng Label
                    Timer timer = new Timer();
                    timer.Interval = 1000; // 1 giây
                    timer.Tick += (s, e) => ItemTimer_Tick(lbl); // Gán sự kiện Tick cho Timer
                    _timers[lbl] = timer; // Lưu vào Dictionary

                    Timer testtimer = new Timer();
                    testtimer.Interval = 1000; // 1 giây
                    testtimer.Tick += (s, e) => TestTimer_Tick(lbl); // Gán sự kiện Tick cho Timer
                    _testTimers[lbl] = testtimer; // Lưu vào Dictionary

                    lbl.DoubleClick += Label_DoubleClick; // Gán sự kiện double-click
                }
            }
        }

        private void Label_DoubleClick(object sender, EventArgs e)
        {
            if (sender is Label lbl && _formDetails.TryGetValue(lbl, out Detail detailForm))
            {
                // Đặt tiêu đề của Detail form bằng tên của Label
                detailForm.Text = lbl.Name.Replace("lbl", "");

                if (!detailForm.Visible)
                {
                    this.Invoke((MethodInvoker)(() => detailForm.Show()));
                    //detailForm.Show();
                }
                else
                {
                    if (detailForm.WindowState == FormWindowState.Minimized)
                    {
                        detailForm.WindowState = FormWindowState.Normal;
                    }
                    detailForm.Activate();
                }
            }
        }
        public Label GetLabelByPosition(string position)
        {
            return _dutInstances.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
        }

        public DUT GetDUTByPosition(string position)
        {
            // 🔹 Tìm Label có chứa position trong tên
            Label targetLabel = _dutInstances.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));

            if (targetLabel != null && _dutInstances.TryGetValue(targetLabel, out DUT dut))
            {
                return dut;
            }

            return new DUT(); // Hoặc có thể trả về một DUT mặc định nếu muốn
        }
        public List<ItemInfo> GetItemListByPosition(string position)
        {
            Label targetLabel = _labelItemLists.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));

            if (targetLabel != null && _labelItemLists.TryGetValue(targetLabel, out List<ItemInfo> itemList))
            {
                return itemList; // Trả về danh sách ItemInfo tương ứng với Label
            }

            return null; // Không tìm thấy Label hoặc danh sách ItemInfo
        }
        private Label GetLabelBySN(string sn)
        {
            return _testingSN.FirstOrDefault(pair => pair.Value == sn).Key;
        }

        private void ItemTimer_Tick(Label lbl)
        {
            if (_formDetails.TryGetValue(lbl, out Detail detail))
            {
                if (!_itemTestTimes.ContainsKey(lbl))
                    _itemTestTimes[lbl] = 0; // Nếu chưa có, khởi tạo giá trị

                _itemTestTimes[lbl]++; // Tăng giá trị thời gian

                if (_currentItems.TryGetValue(lbl, out string currentItem))
                {
                    detail.UpdateTime(currentItem, _itemTestTimes[lbl]); // 🔹 Cập nhật theo từng label
                }
            }
        }


        private void TestTimer_Tick(Label lbl)
        {
            _testTimes[lbl]++; // Tăng thời gian riêng cho từng Label

            string title = lbl.Name.Replace("lbl", "");
            string timeText = TimeSpan.FromSeconds(_testTimes[lbl]).ToString(@"mm\:ss");

            lbl.Invoke((MethodInvoker)delegate
            {
                SetLabelText(lbl, $"{title}\n{timeText}");
            });
        }


        public void StartForm()
        {
            loadform = new Loading(this);
            loadform.ShowDialog();
        }

        private string CalculateChecksum(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                MD5 md5 = MD5.Create();
                byte[] checksumBytes = md5.ComputeHash(stream);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < checksumBytes.Length; i++)
                {
                    sb.Append(checksumBytes[i].ToString("X2"));
                }
                string checksum = sb.ToString();
                return checksum.Substring(checksum.Length - 8);
            }
        }

        static string GetLocalIPAddressStartingWith(string prefix)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString().StartsWith(prefix))
                {
                    return ip.ToString();
                }
            }
            return null; // Không tìm thấy IP phù hợp
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                TPG_Checksum = CalculateChecksum(Application.ExecutablePath);
                File.WriteAllText(current_directory + "\\tpgmd5.txt", TPG_Checksum);

                isLockScreenShowing = false;
                fail_count = 0;
                fail_fixclose_count = 0;
                InitUI();
                checkDebug();

                var ipAddress = GetLocalIPAddressStartingWith("10.90");
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    lbServerIP.Text = ipAddress;
                }
                else
                {
                    lbServerIP.Text = "Server error";
                }

                InitEQ();
                CheckAmbitVersion();
                ReadErrorCode();
                ReadGolden();

                if (!IsProcessRunning("CPU_Temperature"))
                {
                    Process.Start(@".\CPU_Temperature.exe");
                }

                Process.Start(@".\RemoveDesktopFiles.exe");

                if (DebugProgram)
                {
                    InitialLimit(current_directory + @"\python\limits1.json");
                }
                else
                {
                    InitialLimit(current_directory + @"\python\limits.json");
                }
                loadform.bClose = true;

                //Thread openCPUtool = new Thread(CPU_Temperature_monitor);
                //openCPUtool.IsBackground = true;
                //openCPUtool.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                Environment.Exit(0);
            }
        }
        private void CPU_Temperature_monitor()
        {
            while (true)
            {
                Thread.Sleep(30000);

                if (!IsProcessRunning("CPU_Temperature"))
                {
                    Process.Start(@".\CPU_Temperature.exe");
                }
            }
        }
        void DeleteAllFile(string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);

                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch
                    {
                    }
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }
        private void DownloadDirectory(string from, string to)
        {
            try
            {
                if (!Directory.Exists(to))
                {
                    Directory.CreateDirectory(to);
                }

                string[] directoryList = ftpClient.Dir(from, true);
                foreach (string directory in directoryList)
                {
                    string[] tokens = directory.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                    string name = tokens[8];
                    string permissions = tokens[0];

                    string localFilePath = Path.Combine(to, name);
                    string fileUrl = from + name;

                    ftpClient.Get(localFilePath, fileUrl);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private delegate void dActive();
        public void Active()
        {
            if (this.InvokeRequired)
            {
                dActive d = new dActive(Active);
                this.Invoke(d);
            }
            else
            {
                this.Activate();
            }
        }

        private void InitUI()
        {
            try
            {
                ChangeTestMode(TestMode.Production);
                ChangeSFCSMode(SFCS_mode.ON);
                //ChangeStatus(Status.READY, position);

                PC_NAME = Environment.MachineName;

                mo_1p = IO_ini.ReadIniFile("Setting", "Setting", "OnePoint", "");
                no_dhcp = IO_ini.ReadIniFile("Setting", "Setting", "NoDHCP", "");
                _station = IO_ini.ReadIniFile("Setting", "Setting", "Station", "");
                _Product = IO_ini.ReadIniFile("Setting", "Setting", "Product", "");
                _PN = IO_ini.ReadIniFile("Setting", "Setting", "PN", "");
                _Type = IO_ini.ReadIniFile("Setting", "Setting", "Type", "Normal");
                _SendAPI = Convert.ToInt32(IO_ini.ReadIniFile("Setting", "Setting", "Send_API", "1"));
                _parseSweepLog = Convert.ToInt32(IO_ini.ReadIniFile("Setting", "Setting", "parseSweepLog", "0"));
                _MBLTPN = IO_ini.ReadIniFile("Setting", "Setting", "MBLTPN", "");
                _MO = IO_ini.ReadIniFile("Setting", "Setting", "MO", "");
                sfis_to_ui = IO_ini.ReadIniFile("Setting", "SFIS", "SFIS_TO_UI", "");
                ui_to_sfis = IO_ini.ReadIniFile("Setting", "SFIS", "UI_TO_SFIS", "");
                py_get_limit = IO_ini.ReadIniFile("Setting", "API", "Get_Limit", "");
                py_send_api = IO_ini.ReadIniFile("Setting", "API", "Upload_API", "");
                Stage_mode = IO_ini.ReadIniFile("Setting", "Setting", "Stage_Mode", "");
                AutoScan_Flag = IO_ini.ReadIniFile("Setting", "AutoScan", "Flag", "0");
                AutoScan_Signal = IO_ini.ReadIniFile("Setting", "AutoScan", "Signal", "NONE");

                SNHead = IO_ini.ReadIniFile("Setting", "Setting", "SN_Head", "XXX");
                sfis_connect = IO_ini.ReadIniFile("Setting", "SFIS", "Connect", "");
                sfis_final = IO_ini.ReadIniFile("Setting", "SFIS", "Final", "");
                IQ_BLE_Path = IO_ini.ReadIniFile("Setting", "IQ", "IQ_BLE_Path", "");
                IQ_BLE_File = IO_ini.ReadIniFile("Setting", "IQ", "IQ_BLE_File", "");
                IQ_Wifi_Path = IO_ini.ReadIniFile("Setting", "IQ", "IQ_Wifi_Path", "");
                IQ_Wifi_File = IO_ini.ReadIniFile("Setting", "IQ", "IQ_Wifi_File", "");

                mydas_ip = IO_ini.ReadIniFile("AutoDL_Setting", "Setting", "Server_IP", "10.90.10.168");

                ftpPathLoss_IP = IO_ini.ReadIniFile("Setting", "PathLoss", "FTP_IP", "");
                ftpPathLoss_User = IO_ini.ReadIniFile("Setting", "PathLoss", "FTP_User", "");
                ftpPathLoss_Password = IO_ini.ReadIniFile("Setting", "PathLoss", "FTP_Pass", "");

                ftpLog_IP = IO_ini.ReadIniFile("Setting", "FTPLOG", "FTP_IP", "");
                ftpLog_User = IO_ini.ReadIniFile("Setting", "FTPLOG", "FTP_User", "");
                ftpLog_Password = IO_ini.ReadIniFile("Setting", "FTPLOG", "FTP_Pass", "");

                Tools = IO_ini.ReadIniFile("Setting", "Tools", "Tools", "0");
                ToolPath = IO_ini.ReadIniFile("Setting", "Tools", "Path", "");
                ToolName = IO_ini.ReadIniFile("Setting", "Tools", "Name", "");

                loop = IO_ini.ReadIniFile("Setting", "Setting", "Loop", "0");
                loopCount = Convert.ToInt32(IO_ini.ReadIniFile("Setting", "Setting", "LoopCount", "1"));
                loopDelay = Convert.ToInt32(IO_ini.ReadIniFile("Setting", "Setting", "LoopDelay", "1"));

                Iperf_sv_flag = Convert.ToInt32(IO_ini.ReadIniFile("Setting", "IperfServer", "Flag", "0"));
                Iperf_IP = IO_ini.ReadIniFile("Setting", "IperfServer", "IP", "10.0.0.2");

                sdebug = IO_ini.ReadIniFile("Setting", "Setting", "Debug", "");

                MO = IO_ini.ReadIniFile("Setting", "Setting", "MO", "");

                BuildName = IO_ini.ReadIniFile("Public_Setting", "Setting", "BuildName", "");

                if (_station == "MBFT")
                {
                    ftpPathLoss = new FtpClient(ftpPathLoss_IP, ftpPathLoss_User, ftpPathLoss_Password);
                }

                ftpLog = new FtpClient(ftpLog_IP, ftpLog_User, ftpLog_Password);

                string test_mode = IO_ini.ReadIniFile("Setting", "Setting", "Test_Mode", "").ToLower();
                switch (test_mode)
                {
                    case "production":
                        config_testmode = TestMode.Production;
                        ChangeTestMode(TestMode.Production);
                        break;
                    case "rma":
                        config_testmode = TestMode.RMA;
                        ChangeTestMode(TestMode.RMA);
                        break;
                    case "reliability":
                        config_testmode = TestMode.RELIABILITY;
                        ChangeTestMode(TestMode.RELIABILITY);
                        break;
                }

                //lblTestTime.Text = testTime.ToString();
                if (!string.IsNullOrEmpty(_MBLTPN.Trim()))
                {
                    lbPN.Text = _MBLTPN;
                }
                else
                {
                    lbPN.Text = _PN;
                }
                lbType.Text = _Type;
                lbBuildName.Text = BuildName;
                total_pass_qty = 0;
                total_fail_qty = 0;
                //lblErrorCode.Text = "";
                //lblTestIem.Text = "";
                fail_list = "";
                old_sn = "";
                lbPCName.Text = PC_NAME;
                DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
                this.Text = _Product + " - " + _PN + " - " + _station + " - " + version + " - AutoDl at : " + buildDate;
                //_formDetail = new Detail(this);
                //_formDetail.Show();
                SetTime();
                LogFilePath = LogFilePath + _Product + "\\" + _PN + "\\";
            }
            catch (Exception ex)
            {
                MessageBox.Show("InitUI Exception : " + ex.Message);
                Environment.Exit(0);
            }

        }
        public static string HexToStr(string s)
        {
            string empty = string.Empty;
            s = s.Replace(" ", "");
            byte[] bytes = new byte[s.Length / 2];
            int num1 = 0;
            for (int startIndex = 0; startIndex < s.Length; startIndex += 2)
            {
                try
                {
                    bytes[num1++] = Convert.ToByte(s.Substring(startIndex, 2), 16);
                }
                catch
                {
                }
            }
            return Encoding.Default.GetString(bytes).Replace("\0", "");
        }
        
        private void InitEQ()
        {
            try
            {
                if (DebugProgram)
                {
                    return;
                }
                dos.KillTaskProcess(ToolName.Replace(".exe", "").Trim());
                Thread.Sleep(300);
                ReNameFile();

                if (!DownloadLimit())
                {
                    MessageBox.Show("Download Limit Fail !\r\nKhông tải được file limit !");
                    Environment.Exit(0);
                }

                #region Open Tool
                if (Tools == "1")
                {
                    string sReceive = "";
                    try
                    {
                        //dos.KillTaskProcess(ToolName.Replace(".exe", "").Trim());
                        //Thread.Sleep(500);
                        if (!dos.CheckTaskProcess(ToolName.Replace(".exe", "").Trim()))
                        {
                            dos.ExecuteDOSCommand(ToolPath + "\\" + ToolName, "", false, ref sReceive, 10, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Environment.Exit(0);
                    }
                }
                #endregion
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Init EQ Exception : " + ex.Message);
                Environment.Exit(0);
            }
        }

        private void ReNameFile()
        {
            try
            {
                string[] files = Directory.GetFiles(current_directory);
                foreach (string file in files)
                {
                    if (Path.GetExtension(file) == ".ddl")
                    {
                        try
                        {
                            if (File.Exists(file.Replace(".ddl", ".dll")))
                            {
                                File.Delete(file.Replace(".ddl", ".dll"));
                            }
                            File.Move(file, file.Replace(".ddl", ".dll"));
                        }
                        catch { }
                    }
                    else if (Path.GetExtension(file) == ".exx")
                    {
                        try
                        {
                            if (File.Exists(file.Replace(".exx", ".exe")))
                            {
                                File.Delete(file.Replace(".exx", ".exe"));
                            }
                            File.Move(file, file.Replace(".exx", ".exe"));
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rename File Exception : " + ex.Message);
            }
        }
        
        private bool DownloadLimit()
        {
            try
            {
                string error = "";
                string received = "";
                dos.ExecuteCMDWithInputStream(Application.StartupPath + @"\python", py_get_limit + " " + _station, ref error, ref received);
                if (!String.IsNullOrEmpty(error))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            finally
            {

            }
            return false;
        }

        private void checkDebug()
        {
            try
            {
                if (sdebug.Contains("1"))
                {
                    DebugProgram = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check SFIS IP Exception : " + ex.Message);
                Environment.Exit(0);
            }
        }

        private void InitialLimit(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("Don't have " + path);
                    Environment.Exit(0);
                }

                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    limits = JsonConvert.DeserializeObject<Limits>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Init Limit Exception : " + ex.Message);
                Environment.Exit(0);
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
        private string SendToSFCS(string url, string message)
        {
            string rs = "";
            try
            {
                WriteToFile(sfis_log_path + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + ".txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + " : UI=>SFIS : " + message, true);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.Accept = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(message);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    rs = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                rs = "Exception : " + ex.Message;
            }
            finally
            {
                WriteToFile(sfis_log_path + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + ".txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + " : SFIS=>UI : " + rs, true);
            }

            return rs;
        }

        private void ReadGolden()
        {
            try
            {
                if (!File.Exists("Golden.txt"))
                {
                    return;
                }
                string[] contents = File.ReadAllLines("Golden.txt");
                foreach (string line in contents)
                {
                    if (line.Contains(","))
                    {
                        Golden g = new Golden();
                        string[] parts = line.Trim().Split(',');
                        if (parts.Length == 2)
                        {
                            g.mblsn = parts[0].Trim();
                            g.mac = parts[1].Trim();
                        }
                        else if (parts.Length == 3)
                        {
                            g.mblsn = parts[1].Trim();
                            g.sn = parts[0].Trim();
                            g.mac = parts[2].Trim();
                        }

                        GoldenList.Add(g.mblsn, g);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Read Golden.txt Fail !\r\n" + ex.Message);
            }
        }

        private void ReadCSV_Template(string position)
        {
            try
            {
                if (!File.Exists("CSV_Template.txt"))
                {
                    MessageBox.Show("CSV_Template.txt not exists !\r\nKhông có file CSV_Template.txt !");
                    Environment.Exit(0);
                }

                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                if (targetLabel == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy Label cho vị trí: {position}");
                    return;
                }

                string[] contents = File.ReadAllLines("CSV_Template.txt");

                if (!CSV.ContainsKey(targetLabel))
                    CSV[targetLabel] = new Dictionary<string, string>();

                var csvDict = CSV[targetLabel];
                csvDict.Clear();

                foreach (string line in contents)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    if (line.Contains("Version"))
                    {
                        csv_ver = line.Substring(line.IndexOf("=") + 1);
                    }
                    else
                    {
                        string key = line.Trim();
                        if (!csvDict.ContainsKey(key))
                            csvDict.Add(key, "");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Read CSV_Template.txt Fail !\r\n" + ex.Message);
                Environment.Exit(0);
            }
        }

        private void ReadErrorCode()
        {
            string current_key = "";
            try
            {
                if (!File.Exists("Error_Code.txt"))
                {
                    MessageBox.Show("Error_Code.txt not exists !\r\nKhông có file Error_Code.txt !");
                    Environment.Exit(0);
                }

                string[] contents = File.ReadAllLines("Error_Code.txt");
                foreach (string line in contents)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    if (line.Contains(","))
                    {
                        current_key = line;
                        ErrorCodeList.Add(line.Trim().Split(',')[1], line.Trim().Split(',')[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Read Error_Code.txt Fail !\r\n" + ex.Message + "\r\n" + current_key);
                Environment.Exit(0);
            }
        }

        private void ReadAmbitConfig(Label lbl)
        {
            try
            {
                if (!_labelItemLists.TryGetValue(lbl, out List<ItemInfo> listItems) || listItems == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy danh sách ItemInfo cho vị trí: {lbl.Name}");
                    return;
                }

                using (StreamReader r = new StreamReader("AmbitConfig.txt"))
                {
                    string json = r.ReadToEnd();
                    List<ItemInfo> newList = JsonConvert.DeserializeObject<List<ItemInfo>>(json);

                    if (newList != null)
                    {
                        // Loại bỏ các Item có Flag == 0
                        newList.RemoveAll(item => item.Flag == 0);

                        // 🔹 Cập nhật vào _labelItemLists[lbl]
                        _labelItemLists[lbl] = newList;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Read AmbitConfig Exception : " + ex.Message);
                Environment.Exit(0);
            }
        }

        private void ChangeTestMode(TestMode mode)
        {
            if (mode == TestMode.Debug)
            {
                btnTestMode.Text = "DEBUG";
                testmode = TestMode.Debug;
                btnTestMode.BackColor = Color.Tomato;
                btnTestMode.ForeColor = Color.Black;
                //btnTest.Enabled = true;
            }
            else
            {
                btnTestMode.Text = config_testmode.ToString();
                testmode = config_testmode;
                btnTestMode.BackColor = Color.Lime;
                btnTestMode.ForeColor = Color.Black;
                //btnTest.Enabled = false;
            }
        }

        private void ChangeSFCSMode(SFCS_mode mode)
        {
            if (mode == SFCS_mode.ON)
            {
                btnSFCS.Text = "SFCS ON";
                SFCSmode = SFCS_mode.ON;
                btnSFCS.BackColor = Color.Lime;
                btnTestMode.ForeColor = Color.Black;
            }
            else
            {
                btnSFCS.Text = "SFCS OFF";
                SFCSmode = SFCS_mode.OFF;
                btnSFCS.BackColor = Color.Tomato;
                btnTestMode.ForeColor = Color.Black;
            }
        }

        delegate void dChangeStatus(Status status, string position);
        private void ChangeStatus(Status status, string position)
        {
            if (this.InvokeRequired)
            {
                dChangeStatus d = new dChangeStatus(ChangeStatus);
                this.Invoke(d, new object[] { status, position });
            }
            else
            {
                // 🔹 Tìm Label theo vị trí
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));

                if (targetLabel != null)
                {
                    // 🔹 Tìm đúng Timer của Label đó
                    if (_testTimers.TryGetValue(targetLabel, out Timer testTimer))
                    {
                        switch (status)
                        {
                            case Status.READY:
                                targetLabel.BackColor = Color.SkyBlue;
                                targetLabel.ForeColor = Color.White;
                                _Labelstatus[targetLabel] = Status.READY;
                                break;

                            case Status.TESTING:
                                targetLabel.BackColor = Color.Yellow;
                                targetLabel.ForeColor = Color.Black;
                                _testTimes[targetLabel] = 0;
                                _Labelstatus[targetLabel] = Status.TESTING;
                                testTimer.Enabled = true; // Bắt đầu đếm thời gian
                                _startTime[targetLabel] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                break;

                            case Status.FAIL:
                                targetLabel.BackColor = Color.Red;
                                targetLabel.ForeColor = Color.Yellow;
                                testTimer.Enabled = false; // Dừng timer
                                _Labelstatus[targetLabel] = Status.FAIL;
                                _testingSN[targetLabel] = "";
                                break;

                            case Status.PASS:
                                targetLabel.BackColor = Color.Lime;
                                targetLabel.ForeColor = Color.Blue;
                                testTimer.Enabled = false; // Dừng timer
                                _Labelstatus[targetLabel] = Status.PASS;
                                _testingSN[targetLabel] = "";
                                break;

                            case Status.WARNING:
                                targetLabel.BackColor = Color.LightSalmon;
                                targetLabel.ForeColor = Color.Yellow;
                                _Labelstatus[targetLabel] = Status.WARNING;
                                break;
                        }
                    }
                }
                else
                {
                    // 🔹 Nếu không tìm thấy label, log lỗi hoặc xử lý mặc định
                    MessageBox.Show($"⚠ Không tìm thấy label cho vị trí: {position}");
                }
            }
        }

        private void btnSFCS_Click(object sender, EventArgs e)
        {
            if (SFCSmode == SFCS_mode.ON)
            {
                ChangeSFCSMode(SFCS_mode.OFF);
                ChangeTestMode(TestMode.Debug);
            }
            else
            {
                ChangeSFCSMode(SFCS_mode.ON);
                ChangeTestMode(config_testmode);
            }
        }

        private void SetTime()
        {
            log_time = DateTime.Now.ToString("yyyy-MM-dd_HHmmss", CultureInfo.InvariantCulture);
        }
        private string ftpServerIP = IO_ini.ReadIniFile("AutoDL_Setting", "Setting", "Server_IP", "");
        private string ftpUserID = "AutoDeploy";
        private string ftpPassword = "autodeploy!!";
        private FTPClient ftpClient;

        private void ConnectServer()
        {
            ftpClient = new FTPClient();
            ftpClient.RemoteHost = ftpServerIP;
            ftpClient.Connect();
            ftpClient.ConnectMode = FTPConnectMode.PASV;
            ftpClient.Login(ftpUserID, ftpPassword);
            ftpClient.TransferType = FTPTransferType.BINARY;
        }
        private string CreateMD5(string filename)
        {
            byte[] data;
            using (System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] input;
                using (FileStream fileStream = File.OpenRead(filename))
                {
                    input = new byte[fileStream.Length];
                    fileStream.Position = 0L;
                    fileStream.Read(input, 0, input.Length);
                }

                data = md5.ComputeHash(input);
            }
            return ToHexa(data);
        }

        private static string ToHexa(byte[] data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.AppendFormat("{0:X2}", data[i]);
            }
            return stringBuilder.ToString();
        }

        private bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            return processes.Length > 0;
        }
        public string lock_status = "";
        private void StartTest(string position)
        {
            try
            {
                DUT myDut = GetDUTByPosition(position);
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                if (!Environment.MachineName.ToUpper().Equals("RTT-9600") && _PN != "SET-IP")
                {
                    //if (testmode == TestMode.Production || testmode == TestMode.RELIABILITY)
                    //{
                    //    string registryPath = @"Software\" + _Product + "\\" + _station;
                    //    string value = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Unlock_Status", null) as string;

                    //    if (value == "0")
                    //    {
                    //        //SetLabelText(lblStatus, "Sau khi mở khóa máy cần test Debug PASS thì mới có thể tiếp tục chạy");
                    //        return;
                    //    }
                    //}
                    ////Check if fail 3 times will lock screen
                    ////fail_count = 3;
                    //if (fail_count == 3)
                    //{
                    //    AddLog("Fail continue 3 times => lock screen", position);
                    //    string registryPath = @"Software\" + _Product + "\\" + _station;
                    //    // Ghi giá trị vào registry
                    //    Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Unlock_Status", "0");

                    //    // Kiểm tra giá trị đã ghi
                    //    string value = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Unlock_Status", null) as string;
                    //    if (value == null)
                    //    {
                    //        //SetLabelText(lblStatus, "Chương trình lỗi => Gọi TE-Pro");
                    //        return;
                    //    }

                    //    isLockScreenShowing = true;
                    //    lock_status = "FAIL 3 bản liên tiếp";
                    //    LockScreen ls = new LockScreen(this);
                    //    ls.FormClosed += (s, e) => isLockScreenShowing = false;
                    //    ls.ShowDialog();
                    //    fail_count = 0;
                    //    return;
                    //}
                    ////fail_fixclose_count = 2;
                    //if (fail_fixclose_count == 2)
                    //{
                    //    AddLog("Fail fix_close continue 2 times => lock screen", position);
                    //    string registryPath = @"Software\" + _Product + "\\" + _station;
                    //    // Ghi giá trị vào registry
                    //    Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Unlock_Status", "0");

                    //    // Kiểm tra giá trị đã ghi
                    //    string value = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Unlock_Status", null) as string;
                    //    if (value == null)
                    //    {
                    //        //SetLabelText(lblStatus, "Chương trình lỗi => Gọi TE-Pro");
                    //        return;
                    //    }

                    //    isLockScreenShowing = true;
                    //    lock_status = "Fix.clsose 2 bản liên tiếp";
                    //    LockScreen ls = new LockScreen(this);
                    //    ls.FormClosed += (s, e) => isLockScreenShowing = false;
                    //    ls.ShowDialog();
                    //    fail_fixclose_count = 0;
                    //    return;
                    //}

                    //if (isSETIP)
                    //{
                    //    isLockScreenShowing = true;
                    //    lock_status = "SET IP FAIL";
                    //    LockScreen ls = new LockScreen(this);
                    //    ls.FormClosed += (s, e) => isLockScreenShowing = false;
                    //    ls.ShowDialog();
                    //    isSETIP = false;
                    //    return;
                    //}

                    //Check Datetime : Need use UTC time zone
                    //DateTime localTime = DateTime.Now;
                    //DateTime utcTime = localTime.ToUniversalTime();
                    //DateTime currentUtcTime = DateTime.UtcNow;
                    //TimeSpan timeDifference = localTime - currentUtcTime;

                    //if (timeDifference.TotalSeconds < 0 || timeDifference.TotalSeconds > 0)
                    //{
                    //    SetLabelText(lblStatus, "Sai múi giờ, Gọi TE chỉnh lại thời gian và Autodl lại chương trình !");
                    //    return;
                    //}

                    //Compare TPG on server and local

                    if (!string.IsNullOrEmpty(ftpServerIP.Trim()) && _PN != "Golden" && _PN != "SWEEP" && _PN != "Customer" && _PN != "Customer-Combine")
                    {
                        ConnectServer();
                        string configDir = "/CONFIG/" + _Product + "/" + _PN + "/";
                        ftpClient.Get("md5.ini", configDir + "AmbitMD5.ini");
                        string svdata = File.ReadAllText(@"./md5.ini");
                        //AddLog("Md5 data:\r\n" + svdata);
                        string ServerMD5 = IO_ini.ReadIniFile("md5", _PN, _station, "-1");
                        AddLog("Server md5: " + ServerMD5, position);
                        if (ServerMD5 == "-1")
                        {
                            MessageBox.Show("Không lấy được checksum của chương trình, Gọi TE-PRO kiểm tra !");
                            return;
                        }
                        string LocalMD5 = CreateMD5(@".\AmbitUI.exe").ToLower();
                        AddLog("Local md5: " + LocalMD5, position);
                        if (!ServerMD5.Trim().Equals(LocalMD5.Trim()))
                        {
                            MessageBox.Show("Bạn đang dùng chương trình cũ, hãy Autodl lại !");
                            return;
                        }
                    }

                    ////Cable used times
                    //if (_station == "MBFT")
                    //{
                    //    string registryPath = @"Software\" + _Product + "\\" + _station;

                    //    string power_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Power_Cable", null) as string;
                    //    if (power_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Power_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int power_ut_new = Convert.ToInt32(power_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Power_Cable", power_ut_new.ToString());
                    //    }
                    //    string eth_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Ethernet_Cable", null) as string;
                    //    if (eth_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Ethernet_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int eth_ut_new = Convert.ToInt32(eth_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Ethernet_Cable", eth_ut_new.ToString());
                    //    }
                    //    string ant1_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna1_Cable", null) as string;
                    //    if (ant1_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna1_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant1_ut_new = Convert.ToInt32(ant1_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna1_Cable", ant1_ut_new.ToString());
                    //    }
                    //    string ant2_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna2_Cable", null) as string;
                    //    if (ant2_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna2_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant2_ut_new = Convert.ToInt32(ant2_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna2_Cable", ant2_ut_new.ToString());
                    //    }
                    //    string ant3_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna3_Cable", null) as string;
                    //    if (ant3_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna3_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant3_ut_new = Convert.ToInt32(ant3_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna3_Cable", ant3_ut_new.ToString());
                    //    }
                    //    string ant4_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna4_Cable", null) as string;
                    //    if (ant4_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna4_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant4_ut_new = Convert.ToInt32(ant4_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna4_Cable", ant4_ut_new.ToString());
                    //    }
                    //    string ant5_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna5_Cable", null) as string;
                    //    if (ant5_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna5_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant5_ut_new = Convert.ToInt32(ant5_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna5_Cable", ant5_ut_new.ToString());
                    //    }
                    //    string ant6_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna6_Cable", null) as string;
                    //    if (ant6_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna6_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant6_ut_new = Convert.ToInt32(ant6_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna6_Cable", ant6_ut_new.ToString());
                    //    }
                    //    string ant7_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna7_Cable", null) as string;
                    //    if (ant7_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna7_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant7_ut_new = Convert.ToInt32(ant7_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna7_Cable", ant7_ut_new.ToString());
                    //    }
                    //    string ant8_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna8_Cable", null) as string;
                    //    if (ant8_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna8_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant8_ut_new = Convert.ToInt32(ant8_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna8_Cable", ant8_ut_new.ToString());
                    //    }
                    //    string ant9_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna9_Cable", null) as string;
                    //    if (ant9_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna9_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant9_ut_new = Convert.ToInt32(ant9_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna9_Cable", ant9_ut_new.ToString());
                    //    }
                    //    string ant10_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna10_Cable", null) as string;
                    //    if (ant10_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna10_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant10_ut_new = Convert.ToInt32(ant10_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna10_Cable", ant10_ut_new.ToString());
                    //    }
                    //    string ant11_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Antenna11_Cable", null) as string;
                    //    if (ant11_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna11_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int ant11_ut_new = Convert.ToInt32(ant11_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Antenna11_Cable", ant11_ut_new.ToString());
                    //    }
                    //}
                    //else
                    //{
                    //    string registryPath = @"Software\" + _Product + "\\" + _station;

                    //    string power_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Power_Cable", null) as string;
                    //    if (power_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Power_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int power_ut_new = Convert.ToInt32(power_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Power_Cable", power_ut_new.ToString());
                    //    }
                    //    string eth_ut = Registry.GetValue(@"HKEY_CURRENT_CONFIG\" + registryPath, "Ethernet_Cable", null) as string;
                    //    if (eth_ut == null)
                    //    {
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Ethernet_Cable", "1");
                    //    }
                    //    else
                    //    {
                    //        int eth_ut_new = Convert.ToInt32(eth_ut) + 1;
                    //        Registry.CurrentConfig.CreateSubKey(registryPath).SetValue("Ethernet_Cable", eth_ut_new.ToString());
                    //    }
                    //}

                    ////Check if firewall enable will be disable
                    //if (!Environment.MachineName.ToUpper().Equals("MBFT-9200"))
                    //{
                    //    INetFwPolicy2 policy2 = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                    //    NET_FW_PROFILE_TYPE2_ publicfw = NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC;
                    //    NET_FW_PROFILE_TYPE2_ privatefw = NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE;
                    //    if (policy2.get_FirewallEnabled(privatefw))
                    //    {
                    //        policy2.set_FirewallEnabled(privatefw, false);
                    //    }

                    //    if (policy2.get_FirewallEnabled(publicfw))
                    //    {
                    //        policy2.set_FirewallEnabled(publicfw, false);
                    //    }
                    //}
                }
                if (btnSFCS.InvokeRequired)
                {
                    btnSFCS.Invoke((MethodInvoker)(() => btnSFCS.Enabled = false));
                }
                else
                {
                    btnSFCS.Enabled = false;
                }

                ReadCSV_Template(position);
                ResetData(position);
                SetTime();
                AddLog("-----------------------------------------------------------", position);
                AddLog("Product : " + _Product + ", Station : " + _station, position);
                AddLog("Fixture : " + PC_NAME, position);
                AddLog("Test Mode : " + testmode, position);
                if (GoldenTest)
                {
                    AddLog("Golden Test", position);
                }
                else
                {
                    AddLog("Normal Test", position);
                }
                AddLog("-----------------------------------------------------------", position);
                SetButtonEnable(btnSFCS, false);
                if (_Labelstatus[targetLabel] == Status.TESTING)
                {
                    return;
                }

                ChangeStatus(Status.TESTING, position);

                Main_info += myDut.mlbsn + ",";

                Test(position);
                TestFinish(position);
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, position);
                AddLog(ex.StackTrace, position);
                ChangeStatus(Status.FAIL, position);
            }
            finally
            {
                SetButtonEnable(btnSFCS, true);

                Label targetLabel = _dutInstances.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));

                if (targetLabel != null)
                {
                    _dutInstances[targetLabel] = new DUT(); // 🔄 Khởi tạo lại DUT
                }
                else
                {
                    MessageBox.Show($"⚠ Không tìm thấy DUT cho vị trí: {position}");
                }

                SetTime();
            }
        }
        private bool specical_errorcode = false;
        private void Test(string position)
        {
            try
            {
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                if (targetLabel == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy Label cho vị trí: {position}");
                    return;
                }
                ReadAmbitConfig(targetLabel);
                if (!_labelItemLists.TryGetValue(targetLabel, out List<ItemInfo> ListItems) || ListItems == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy danh sách ItemInfo cho vị trí: {position}");
                    return;
                }
                for (int i = 0; i < ListItems.Count; i++)
                {
                    if ((CheckGoNoGo(position) && ListItems[i].Flag == 1) || (ListItems[i].Flag == 5))
                    {
                        _itemTestTimes[targetLabel] = 0;
                        _currentItems[targetLabel] = ListItems[i].ItemName;
                        AddLog("ITEM = ID[" + (i + 1) + "] NAME[" + ListItems[i].ItemName + "]", position);
                        AddLog("Function Name : " + ListItems[i].ItemDes, position);
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        StartItemTimer(position);

                        if (targetLabel != null && _formDetails.TryGetValue(targetLabel, out Detail formDetail))
                        {
                            formDetail.AddItem(i + 1, ListItems[i].ItemName);
                        }
                        else
                        {
                            MessageBox.Show("Form Detail not found !");
                            return;
                        }
                        
                        for (int r = 0; r <= ListItems[i].RetryValue; r++)
                        {
                            if (r > 0)
                            {
                                AddLog("==> Start Retry " + r, position);
                                ResetError(position);
                                ListItems[i].TestValue = "";
                            }
                            ListItems[i].StartTime = DateTime.Now;
                            ListItems[i].TestResult = TestFunction(i, r, position);
                            if (ListItems[i].TestResult)
                            {
                                break;
                            }
                        }
                        if (!ListItems[i].TestResult)
                        {
                            fail_value = ListItems[i].TestValue;
                        }
                        StopItemTimer(position);
                        stopwatch.Stop();
                        TimeSpan ts = stopwatch.Elapsed;
                        ListItems[i].test_time = string.Format("{0:N3}", ts.TotalSeconds);
                        formDetail.UpdateResult(ListItems[i].ItemName, ListItems[i].TestResult, ListItems[i].test_time);
                        AddLog("Test Time : " + ListItems[i].test_time + "\r\n", position);
                        AddLog("-----------------------------------------------------------", position);
                        AddToAPI(i, position);
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, position);
                AddLog(ex.StackTrace, position);
                ChangeStatus(Status.FAIL, position);
            }
            finally
            {
                SetButtonEnable(btnSFCS, true);

                //Label targetLabel = _dutInstances.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));

                //if (targetLabel != null)
                //{
                //    _dutInstances[targetLabel] = new DUT(); // 🔄 Khởi tạo lại DUT
                //}
                //else
                //{
                //    MessageBox.Show($"⚠ Không tìm thấy DUT cho vị trí: {position}");
                //}
            }
        }

        public void TestDebug(List<int> array, string position)
        {
            try
            {
                ChangeSFCSMode(SFCS_mode.OFF);
                ChangeTestMode(TestMode.Debug);
                SetTime();
                AddLog("Product : " + _Product + ", Station : " + _station, position);
                AddLog("Test Mode : " + testmode, position);
                AddLog("-----------------------------------------------------------", position);

                SetButtonEnable(btnSFCS, false);
                
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                if (targetLabel == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy Label cho vị trí: {position}");
                    return;
                }

                if (!_labelItemLists.TryGetValue(targetLabel, out List<ItemInfo> ListItems) || ListItems == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy danh sách ItemInfo cho vị trí: {position}");
                    return;
                }
                if (_Labelstatus[targetLabel] == Status.TESTING)
                {
                    return;
                }
                for (int i = 0; i < ListItems.Count; i++)
                {
                    if (array.Contains(i))
                    {
                        _itemTestTimes[targetLabel] = 0;
                        _currentItems[targetLabel] = ListItems[i].ItemName;


                        if (targetLabel != null && _formDetails.TryGetValue(targetLabel, out Detail formDetail))
                        {
                            formDetail.UpdateStatus(ListItems[i].ItemName, "Testing");
                        }
                        else
                        {
                            MessageBox.Show("Form Detail not found !");
                            return;
                        }
                        AddLog("ITEM = ID[" + (i + 1) + "] NAME[" + ListItems[i].ItemName + "]", position);
                        AddLog("Function Name : " + ListItems[i].ItemDes, position);
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        StartItemTimer(position);
                        for (int r = 0; r <= ListItems[i].RetryValue; r++)
                        {
                            if (r > 0)
                            {
                                AddLog("==> Start Retry " + r, position);
                                ResetError(position);
                                ListItems[i].TestValue = "";
                            }
                            ListItems[i].StartTime = DateTime.Now;
                            ListItems[i].TestResult = TestFunction(i, r, position);
                            if (ListItems[i].TestResult)
                            {
                                break;
                            }
                        }
                        StopItemTimer(position);
                        stopwatch.Stop();
                        TimeSpan ts = stopwatch.Elapsed;
                        ListItems[i].test_time = string.Format("{0:N3}", ts.TotalSeconds);
                        formDetail.UpdateResult(ListItems[i].ItemName, ListItems[i].TestResult, ListItems[i].test_time);
                        AddLog("-----------------------------------------------------------", position);
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, position);
                AddLog(ex.StackTrace, position);
            }
            finally
            {
                SetButtonEnable(btnSFCS, true);

                //Label targetLabel = _dutInstances.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));

                //if (targetLabel != null)
                //{
                //    _dutInstances[targetLabel] = new DUT(); // 🔄 Khởi tạo lại DUT
                //}
                //else
                //{
                //    MessageBox.Show($"⚠ Không tìm thấy DUT cho vị trí: {position}");
                //}

                SetTime();
            }
        }
        private void AddToAPI(int i, string position)
        {
            Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
            if (targetLabel == null)
            {
                MessageBox.Show($"⚠ Không tìm thấy Label cho vị trí: {position}");
                return;
            }

            if (!_labelItemLists.TryGetValue(targetLabel, out List<ItemInfo> ListItems) || ListItems == null)
            {
                MessageBox.Show($"⚠ Không tìm thấy danh sách ItemInfo cho vị trí: {position}");
                return;
            }
            if (!string.IsNullOrEmpty(ListItems[i].API_NAME.Trim()) && !ListItems[i].API_NAME.Contains(","))
            {
                AddToEeroAPI(ListItems[i].API_NAME, ListItems[i].TestResult, ListItems[i].TestValue, i, position, true);
            }
            else if (limits.limits.ContainsKey(ListItems[i].ItemName.Replace("_0", "").Replace("_1", "").Replace("_2", "").Replace("_3", "").Replace("_4", "").Replace("_5", "").Replace("_6", "").Replace("_7", "").Replace("_8", "").Trim()))
            {
                AddToEeroAPI(ListItems[i].ItemName, ListItems[i].TestResult, ListItems[i].TestValue, i, position, true);
            }
            else
            {
                if (!ListItems[i].TestResult && !string.IsNullOrEmpty(ListItems[i].API_NAME))
                {
                    if (!ListItems[i].API_NAME.Contains(","))
                    {
                        AddToEeroAPI(ListItems[i].API_NAME, ListItems[i].TestResult, ListItems[i].TestValue, i, position, true);
                    }
                    else if (string.IsNullOrEmpty(customer_error_code[targetLabel]))
                    {
                        AddToEeroAPI(ListItems[i].API_NAME.Split(',')[0], ListItems[i].TestResult, ListItems[i].TestValue, i, position, true);
                    }
                }
            }
        }

        private delegate void dUpdateItemTimer();
        private void StartItemTimer(string position)
        {
            if (this.InvokeRequired)
            {
                dUpdateItemTimer d = new dUpdateItemTimer(() => StartItemTimer(position));
                this.Invoke(d);
            }
            else
            {
                Label lbl = _timers.Keys.FirstOrDefault(l => l.Name.Contains(position));

                if (lbl != null && _timers.TryGetValue(lbl, out Timer timer))
                {
                    lbl.Tag = 0; // Reset thời gian
                    timer.Start();
                }
            }
        }

        private delegate void dUpdateProgressBar();
        private void UpdateProgressBar()
        {
            if (this.InvokeRequired)
            {
                dUpdateProgressBar d = new dUpdateProgressBar(UpdateProgressBar);
                this.Invoke(d);
            }
            else
            {
                //progressBar1.PerformStep();
            }
        }

        private delegate void dInitProgressBar(int max);
        private void InitProgressBar(int max)
        {
            if (this.InvokeRequired)
            {
                dInitProgressBar d = new dInitProgressBar(InitProgressBar);
                this.Invoke(d, new object[] { max });
            }
            else
            {
                //progressBar1.Maximum = max;
                //progressBar1.Minimum = 0;
                //progressBar1.Value = 0;
                //progressBar1.Step = 1;
            }
        }
        private void StopItemTimer(string position)
        {
            if (this.InvokeRequired)
            {
                dUpdateItemTimer d = new dUpdateItemTimer(() => StopItemTimer(position));
                this.Invoke(d);
            }
            else
            {
                Label lbl = _timers.Keys.FirstOrDefault(l => l.Name.Contains(position));

                if (lbl != null && _timers.TryGetValue(lbl, out Timer timer))
                {
                    timer.Stop();
                }
            }
        }

        delegate void dSetButtonEnable(Button bt, bool anable);
        private void SetButtonEnable(Button bt, bool enable)
        {
            if (bt.InvokeRequired)
            {
                dSetButtonEnable d = new dSetButtonEnable(SetButtonEnable);
                bt.Invoke(d, new object[] { bt, enable });
            }
            else
            {
                bt.Enabled = enable;
            }
        }

        private void ResetData(string position)
        {
            try
            {
                ResetError(position);
                //DeleteOldLog(current_directory + @"\python\log\", position);
                IQLog.Clear();
                IQFailKey = "";
                IQEndItem = "";
                BLE_Log = "";
                WIFI_Log = "";
                Cali_Log = "";
                Main_info = "";
                Detail_info = "Still not config";
                Error_info = "";
                CycleTime = 0;
                t_cpu_0 = 0;
                t_cpu_3 = 0;
                t_5g_0 = 0;
                t_5g_3 = 0;
                t_6g_0 = 0;
                t_6g_3 = 0;
                t_eth_0 = 0;
                t_eth_3 = 0;

                ResetCSV(position);
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                if (targetLabel == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy Label cho vị trí: {position}");
                    return;
                }

                if (!_labelItemLists.TryGetValue(targetLabel, out List<ItemInfo> ListItems) || ListItems == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy danh sách ItemInfo cho vị trí: {position}");
                    return;
                }
                //if (test_items_json[targetLabel] != null) {
                //    test_items_json[targetLabel].Clear();
                //}
                
                test_items_json[targetLabel] = new Dictionary<string, API_TestItem>();
                foreach (ItemInfo i in ListItems)
                {
                    i.TestResult = true;
                    i.TestValue = "";
                    i.test_time = "";
                }
                if (targetLabel != null && _formDetails.TryGetValue(targetLabel, out Detail formDetail))
                {
                    formDetail.Clear();
                }
                else
                {
                    MessageBox.Show("Form Detail not found !");
                    return;
                }
                _currentItems[targetLabel] = "";
                LogAPIPath = "";
                //SetLabelText(lblErrorCode, "");
            }
            catch (Exception ex)
            {
                AddLog(ex.Message + "\r\n" + ex.StackTrace, position);
            }
        }

        private void ResetError(string position)
        {
            Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
            error_code[targetLabel] = "";
            error_description[targetLabel] = "";
            customer_error_code[targetLabel] = "";
            customer_error_des[targetLabel] = "";
        }
        public void SetErrorCode(string error_des, string position)
        {
            Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
            if (!ErrorCodeList.ContainsKey(error_des))
            {
                //if (_PN == "Golden" || _PN.Contains("Customer") || _PN.Contains("Loop") || _PN.Contains("Sweep"))
                if (testmode == TestMode.Debug)
                {
                    error_code[targetLabel] = "UNKNOW";
                    error_description[targetLabel] = error_des;
                }
                else
                {
                    AddLog("Don't have error code of \"" + error_des + "\"", position);
                    error_code[targetLabel] = "UNKNOW";
                    error_description[targetLabel] = error_des;
                    //MessageBox.Show("Don't have error code of \"" + error_des + "\"");
                    //Environment.Exit(0);
                }
            }
            else
            {
                error_code[targetLabel] = ErrorCodeList[error_des];
                error_description[targetLabel] = error_des;
            }
        }
        private void BackupLocalLog(string from, string to, string position)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(to)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(to));
                }

                File.Move(from, to);
                AddLog("Backup local log done..", position);
            }
            catch (Exception ex)
            {
                AddLog("BackupLocalLog Exception : " + ex.Message + " ===> " + ex.StackTrace, position);
            }
        }
        private double CycleTime = 0;
        private string cal_2g_new = "";
        private string cal_5g_new = "";
        private string cal_6g_new = "";

        private void SummaryLog(string position)
        {
            try
            {
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                if (targetLabel == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy Label cho vị trí: {position}");
                    return;
                }

                if (!_labelItemLists.TryGetValue(targetLabel, out List<ItemInfo> ListItems) || ListItems == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy danh sách ItemInfo cho vị trí: {position}");
                    return;
                }

                CycleTime = 0;
                AddLog(".                     SUMMARY", position);
                int index = 0;
                bool fail = false;
                foreach (ItemInfo i in ListItems)
                {
                    if (string.IsNullOrEmpty(i.ItemName))
                    {
                        break;
                    }
                    index++;
                    if (!fail || i.Flag == 5)
                    {
                        string rs = i.TestResult ? "PASS" : "FAIL";
                        if (index < 10)
                        {
                            AddLog(index + ". " + i.ItemName.PadRight(45, ' ') + rs.PadRight(10, ' ') + i.test_time, position);
                        }
                        else
                        {
                            AddLog(index + ". " + i.ItemName.PadRight(44, ' ') + rs.PadRight(10, ' ') + i.test_time, position);
                        }
                    }
                    if (!i.TestResult)
                    {
                        fail = true;
                    }
                    CycleTime += Convert.ToDouble(i.test_time.Trim());
                }

                AddLog("-----------------------------------------------------------", position);
                AddLog("CYCLE TIME : " + CycleTime, position);
            }
            catch { }
        }
        bool isSETIP = false;
        private void TestFinish(string position)
        {
            try
            {
                DUT myDut = GetDUTByPosition(position);
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));

                string new_txtlog = "";

                string directory_wifi = "";
                string fileNameWithoutExtension_wifi = "";
                string newFileName_wifi = "";
                string newPath_wifi = WIFI_Log;

                string directory_ble = "";
                string fileNameWithoutExtension_ble = "";
                string newFileName_ble = "";
                string newPath_ble = BLE_Log;

                string final_result = "NONE";

                bool test_result = CheckGoNoGo(position);
                bool sfis_result = true;
                btnSFCS.Enabled = true;

                isSETIP = false;

                SummaryLog(position);
                GenerateEeroAPI(ref LogAPIPath, position);
                GenerateLogTxt(position);
                if (_station == "MBFT" || _station == "SRF")
                {
                    ZipIQLog(position);
                }
                AddLog("SFIS Mode : " + SFCSmode.ToString(), position);

                Random rd = new Random();
                int rd_result = 0;
                bool API_result = false;
                if (SFCSmode == SFCS_mode.ON)
                {
                    if (_SendAPI == 1)
                    {
                        if (UploadEeroAPI(position))
                        {
                            #region SendtoSFIS
                            if (!GoldenTest)
                            {
                                JObject send_sfis = new JObject();
                                string[] send = ui_to_sfis.Trim().Split(',');
                                foreach (string s in send)
                                {
                                    switch (s)
                                    {
                                        case "MLBSN":
                                            send_sfis.Add("MLBSN", myDut.mlbsn);
                                            break;
                                        case "SN":
                                            send_sfis.Add("SN", myDut.mlbsn);
                                            break;
                                        case "PCNAME":
                                            send_sfis.Add("PCNAME", PC_NAME);
                                            break;
                                        case "STATUS":
                                            send_sfis.Add("STATUS", test_result == true ? "PASS" : "FAIL");
                                            break;
                                        case "ERRORCODE":
                                            send_sfis.Add("ERRORCODE", error_code[targetLabel]);
                                            break;
                                    }
                                }

                                AddLog("UI => SFIS : " + send_sfis.ToString().Replace("\r\n", ""), position);
                                string response = SendToSFCS(sfis_final, send_sfis.ToString());
                                AddLog("SFIS => UI : " + response.Replace("\r\n", ""), position);
                                if (response.StartsWith("Exception : "))
                                {
                                    if (test_result)
                                    {
                                        sfis_result = false;
                                        error_code[targetLabel] = "SF01";
                                        error_description[targetLabel] = "Send SFIS Exception";
                                        ChangeStatus(Status.FAIL, position);
                                    }
                                }
                                else
                                {
                                    JObject jresponse = JObject.Parse(response);
                                    bool rs = jresponse.GetValue("result").ToString() == "PASS" ? true : false;
                                    string message = jresponse.GetValue("message").ToString();
                                    SetRtbText(rtbSFCS, message);
                                    if (!rs)
                                    {
                                        if (test_result)
                                        {
                                            sfis_result = false;
                                            error_code[targetLabel] = "SF03";
                                            error_description[targetLabel] = "Send SFIS Fail";
                                        }
                                    }
                                    else
                                    {
                                        AddLog("Send SFIS PASS !", position);
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            for (int k = 0; k < 1; k++)
                            {
                                if (UploadEeroAPI(position))
                                {
                                    #region SendtoSFIS
                                    if (!GoldenTest)
                                    {
                                        JObject send_sfis = new JObject();
                                        string[] send = ui_to_sfis.Trim().Split(',');
                                        foreach (string s in send)
                                        {
                                            switch (s)
                                            {
                                                case "MLBSN":
                                                    send_sfis.Add("MLBSN", myDut.mlbsn);
                                                    break;
                                                case "SN":
                                                    send_sfis.Add("SN", myDut.mlbsn);
                                                    break;
                                                case "PCNAME":
                                                    send_sfis.Add("PCNAME", PC_NAME);
                                                    break;
                                                case "STATUS":
                                                    send_sfis.Add("STATUS", test_result == true ? "PASS" : "FAIL");
                                                    break;
                                                case "ERRORCODE":
                                                    send_sfis.Add("ERRORCODE", error_code[targetLabel]);
                                                    break;
                                            }
                                        }

                                        AddLog("UI => SFIS : " + send_sfis.ToString().Replace("\r\n", ""), position);
                                        string response = SendToSFCS(sfis_final, send_sfis.ToString());
                                        AddLog("SFIS => UI : " + response.Replace("\r\n", ""), position);

                                        if (response.StartsWith("Exception : "))
                                        {
                                            if (test_result)
                                            {
                                                sfis_result = false;
                                                error_code[targetLabel] = "SF01";
                                                error_description[targetLabel] = "Send SFIS Exception";
                                                ChangeStatus(Status.FAIL, position);
                                            }
                                        }
                                        else
                                        {
                                            JObject jresponse = JObject.Parse(response);
                                            bool rs = jresponse.GetValue("result").ToString() == "PASS" ? true : false;
                                            string message = jresponse.GetValue("message").ToString();
                                            SetRtbText(rtbSFCS, message);
                                            if (!rs)
                                            {
                                                if (test_result)
                                                {
                                                    sfis_result = false;
                                                    error_code[targetLabel] = "SF03";
                                                    error_description[targetLabel] = "Send SFIS Fail";
                                                }
                                            }
                                            else
                                            {
                                                AddLog("Send SFIS PASS !", position);
                                            }
                                        }
                                    }
                                    #endregion
                                    API_result = true;
                                    break;
                                }
                            }
                            if (!API_result)
                            {
                                test_result = false;
                                error_code[targetLabel] = "AP01";
                                error_description[targetLabel] = "Upload EERO API Fail";
                                ChangeStatus(Status.FAIL, position);
                            }
                        }
                    }
                }
                else
                {
                    if (_PN != "Golden")
                    {
                        if (_SendAPI == 1)
                        {
                            if (customer_error_code[targetLabel] == "FIX.CLOSE" || customer_error_code[targetLabel] == "DUT.PING")
                            {
                                double randomValue = rd.NextDouble();

                                if (randomValue < 0.5)
                                {
                                    rd_result = 0; // 70% chance for 0
                                }
                                else
                                {
                                    rd_result = 1; // 30% chance for 1
                                }

                                AddLog("Send: " + rd_result.ToString(), position);
                                if (rd_result == 1)
                                {
                                    if (!UploadEeroAPI(position))
                                    {
                                        for (int k = 0; k < 1; k++)
                                        {
                                            if (UploadEeroAPI(position))
                                            {
                                                API_result = true;
                                                break;
                                            }
                                        }
                                        if (!API_result)
                                        {
                                            test_result = false;
                                            error_code[targetLabel] = "AP01";
                                            error_description[targetLabel] = "Upload EERO API Fail";
                                            ChangeStatus(Status.FAIL, position);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!UploadEeroAPI(position))
                                {
                                    for (int k = 0; k < 1; k++)
                                    {
                                        if (UploadEeroAPI(position))
                                        {
                                            API_result = true;
                                            break;
                                        }
                                    }
                                    if (!API_result)
                                    {
                                        test_result = false;
                                        error_code[targetLabel] = "AP01";
                                        error_description[targetLabel] = "Upload EERO API Fail";
                                        ChangeStatus(Status.FAIL, position);
                                    }
                                }
                            }
                        }
                    }
                }

                if (test_result && sfis_result)
                {
                    ChangeStatus(Status.PASS, position);
                    SetLabelText(targetLabel, $"{targetLabel.Text}\nPASS");
                    final_result = "PASS";
                    Main_info += "1,";
                    old_sn = "";
                    total_pass_qty++;
                    fail_count = 0;
                    fail_fixclose_count = 0;
                }
                else
                {
                    ChangeStatus(Status.FAIL, position);
                    SetLabelText(targetLabel, $"{targetLabel.Text}\n{error_code[targetLabel]}");
                    final_result = "FAIL";
                    Main_info += "0,";
                    Error_info += error_code[targetLabel] + "," + error_description[targetLabel] + "," + fail_value + "|";
                    old_sn = myDut.mlbsn;
                    total_fail_qty++;

                    fail_list += myDut.mlbsn + ", " + error_code[targetLabel] + " ," + error_description[targetLabel] + "\r\n";
                }

                string txtLog = LogFilePath + log_time.Substring(0, 10) + "\\" + LogFileName;

                string txt_server_log = "/Data/" + _Product + "/Text/" + _PN + "/" + _station + "/" + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + final_result + "/" + Path.GetFileName(txtLog);

                new_txtlog = LogFilePath + log_time.Substring(0, 10) + "\\" + final_result + "\\" + LogFileName.Replace(".txt", "") + "_" + Environment.MachineName + "_" + "SFIS_" + SFCSmode + "_" + final_result + "_" + error_code[targetLabel] + ".txt";

                string jsonLog = current_directory + @"\python\log\" + LogAPIPath + ".json";
                //AddLog(jsonLog);

                if (final_result == "FAIL")
                {
                    if (File.Exists(WIFI_Log))
                    {
                        directory_wifi = Path.GetDirectoryName(WIFI_Log);
                        fileNameWithoutExtension_wifi = Path.GetFileNameWithoutExtension(WIFI_Log);
                        newFileName_wifi = fileNameWithoutExtension_wifi + "_" + error_code[targetLabel];
                        newPath_wifi = Path.Combine(directory_wifi, newFileName_wifi + ".txt");
                        File.Move(WIFI_Log, newPath_wifi);
                    }

                    if (File.Exists(BLE_Log))
                    {
                        directory_ble = Path.GetDirectoryName(BLE_Log);
                        fileNameWithoutExtension_ble = Path.GetFileNameWithoutExtension(BLE_Log);
                        newFileName_ble = fileNameWithoutExtension_ble + "_" + error_code[targetLabel];
                        newPath_ble = Path.Combine(directory_ble, newFileName_ble + ".txt");
                        File.Move(BLE_Log, newPath_ble);
                    }
                }

                //Save log path to csv
                csv_txtlog = "/Data/" + _Product + "/Text/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + final_result + "/" + Path.GetFileName(new_txtlog);
                csv_jsonlog = "/Data/" + _Product + "/Json/" + testmode + "/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + final_result + "/" + Path.GetFileName(jsonLog);
                csv_ble = "/Data/" + _Product + "/IQ/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + final_result + "/" + Path.GetFileName(newPath_ble);
                csv_wifi = "/Data/" + _Product + "/IQ/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + final_result + "/" + Path.GetFileName(newPath_wifi);
                csv_cali = "/Data/" + _Product + "/IQ/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + final_result + "/" + Path.GetFileName(Cali_Log);
                Thread.Sleep(100);

                Main_info += csv_txtlog + "," + version + "," + Environment.MachineName + "," + CycleTime + "," + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "," + csv_wifi + "," + csv_ble + ",,,,";

                SaveToCSV(position);

                BackupLocalLog(txtLog, new_txtlog, position);

                #region Send log infor to Truyen tool
                
                try
                {
                    if (string.IsNullOrEmpty(myDut.mlbsn))
                    {
                        myDut.mlbsn = "GGB3520000000000";
                    }
                    finish_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    Data_API dt_API = new Data_API();
                    dt_API.product = _Product;
                    dt_API.mo = myDut.mo;
                    if (SFCSmode == SFCS_mode.ON)
                    {
                        dt_API.onSfis = "on";
                    }
                    else
                    {
                        dt_API.onSfis = "off";
                    }
                    dt_API.line = "LADRO";
                    dt_API.pnname = _PN;
                    dt_API.mode = Stage_mode;
                    dt_API.station = _station;
                    dt_API.pcname = PC_NAME;
                    dt_API.position = position;
                    dt_API.mlbsn = myDut.mlbsn;
                    dt_API.sn = myDut.mlbsn;
                    dt_API.ethernetmac = myDut.ethernetmac;
                    dt_API.test_software_version = version;
                    dt_API.error_code = error_code[targetLabel];
                    dt_API.error_details = error_description[targetLabel];
                    dt_API.start_time = _startTime[targetLabel];
                    dt_API.finish_time = finish_time;
                    if (test_result && sfis_result)
                    {
                        dt_API.status = "passed";
                    }
                    else
                    {
                        dt_API.status = "failed";
                    }

                    dt_API.tests = test_items_json[targetLabel].Values.ToList<API_TestItem>();

                    string json = JsonConvert.SerializeObject(dt_API, Formatting.Indented);
                    string data_api_filename = myDut.mlbsn + "_" + PC_NAME + "_" + testmode.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

                    if (!Directory.Exists(current_directory + @"\data\"))
                    {
                        Directory.CreateDirectory(current_directory + @"\data\");
                    }

                    File.WriteAllText(current_directory + @"\data\" + data_api_filename + ".json", json);
                    Thread.Sleep(300);

                    string error = "";
                    string received = "";
                    string command = "";
                    if (_station == "MBFT" || _station == "SRF")
                    {
                        command = "java -jar UploadLogAPI.jar " + ftpServerIP + " " + @"data\" + data_api_filename + ".json" + " " + new_txtlog + " " + @"python\log\litepoint.zip" + " " + Cali_Log;
                    }
                    else
                    {
                        command = "java -jar UploadLogAPI.jar " + ftpServerIP + " " + @"data\" + Path.GetFileName(data_api_filename) + ".json" + " " + new_txtlog;
                    }
                    //AddLog("Send data API : " + command);
                    dos.ExecuteCMDWithInputStream(current_directory, command, ref error, ref received);

                    if (!String.IsNullOrEmpty(error))
                    {
                        AddLog("Error : " + error, position);
                    }
                    AddLog(received, position);
                    //string[] lines = received.Trim().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                    //for (int i = lines.Length - 1; i >= 0; i--)
                    //{
                    //    if (lines[i].Trim().StartsWith("200"))
                    //    {
                    //        return true;
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    AddLog(ex.Message, position);
                    AddLog(ex.StackTrace, position);
                }
                #endregion

                AddLog("Start upload data to Mydas..", position);
                Thread mydas = new Thread(() => SendToMyDas(position));
                mydas.IsBackground = true;
                mydas.Start();

                Thread.Sleep(300);

                Thread ftp_log = new Thread(() => UploadFTPLog(testmode.ToString(), test_result, new_txtlog, jsonLog, newPath_ble, newPath_wifi, Cali_Log, Xtal_Cali_Log, RX_Cali_Log, position));
                ftp_log.IsBackground = true;
                ftp_log.Start();

                try
                {
                    File.Delete(cal_2g_new);
                    File.Delete(cal_5g_new);
                }
                catch
                {
                }

                rtbForcus(rtbSFCS);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                Environment.Exit(0);
            }
        }

        private void DeleteOldLog(string path, string position)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (string f in files)
                    {
                        File.Delete(f);
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog("Delete old log exception : " + ex.Message, position);
            }
        }

        private delegate void drtbForcus(RichTextBox rtb);
        private void rtbForcus(RichTextBox rtb)
        {
            if (this.InvokeRequired)
            {
                drtbForcus d = new drtbForcus(rtbForcus);
                this.Invoke(d, new object[] { rtb });
            }
            else
            {
                //rtb.Text = "";
                rtb.Focus();
            }
        }

        private bool UploadEeroAPI(string position)
        {
            try
            {
                string api_file_name = LogAPIPath;
                //GenerateEeroAPI(ref api_file_name);

                string error = "";
                string received = "";
                string command = py_send_api + " " + @"log\" + Path.GetFileNameWithoutExtension(api_file_name);
                AddLog("Send API : " + command, position);
                dos.ExecuteCMDWithInputStream(current_directory + @"\python", command, ref error, ref received);

                if (!String.IsNullOrEmpty(error))
                {
                    AddLog("Error : " + error, position);
                    return false;
                }

                AddLog(received, position);
                string[] lines = received.Trim().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    if (lines[i].Trim().StartsWith("200"))
                    {
                        return true;
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                AddLog(ex.Message, position);
                AddLog(ex.StackTrace, position);
            }
            return false;
        }
        DBOp db = new DBOp();
        private void SendToMyDas(string position)
        {
            if (!GoldenTest)
            {
                if (testmode == TestMode.Debug)
                {
                    AddLog("Debug test => No send Mydas.", position);
                }
                else
                {
                    AddLog("===================== MYDAS =====================", position);
                    AddLog("Mydas IP : " + mydas_ip, position);
                    AddLog("DetailInfo : [" + Detail_info + "]", position);
                    AddLog("ErrorInfo : [" + Error_info + "]", position);
                    AddLog("MainInfo : [" + Main_info + "]", position);
                    AddLog("Send Mydas data : " + _Product + "," + _station + "," + _PN + ",3.0," + Detail_info + "," + Error_info + "," + Main_info, position);

                    try
                    {
                        PTSC.ConnectNetPTS conn = new ConnectNetPTS(Environment.MachineName, "2.0", "3.0", _Product, _station, _PN);

                        bool send = conn.SendToMyDas(mydas_ip, Detail_info, Error_info, Main_info);
                        if (send)
                        {
                            AddLog("Insert Mydas OK.", position);
                        }
                        else
                        {
                            AddLog("Insert Mydas Fail.", position);
                        }
                    }
                    catch (Exception ex)
                    {
                        AddLog("Send Mydas have exception : " + ex.Message, position);
                    }
                }
            }
            else
            {
                AddLog("Golden test => No send Mydas", position);
            }
        }

        private void UploadFTPLog(string testmode, bool test_rs, string txtLog, string jsonLog, string BLELog = "", string WIFILog = "", string CaliLog = "", string Xtal_CaliLog = "", string RX_CaliLog = "", string position = "")
        {
            try
            {
                string error = "";
                string test_result = test_rs ? "PASS" : "FAIL";
                ftpLog.ConnectServer();
                #region txt log
                try
                {
                    string txt_server = "/Data/" + _Product + "/Text/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + test_result + "/" + Path.GetFileName(txtLog);

                    ftpLog.UploadToFTP(txtLog, txt_server, false, ref error);
                    AddLog("Upload txt log done..", position);
                }
                catch (Exception txtEx)
                {
                    AddLog("Upload txt log exception : " + txtEx.Message, position);
                }

                #endregion

                #region json
                try
                {
                    string json_server = "/Data/" + _Product + "/Json/" + testmode + "/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + test_result + "/" + Path.GetFileName(jsonLog);
                    if (File.Exists(jsonLog))
                    {
                        ftpLog.UploadToFTP(jsonLog, json_server, false, ref error);
                        AddLog("Upload json log done..", position);
                    }
                }
                catch (Exception jsonEx)
                {
                    AddLog("Upload json log exception : " + jsonEx.Message, position);
                }

                #endregion

                #region IQ
                if (_station == "MBFT" || _station == "SRF")
                {
                    #region upload BLE log
                    try
                    {
                        string IQ_server = "/Data/" + _Product + "/IQ/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + test_result + "/" + Path.GetFileName(BLELog);
                        if (File.Exists(BLELog))
                        {
                            ftpLog.UploadToFTP(BLELog, IQ_server, false, ref error);
                            AddLog("Upload BLE log done..", position);
                        }
                    }
                    catch (Exception IQEx)
                    {
                        AddLog("Upload BLE log exception : " + IQEx.Message, position);
                    }
                    #endregion

                    #region upload Wifi Log
                    try
                    {
                        string IQ_server = "/Data/" + _Product + "/IQ/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + test_result + "/" + Path.GetFileName(WIFILog);
                        if (File.Exists(WIFILog))
                        {
                            ftpLog.UploadToFTP(WIFILog, IQ_server, false, ref error);
                            AddLog("Upload WIFI log done..", position);
                        }
                    }
                    catch (Exception IQEx)
                    {
                        AddLog("Upload WIFI log exception : " + IQEx.Message, position);
                    }
                    #endregion

                    #region upload Cali Log
                    try
                    {
                        string IQ_server = "/Data/" + _Product + "/IQ/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + test_result + "/" + Path.GetFileName(CaliLog);
                        if (File.Exists(CaliLog))
                        {
                            ftpLog.UploadToFTP(CaliLog, IQ_server, false, ref error);
                            AddLog("Upload TX Cali file done..", position);
                        }
                    }
                    catch (Exception IQEx)
                    {
                        AddLog("Upload Cali log exception : " + IQEx.Message, position);
                    }
                    #endregion
                    #region upload Xtal Cali Log
                    try
                    {
                        string IQ_server = "/Data/" + _Product + "/IQ/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + test_result + "/" + Path.GetFileName(Xtal_CaliLog);
                        if (File.Exists(Xtal_CaliLog))
                        {
                            ftpLog.UploadToFTP(Xtal_CaliLog, IQ_server, false, ref error);
                            AddLog("Upload Xtal Cali file done..", position);
                        }
                    }
                    catch (Exception IQEx)
                    {
                        AddLog("Upload Cali log exception : " + IQEx.Message, position);
                    }
                    #endregion
                    #region upload RX Cali Log
                    try
                    {
                        string IQ_server = "/Data/" + _Product + "/IQ/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + PC_NAME + "/" + test_result + "/" + Path.GetFileName(RX_CaliLog);
                        if (File.Exists(RX_CaliLog))
                        {
                            ftpLog.UploadToFTP(RX_CaliLog, IQ_server, false, ref error);
                            AddLog("Upload RX Cali file done..", position);
                        }
                    }
                    catch (Exception IQEx)
                    {
                        AddLog("Upload RX Cali log exception : " + IQEx.Message, position);
                    }
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                AddLog("Upload FTP Exception : " + ex.Message, position);
            }
            finally
            {
                ftpLog.Close();
            }
        }


        public void AddToEeroAPI(string api_name, bool status, string value, int id, string position = "", bool time = false)
        {
            Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
            if (targetLabel == null)
            {
                MessageBox.Show($"⚠ Không tìm thấy Label cho vị trí: {position}");
                return;
            }

            if (!_labelItemLists.TryGetValue(targetLabel, out List<ItemInfo> ListItems) || ListItems == null)
            {
                MessageBox.Show($"⚠ Không tìm thấy danh sách ItemInfo cho vị trí: {position}");
                return;
            }

            string api_limit = api_name.Replace("_0", "").Replace("_1", "").Replace("_2", "").Replace("_3", "").Replace("_4", "").Replace("_5", "").Replace("_6", "").Replace("_7", "").Replace("_8", "").Trim();

            if (limits.limits.ContainsKey(api_limit) && customer_error_code[targetLabel] == "")
            {
                DateTime endTime = DateTime.Now;
                
                ItemLimits il = limits.limits[api_limit];

                if (il.limit_type == "LIMIT")
                {
                    if (value == "FAIL")
                    {
                        value = "-1";
                    }
                }

                API_TestItem i = new API_TestItem();
                i.upper_limit = il.upper_limit;
                i.lower_limit = il.lower_limit;
                i.status = status == true ? "passed" : "failed";
                i.finish_time = endTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                i.test_name = api_name;

                #region Add ErrorCode
                if (!status)
                {
                    if (error_code[targetLabel] == "")
                    {
                        string error_des = i.test_name;
                        if (il.limit_type == "LIMIT")
                        {
                            double dvalue, ll, ul;
                            Double.TryParse(value, out dvalue);
                            Double.TryParse(il.lower_limit, out ll);
                            Double.TryParse(il.upper_limit, out ul);

                            if (dvalue == -1)
                            {
                                error_des = i.test_name;
                            }
                            else if (dvalue < ll)
                            {
                                error_des = error_des + "_tooLow";
                            }
                            else if (dvalue > ul)
                            {
                                error_des = error_des + "_tooHigh";
                            }
                        }

                        if (!ErrorCodeList.ContainsKey(error_des))
                        {
                            //if (_PN == "Golden" || _PN.Contains("Customer") || _PN.Contains("Loop") || _PN.Contains("Sweep"))
                            if (testmode == TestMode.Debug)
                            {  
                                error_code[targetLabel] = "UNKNOW";
                                error_description[targetLabel] = error_des;
                            }
                            else
                            {
                                AddLog("Don't have error code of \"" + error_des + "\"", position);
                                error_code[targetLabel] = "UNKNOW";
                                error_description[targetLabel] = error_des;
                                //MessageBox.Show("Don't have error code of \"" + error_des + "\"");
                                //Environment.Exit(0);
                            }
                        }
                        else
                        {
                            error_code[targetLabel] = ErrorCodeList[error_des];
                            error_description[targetLabel] = error_des;
                        }

                        customer_error_code[targetLabel] = error_code[targetLabel];
                        customer_error_des[targetLabel] = error_description[targetLabel];
                    }
                    else if (customer_error_code[targetLabel] == "")
                    {
                        customer_error_code[targetLabel] = error_code[targetLabel];
                        customer_error_des[targetLabel] = error_description[targetLabel];
                    }
                }
                #endregion

                i.error_code = status == true ? "" : customer_error_code[targetLabel];

                if (time)
                {
                    i.start_time = ListItems[id].StartTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    i.start_time = endTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                }

                i.test_value = value;
                i.units = il.units;
                if (test_items_json[targetLabel].ContainsKey(api_name))
                {
                    test_items_json[targetLabel][api_name] = i;
                }
                else
                {
                    test_items_json[targetLabel].Add(api_name, i);
                }

                AddToCSV(api_name, value, position);
            }
        }

        private void ResetCSV(string position)
        {
            try
            {
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                if (targetLabel == null) return;

                if (!CSV.ContainsKey(targetLabel))
                    CSV[targetLabel] = new Dictionary<string, string>();

                var dict = CSV[targetLabel];
                foreach (string k in dict.Keys.ToList())
                {
                    dict[k] = "";
                }
            }
            catch (Exception ex)
            {
                AddLog("Reset CSV exception : " + ex.Message, position);
            }
        }

        private void AddToCSV(string key, string value, string position)
        {
            try
            {
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                if (targetLabel == null) return;

                if (!CSV.ContainsKey(targetLabel))
                    CSV[targetLabel] = new Dictionary<string, string>();

                var dict = CSV[targetLabel];
                dict[key] = value;
            }
            catch (Exception ex)
            {
                AddLog("AddToCSV Exception : " + ex.Message, position);
            }
        }

        private void SaveToCSV(string position)
        {
            try
            {
                DUT myDut = GetDUTByPosition(position);
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                string serverFile = "/Data/" + _Product + "/Text/" + _PN + "/" + _station + "/"
                        + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "/" + _station + "_" + PC_NAME + "_Ver" + csv_ver + "_" + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + ".csv";
                string test_result = CheckGoNoGo(position) ? "PASS" : "FAIL";
                string localFile = current_directory + "\\test_result.csv";
                string error = "";
                string title = "SN,MO,SFCS MODE,PC_NAME,Position,Start Time, Finish Time,Cycle Time, Test Result, Error Code, Error Description, Txt_Path, Json_Path, BLE_Path, Wifi_Path, Cali_Path";
                string content = myDut.mlbsn + "," + myDut.mo + "," + SFCSmode + "," + PC_NAME + "," + position + "," + _startTime[targetLabel] + "," + finish_time + "," + _testTimes[targetLabel] + "," + test_result + "," + error_code[targetLabel] + "," + error_description[targetLabel] + "," + csv_txtlog + "," + csv_jsonlog + "," + csv_ble + "," + csv_wifi + "," + csv_cali;
                ftpLog.ConnectServer();

                if (CSV.TryGetValue(targetLabel, out var csvDict))
                {
                    foreach (string k in csvDict.Keys)
                    {
                        title += "," + k;
                        content += "," + csvDict[k];
                    }
                }

                if (!ftpLog.FileExits(serverFile, ref error))
                {
                    if (error != "")
                    {
                        AddLog("Check File Exits Exception : " + error, position);
                        return;
                    }

                    WriteToFile(localFile, title + "\r\n" + content, false);
                }
                else
                {
                    WriteToFile(localFile, content, false);
                }

                ftpLog.UploadToFTP(localFile, serverFile, true, ref error);
                if (error != "")
                {
                    AddLog("Upload CSV FAIL : " + error, position);
                }

            }
            catch (Exception ex)
            {
                AddLog("SaveToCSV Exception : " + ex.Message, position);
            }
            finally
            {
                ftpLog.Close();
            }
        }
        string generate_api_time = "";
        private void GenerateEeroAPI(ref string api_file_name, string position)
        {
            try
            {
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                DUT myDut = GetDUTByPosition(position);
                generate_api_time = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
                if (string.IsNullOrEmpty(myDut.mlbsn))
                {
                    myDut.mlbsn = "GGB3520000000000";
                }
                finish_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                EERO_API eERO_API = new EERO_API();
                eERO_API.mode = testmode.ToString().ToLower();
                eERO_API.station_name = PC_NAME;
                eERO_API.error_code = customer_error_code[targetLabel];
                eERO_API.error_details = customer_error_des[targetLabel];
                eERO_API.position = position;
                eERO_API.serial = myDut.mlbsn;
                eERO_API.station_type = _station;
                eERO_API.test_software_version = version;
                eERO_API.finish_time = finish_time;
                eERO_API.start_time = _startTime[targetLabel];
                eERO_API.status = CheckGoNoGo(position) == true ? "passed" : "failed";
                eERO_API.tests = test_items_json[targetLabel].Values.ToList<API_TestItem>();

                string json = JsonConvert.SerializeObject(eERO_API, Formatting.Indented);
                api_file_name = myDut.mlbsn + "_" + PC_NAME + "_" + testmode.ToString() + "_" + generate_api_time;

                if (!Directory.Exists(current_directory + @"\python\log\"))
                {
                    Directory.CreateDirectory(current_directory + @"\python\log\");
                }

                File.WriteAllText(current_directory + @"\python\log\" + api_file_name + ".json", json);

            }
            catch (Exception ex)
            {
                AddLog(ex.Message, position);
                AddLog(ex.StackTrace, position);
            }
        }

        private void GenerateLogTxt(string position)
        {
            try
            {
                DUT myDut = GetDUTByPosition(position);
                if (string.IsNullOrEmpty(myDut.mlbsn))
                {
                    myDut.mlbsn = "GGB3520000000000";
                }
                string file_name = myDut.mlbsn + "_" + PC_NAME + "_" + testmode.ToString() + "_" + generate_api_time;
                string filePath = current_directory + @"\python\log\" + file_name + "_serial.txt";
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));

                if (targetLabel != null && _formDetails.TryGetValue(targetLabel, out Detail formDetail))
                {
                    File.WriteAllText(current_directory + @"\python\log\" + file_name + "_serial.txt", formDetail.GetAllLog());
                }
                else
                {
                    MessageBox.Show("Form Detail not found !");
                    return;
                }
                
                if (File.Exists(filePath.Replace(".txt", ".zip")))
                {
                    File.Delete(filePath.Replace(".txt", ".zip"));
                }
                using (ZipArchive zip = ZipFile.Open(filePath.Replace(".txt", ".zip"), ZipArchiveMode.Create))
                {
                    zip.CreateEntryFromFile(filePath, file_name + "_serial.txt");
                }
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, position);
                AddLog(ex.StackTrace, position);
            }
        }

        private void ZipIQLog(string position)
        {
            try
            {
                if (File.Exists(current_directory + @"\python\log\litepoint.zip"))
                {
                    File.Delete(current_directory + @"\python\log\litepoint.zip");
                }

                using (ZipArchive zip = ZipFile.Open(current_directory + @"\python\log\litepoint.zip", ZipArchiveMode.Create))
                {
                    if (File.Exists(BLE_Log))
                    {
                        zip.CreateEntryFromFile(BLE_Log, Path.GetFileName(BLE_Log));
                    }
                    if (File.Exists(WIFI_Log))
                    {
                        zip.CreateEntryFromFile(WIFI_Log, Path.GetFileName(WIFI_Log));
                    }

                }
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, position);
                AddLog(ex.StackTrace, position);
            }
        }

        private void SetRtbText(RichTextBox rtb, string text)
        {
            if (rtb.InvokeRequired)
            {
                dSetRtbText d = new dSetRtbText(SetRtbText);
                rtb.Invoke(d, new object[] { rtb, text });
            }
            else
            {
                rtb.Text = text;
            }
        }

        private bool TestFunction(int i, int retry_time, string position)
        {
            try
            {
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                if (targetLabel == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy Label cho vị trí: {position}");
                    return false;
                }

                if (!_labelItemLists.TryGetValue(targetLabel, out List<ItemInfo> ListItems) || ListItems == null)
                {
                    MessageBox.Show($"⚠ Không tìm thấy danh sách ItemInfo cho vị trí: {position}");
                    return false;
                }

                bool test_result = false;

                if (targetLabel != null && _testFunctions.TryGetValue(targetLabel, out TestFunction tf))
                {
                    switch (ListItems[i].ItemDes)
                    {
                        case "GenerateNodeFromBarcode":
                            test_result = tf.GenerateNodeFromBarcode(i);
                            break;
                        case "GetMacFromShopFloor":
                            test_result = tf.GetMacFromShopFloor(i);
                            break;
                        case "GetMOFromShopFloor":
                            test_result = tf.GetMOFromShopFloor(i);
                            break;
                        case "TrueItem":
                            test_result = tf.TrueItem(i);
                            break;
                        case "TPG_CheckSum":
                            test_result = tf.TPG_CheckSum(i);
                            break;
                        case "ScanWIFI":
                            test_result = tf.ScanWIFI(i);
                            break;
                        default:
                            MessageBox.Show("Don't have item " + ListItems[i].ItemDes);
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Function not found !");
                    return false;
                }

                return test_result;
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, position);
                AddLog(ex.StackTrace, position);
                return false;
            }
            finally
            {

            }
        }

        private bool CheckGoNoGo(string position)
        {
            try
            {
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));
                if (targetLabel == null)
                {
                    MessageBox.Show($"CheckGoNoGo ⚠ Không tìm thấy Label cho vị trí: {position}");
                    return false;
                }

                if (!_labelItemLists.TryGetValue(targetLabel, out List<ItemInfo> ListItems) || ListItems == null)
                {
                    MessageBox.Show($"CheckGoNoGo ⚠ Không tìm thấy danh sách ItemInfo cho vị trí: {position}");
                    return false;
                }

                foreach (ItemInfo ii in ListItems)
                {
                    if (!ii.TestResult)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, position);
                return false;
            }
            return true;
        }

        public void AddLog(string log, string position)
        {
            AddLog(log, LogType.LOG, position);
        }
        public void AddLog(string log, LogType type, string position)
        {
            try
            {
                DUT myDut = GetDUTByPosition(position);
                WriteLogEvent.WaitOne();
                string[] l = log.Trim().Split('\n');
                string start = "";
                string message = "";

                switch (type)
                {
                    case LogType.LOG:
                        start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + " : ";
                        break;
                    case LogType.DUT:
                        start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + " : [Dut] ";
                        break;
                    case LogType.FIXTURE:
                        start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + " : [Fixture] ";
                        break;
                    case LogType.PC:
                        start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + " : [PC] ";
                        break;
                    case LogType.GOLDEN:
                        start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + " : [Golden] ";
                        break;
                }

                // 🔹 Tìm Label theo position dựa vào Name
                Label targetLabel = _formDetails.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));

                if (targetLabel != null && _formDetails.TryGetValue(targetLabel, out Detail formDetail))
                {
                    foreach (string s in l)
                    {
                        message = start + s.Trim() + "\r\n";

                        // 🔹 Gửi log vào đúng FormDetail theo vị trí
                        formDetail.Addlog(message);

                        // 🔹 Xây dựng tên file log
                        if (!string.IsNullOrEmpty(myDut.mlbsn))
                        {
                            if (!string.IsNullOrEmpty(myDut.sn))
                            {
                                LogFileName = myDut.mlbsn + "_" + myDut.sn + "_" + log_time + ".txt";
                            }
                            else
                            {
                                LogFileName = myDut.mlbsn + "_" + log_time + ".txt";
                            }
                        }
                        else
                        {
                            LogFileName = "NONE_" + log_time + ".txt";
                        }

                        // 🔹 Ghi log vào file
                        WriteToFile(LogFilePath + log_time.Substring(0, 10) + "\\" + LogFileName, message, true);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy formdetail theo vị trí để addlog.");
                    message = start + $"[Position {position}] " + log + "\r\n";
                    WriteToFile(LogFilePath + log_time.Substring(0, 10) + "\\GeneralLog.txt", message, true);
                }

                WriteLogEvent.Set();
            }
            catch
            {
            }
        }


        private bool firstchar = true;
        private string testPosition = "";
        private string currentSN = "";
        private void rtbSFCS_KeyDown(object sender, KeyEventArgs e)
        {
            if (firstchar)
            {
                firstchar = false;
                rtbSFCS.Text = "";
            }

            if (e.KeyCode == Keys.Enter)
            {
                firstchar = true;
                string input = rtbSFCS.Text.Trim().ToUpper();

                // 🔹 Nếu chưa có SN, lưu lại SN và yêu cầu nhập vị trí
                if (string.IsNullOrEmpty(currentSN))
                {
                    currentSN = input;
                    SetRtbText(rtbSFCS, $"SN: {currentSN}\r\nVui lòng nhập vị trí test (A1, A2, ..., B3)\r\n");
                    return;
                }
                // 🔹 Nếu đã có SN, kiểm tra input có phải là vị trí hợp lệ không
                if (string.IsNullOrEmpty(testPosition))
                {
                    if (Regex.IsMatch(input, @"^[A-B][1-3]$")) // Kiểm tra vị trí hợp lệ
                    {
                        testPosition = input;
                        Label targetLabel = _testTimes.Keys.FirstOrDefault(lbl => lbl.Name.Contains(testPosition));
                        if (_Labelstatus[targetLabel] == Status.TESTING)
                        {
                            SetRtbText(rtbSFCS, $"{testPosition} đang test, hãy test ở vị trí khác !");
                            currentSN = "";
                            testPosition = "";
                            return;
                        }
                        SetRtbText(rtbSFCS, $"SN: {currentSN} - vị trí {testPosition}");

                        // 🔹 Gọi CheckInput với cả SN và vị trí test
                        CheckInput($"{currentSN},{testPosition}");

                        // 🔹 Reset lại để nhập SN mới
                        currentSN = "";
                        testPosition = "";
                        return;
                    }
                    else
                    {
                        SetRtbText(rtbSFCS, "⚠ Vui lòng nhập vị trí hợp lệ (A1, A2, ...B3)");
                        return;
                    }
                }
            }
        }

        string old_sn = "";
        private void CheckInput(string input)
        {
            GoldenTest = false;
            //string sn = input;
            string[] parts = input.Split(',');
            if (parts.Length < 2)
            {
                SetRtbText(rtbSFCS, "⚠ Lỗi: Dữ liệu không hợp lệ!");
                return;
            }

            string sn = parts[0];
            string position = parts[1];
            try
            {
                if (SFCSmode == SFCS_mode.ON)
                {
                    ChangeTestMode(TestMode.Production);
                    CheckAmbitVersion();

                    JObject sendtoSFIS = new JObject();

                    sendtoSFIS.Add("SN", sn.ToUpper());
                    sendtoSFIS.Add("PCNAME", PC_NAME);
                    AddLog("UI=>SFIS : " + sendtoSFIS.ToString().Replace("\r\n", ""), position);
                    string response = SendToSFCS(sfis_connect, sendtoSFIS.ToString());
                    AddLog("SFIS=>UI : " + response, position);

                    //string response = "\"result\":\"PASS\",\"message\":\"OK\",\"data\":{\"sn\":\"RZX0JVY\",\"ethernetmac\":\"08F01E00DC40\",\"pnname\":\"810-01700\",\"mlbsn\":\"GGB350034197001F\",\"typemodel\":\"simple\",\"mo\":\"2144011092\",\"smode\":\"Production\",\"ip\":\"172.10.4.131\",\"revert\":\"N\"}";

                    OnSFISAction(response, position);
                }
                else
                {
                    JObject sendtoSFIS = new JObject();

                    sendtoSFIS.Add("SN", sn.ToUpper());
                    sendtoSFIS.Add("PCNAME", PC_NAME);
                    sendtoSFIS.Add("MODE", "debug");
                    AddLog("UI=>SFIS : " + sendtoSFIS.ToString().Replace("\r\n", ""), position);
                    string response = SendToSFCS(sfis_connect, sendtoSFIS.ToString());
                    AddLog("SFIS=>UI : " + response, position);

                    //string response = "\"result\":\"PASS\",\"message\":\"OK\",\"data\":{\"sn\":\"RZX0JVY\",\"ethernetmac\":\"08F01E00DC40\",\"pnname\":\"810-01700\",\"mlbsn\":\"GGB350034197001F\",\"typemodel\":\"simple\",\"mo\":\"2144011092\",\"smode\":\"Production\",\"ip\":\"172.10.4.131\",\"revert\":\"N\"}";

                    OnSFISAction(response, position);
                }
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, position);
                AddLog(ex.StackTrace, position);
            }
        }
        private Dictionary<string, Thread> testThreads = new Dictionary<string, Thread>(); // Quản lý thread theo vị trí

        private void OnSFISAction(string sfis_data, string position)
        {
            try
            {
                SetRtbText(rtbSFCS, $"{sfis_data}\r\nPosition: {position}");

                JObject sfis_response = JObject.Parse(sfis_data);
                string message = sfis_response.GetValue("message").ToString();

                if (sfis_response.GetValue("result").ToString() == "PASS")
                {
                    JObject data = JObject.Parse(sfis_response.GetValue("data").ToString());

                    Label targetLabel = _dutInstances.Keys.FirstOrDefault(lbl => lbl.Name.Contains(position));

                    if (targetLabel != null)
                    {
                        Label foundLabel = GetLabelBySN(data["mlbsn"].ToString());
                        if (foundLabel != null)
                        {
                            SetRtbText(rtbSFCS, $"⚠ {data["mlbsn"].ToString()} đang test tại {foundLabel.Name.Replace("lbl","")} !");
                            return;
                        }
                        DUT myDut = new DUT(); // 🔄 Tạo một đối tượng DUT mới

                        if (data.ContainsKey("sn")) myDut.sn = data["sn"].ToString();
                        if (data.ContainsKey("type")) myDut.type = data["type"].ToString();
                        if (data.ContainsKey("mo")) myDut.mo = data["mo"].ToString();
                        if (data.ContainsKey("mlbsn")) myDut.mlbsn = data["mlbsn"].ToString();
                        if (data.ContainsKey("ip")) myDut.IP = data["ip"].ToString();
                        if (data.ContainsKey("ethernetmac")) myDut.ethernetmac = data["ethernetmac"].ToString();

                        _dutInstances[targetLabel] = myDut;
                    }
                    else
                    {
                        MessageBox.Show($"⚠ Không tìm thấy DUT cho vị trí: {position}");
                    }


                    // 🔹 Kiểm tra nếu thread tại vị trí này đã tồn tại và đang chạy
                    if (testThreads.ContainsKey(position) && testThreads[position].IsAlive)
                    {
                        SetRtbText(rtbSFCS, $"⚠ {position} đang test. Hãy test tại vị trí khác !");
                        return;
                    }

                    // 🔹 Khởi tạo thread mới cho vị trí test
                    Thread newTestThread = new Thread(() => StartTest(position))
                    {
                        IsBackground = true,
                        Name = $"TestThread_{position}" // Đặt tên thread theo vị trí
                    };

                    // 🔹 Lưu thread vào Dictionary và khởi động
                    testThreads[position] = newTestThread;
                    newTestThread.Start();

                    _testingSN[targetLabel] = data["mlbsn"].ToString();
                    // 🔹 Reset vị trí test để yêu cầu nhập lại cho lần test tiếp theo
                    testPosition = "";
                    SetRtbText(rtbSFCS, $"{position} bắt đầu test! Hãy nhập SN mới.");
                }
                else
                {
                    SetRtbText(rtbSFCS, message);
                    rtbForcus(rtbSFCS);
                }
            }
            catch (Exception ex)
            {
                AddLog(ex.Message + "\r\n" + ex.StackTrace, position);
            }
        }

        private void SetLabelText(Label lb, string text)
        {
            if (lb.InvokeRequired)
            {
                dSetLabelText d = new dSetLabelText(SetLabelText);
                lb.Invoke(d, new object[] { lb, text });
            }
            else
            {
                lb.Text = text;
            }
        }
        private void CheckAmbitVersion()
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void rtbSFCS_Click(object sender, EventArgs e)
        {
            rtbSFCS.Text = "";
        }

        public void OpenLog(string type)
        {
            try
            {
                string log_path = "";
                switch (type)
                {
                    case "txt":
                        log_path = LogFilePath + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                        break;
                    case "ble":
                        log_path = IQ_BLE_Path + "\\log\\";
                        break;
                    case "wifi":
                        log_path = IQ_Wifi_Path + "\\log\\";
                        break;
                }
                Process.Start("explorer.exe", log_path.Replace("\\\\", "\\"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Bạn có chắc chắn muốn đóng chương trình không?",
            "Xác nhận thoát",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true; // Hủy việc đóng chương trình
            }
        }

        private void btnTestMode_Click(object sender, EventArgs e)
        {

        }

        private void failedListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fail_list))
            {
                MessageBox.Show("No Fail till now !");
            }
            else
            {
                MessageBox.Show(fail_list);
            }
        }

        private bool isCableFormOpened = false;
        private Cable cb;
        private void cableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isCableFormOpened)
            {
                cb = new Cable();
                cb.FormClosed += (s, args) => isCableFormOpened = false;
                cb.Show();
                cb.Focus();
                //cb.TopMost = true;
                isCableFormOpened = true;
            }
        }

        private void networkConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "shell:::{7007ACC7-3202-11D1-AAD2-00805FC1270E}");
        }
    }
}
