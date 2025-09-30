using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Sona;
using System.Management;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using MySqlX.XDevAPI.Relational;
using static System.Windows.Forms.LinkLabel;
using System.Net.NetworkInformation;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Application = System.Windows.Forms.Application;
using static OpenCvSharp.Stitcher;
using System.Windows.Input;
using System.Net.Http;
using ManagedNativeWifi;

namespace ATSTemplate
{
    public class TestFunction
    {
        private Form1 ats;
        private DOS dos = new DOS();
        private Telnet telnet = new Telnet();
        private Telnet telnet_gd = new Telnet();
        private MSerialPort fix = new MSerialPort();
        private MSerialPort dut = new MSerialPort();
        private Common common;
        private string _position;

        private const UInt32 WM_CLOSE = 0x0010;

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public TestFunction(Form1 _ats, string position)
        {
            ats = _ats;
            this._position = position;
            common = new Common(_ats);
            telnet.DataReceived += Telnet_DataReceived;
            fix.DataReceived += Fix_DataReceived;
            dut.DataReceived += Dut_DataReceived;
            telnet_gd.DataReceived += Telnet_gd_DataReceived;
        }

        private void Telnet_gd_DataReceived(object sender, MyEventArgs e)
        {
            AddLog(e.Received, LogType.GOLDEN);
        }

        private void Dut_DataReceived(object sender, MyEventArgs e)
        {
            AddLog(e.Received, LogType.DUT);
        }

        private void Fix_DataReceived(object sender, MyEventArgs e)
        {
            AddLog(e.Received, LogType.FIXTURE);
        }

        private void Telnet_DataReceived(object sender, MyEventArgs e)
        {
            AddLog(e.Received, LogType.DUT);
        }

        private string ChangeMACformat(string mac)
        {
            string regex = "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})";
            string replace = "$1:$2:$3:$4:$5:$6";
            string sNewMac = Regex.Replace(mac, regex, replace);

            return sNewMac;
        }
        public bool GetMacFromShopFloor(int i)
        {
            try
            {
                List<ItemInfo> ListItems = ats.GetItemListByPosition(_position);
                DUT myDut = ats.GetDUTByPosition(_position);
                ListItems[i].TestValue = "";
                if (!string.IsNullOrEmpty(myDut.ethernetmac))
                {
                    ListItems[i].TestValue = ChangeMACformat(myDut.ethernetmac).ToLower();
                }
                AddLog("get MAC = " + ListItems[i].TestValue);
                if (ats.testmode == TestMode.Production)
                {
                    if (!ListItems[i].TestValue.EndsWith("0"))
                    {
                        MessageBox.Show("SFIS MAC không đúng quy cách, gọi TE-PRO !");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
                AddLog(ex.StackTrace);
            }
            finally
            {

            }
            return false;
        }
        public bool GetMOFromShopFloor(int i)
        {
            try
            {
                List<ItemInfo> ListItems = ats.GetItemListByPosition(_position);
                DUT myDut = ats.GetDUTByPosition(_position);
                ListItems[i].TestValue = "";
                if (myDut.mo != null)
                {
                    ListItems[i].TestValue = myDut.mo;
                }
                AddLog("get MO = " + ListItems[i].TestValue);
                return true;
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
                AddLog(ex.StackTrace);
            }
            finally
            {

            }
            return false;
        }
        public bool GenerateNodeFromBarcode(int i)
        {
            try
            {
                List<ItemInfo> ListItems = ats.GetItemListByPosition(_position);
                ListItems[i].TestValue = "FAIL";
                AddLog(ats.sfis_to_ui);
                AddLog(ats.ui_to_sfis);
                ListItems[i].TestValue = "PASS";
                return true;
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
                AddLog(ex.StackTrace);
            }
            finally
            {

            }

            return false;
        }
        
        private void AddLog(string log, LogType type)
        {
            ats.AddLog(log, type, _position);
        }

        private void AddLog(string log)
        {
            ats.AddLog(log, _position);
        }
        
        public bool TrueItem(int i)
        {
            List<ItemInfo> ListItems = ats.GetItemListByPosition(_position);
            ListItems[i].TestValue = "PASS";
            return true;
        }

        public bool TPG_CheckSum(int i)
        {
            List<ItemInfo> ListItems = ats.GetItemListByPosition(_position);
            ListItems[i].TestValue = ListItems[i].Command;
            AddLog("TPG Checksum: " + ListItems[i].Command);
            return true;
        }

        static ulong MacToUInt64(string macAddress)
        {
            // Loại bỏ dấu hai chấm và chuyển đổi thành số nguyên
            return Convert.ToUInt64(macAddress.Replace(":", ""), 16);
        }

        static string UInt64ToMac(ulong macAsNumber)
        {
            // Chuyển đổi số nguyên thành chuỗi thập lục phân
            string macHex = macAsNumber.ToString("X12");

            // Thêm dấu hai chấm giữa các cặp ký tự
            return string.Join(":", macHex.Substring(0, 2),
                                     macHex.Substring(2, 2),
                                     macHex.Substring(4, 2),
                                     macHex.Substring(6, 2),
                                     macHex.Substring(8, 2),
                                     macHex.Substring(10, 2));
        }
        
        public bool ScanWIFI(int i)
        {
            try
            {
                List<ItemInfo> ListItems = ats.GetItemListByPosition(_position);

                string prompt = ListItems[i].Spec;
                ListItems[i].TestValue = "FAIL";

                string[] api_name = ListItems[i].API_NAME.Trim().Split(',');

                DUT myDut = ats.GetDUTByPosition(_position);

                string targetMacAddress = ChangeMACformat(myDut.ethernetmac).ToLower();
                string targetSSID = "Kuiper-setup";
                int incrementValue = 21;

                // Convert MAC sang số
                ulong macAsNumber = MacToUInt64(targetMacAddress);
                macAsNumber += (ulong)incrementValue;
                string newMacAddress = UInt64ToMac(macAsNumber).ToLower();

                AddLog($"SN: {myDut.mlbsn}");
                AddLog($"Target SSID: {targetSSID}");
                AddLog($"MAC: {targetMacAddress}");
                AddLog($"Target MAC address: {newMacAddress}");

                ListItems[i].TestValue = "FAIL";

                int k = 30;
                while (k > 0)
                {
                    k--;
                    AddLog("******************************** Scanning WIFI ********************************");
                    // Yêu cầu adapter quét lại
                    NativeWifi.ScanNetworksAsync(TimeSpan.FromSeconds(5));
                    Thread.Sleep(1500); // đợi scan

                    // Lấy danh sách BSS (AP nhìn thấy)
                    var bssNetworks = NativeWifi.EnumerateBssNetworks();
                    
                    foreach (var bss in bssNetworks)
                    {
                        string ssid = bss.Ssid.ToString();
                        string macAddress = bss.Bssid.ToString().ToLower();

                        AddLog($"SSID: {ssid}, BSSID: {macAddress}", LogType.PC);

                        if (macAddress.Equals(newMacAddress, StringComparison.OrdinalIgnoreCase))
                        {
                            AddLog($"Found MAC {newMacAddress} belonging to SSID: {ssid}");

                            if (!targetSSID.Equals(ssid))
                            {
                                AddLog("SSID incorrect");
                                ats.AddToEeroAPI(api_name[1], true, newMacAddress, i, _position, true);
                                ats.AddToEeroAPI(api_name[0], false, ssid, i, _position, true);
                                return false;
                            }

                            ats.AddToEeroAPI(api_name[0], true, ssid, i, _position, true);
                            ats.AddToEeroAPI(api_name[1], true, newMacAddress, i, _position, true);
                            ListItems[i].TestValue = "PASS";
                            return true;
                        }
                    }

                    Thread.Sleep(5000);
                }

                AddLog($"MAC {newMacAddress} not found.");
                ats.AddToEeroAPI(api_name[1], false, "-1", i, _position, true);
                ListItems[i].TestValue = "PASS";
                return false;
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
                AddLog(ex.StackTrace);
            }

            return false;
        }
    }
}
