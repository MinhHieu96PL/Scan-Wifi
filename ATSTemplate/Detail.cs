using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATSTemplate
{
    public partial class Detail : Form
    {
        private Form1 ats;
        //private List<ItemInfo> items;
        private string _position;
        public Detail(Form1 ats, string position)
        {
            InitializeComponent();
            this.ats = ats;
            this._position = position; // Gán position vào biến nội bộ
            //items = ats.ListItems;
        }

        private void Detail_Load(object sender, EventArgs e)
        {
            this.Text = _position;
        }

        private delegate void dAddLog(string log);
        public void Addlog(string log)
        {
            if (rtbLog.InvokeRequired)
            {
                dAddLog d = new dAddLog(Addlog);
                rtbLog.Invoke(d,new object[] {log});
            }
            else
            {
                rtbLog.AppendText(log);
                rtbLog.ScrollToCaret();
            }
        }

        private delegate void dClear();
        public void Clear()
        {
            if (rtbLog.InvokeRequired)
            {
                dClear d = new dClear(Clear);
                rtbLog.Invoke(d);
            }
            else
            {
                rtbLog.Clear();
                dgvItems.Rows.Clear();
            }
        }

        public void ClearLog()
        {
            if (rtbLog.InvokeRequired)
            {
                dClear d = new dClear(ClearLog);
                rtbLog.Invoke(d);
            }
            else
            {
                rtbLog.Clear();
            }
        }

        private delegate string dGetAllLog();
        public string GetAllLog()
        {
            if (rtbLog.InvokeRequired)
            {
                dGetAllLog d = new dGetAllLog(GetAllLog);
                return (string)rtbLog.Invoke(d);
            }
            else
            {
                return rtbLog.Text;
            }
        }

        private delegate void dAddItem(int i, string itemname);
        public void AddItem(int i, string itemname)
        {
            if (this.InvokeRequired)
            {
                dAddItem d = new dAddItem(AddItem);
                this.Invoke(d,new object[] {i,itemname});
            }
            else
            {
                int index = dgvItems.Rows.Add();
                dgvItems.Rows[index].Cells[0].Value = i;
                dgvItems.Rows[index].Cells[1].Value = itemname;
                dgvItems.Rows[index].Cells[2].Value = "";
                dgvItems.Rows[index].Cells[3].Value = "0 s";
                dgvItems.Rows[index].Cells[4].Value = "";
                dgvItems.Rows[index].Cells[5].Value = "";
            }
            
        }

        private void ResetStatus() { }

        private delegate void dUpdateResult(string name, bool result, string test_time);
        public void UpdateResult(string name, bool result, string test_time)
        {
            if (this.InvokeRequired)
            {
                dUpdateResult d = new dUpdateResult(UpdateResult);
                this.Invoke(d, new object[] { name, result,test_time });
            }
            else
            {
                for(int j=0;j< dgvItems.RowCount;j++)
                {
                    if (dgvItems.Rows[j].Cells[1].Value == name)
                    {
                        dgvItems.Rows[j].Cells[2].Value = result == true ? "Pass" : "Fail";
                        dgvItems.Rows[j].Cells[3].Value = test_time;
                        break;
                    }
                }
                //int index = i-1;
                //dgvItems.Rows[index].Cells[2].Value = result == true?"Pass":"Fail";
                //dgvItems.Rows[index].Cells[3].Value = test_time;
            }
        }

        private delegate void dUpdateTime(string name, int time);
        public void UpdateTime(string name, int time)
        {
            if (this.InvokeRequired)
            {
                dUpdateTime d = new dUpdateTime(UpdateTime);
                this.Invoke(d, new object[] { name, time });
            }
            else
            {
                for (int j = 0; j < dgvItems.RowCount; j++)
                {
                    if (dgvItems.Rows[j].Cells[1].Value == name)
                    {
                        dgvItems.Rows[j].Cells[3].Value = time + " s";
                        break;
                    }
                }
                //int index = i;
                //dgvItems.Rows[index].Cells[3].Value = time + " s";
            }
        }

        private delegate void dUpdateStatus(string name, string status);
        public void UpdateStatus(string name, string status)
        {
            if (this.InvokeRequired)
            {
                dUpdateStatus d = new dUpdateStatus(UpdateStatus);
                this.Invoke(d, new object[] { name, status });
            }
            else
            {
                for (int j = 0; j < dgvItems.RowCount; j++)
                {
                    if (dgvItems.Rows[j].Cells[1].Value == name)
                    {
                        dgvItems.Rows[j].Cells[2].Value = status;
                        break;
                    }
                }
                //int index = i - 1;
                //dgvItems.Rows[index].Cells[2].Value = status;
            }
        }

        private void Detail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void mClose()
        {
            this.Close();
        }


        private void Detail_Shown(object sender, EventArgs e)
        {
            RefreshUI();
        }

        private bool ctrl = false;
        private void dgvItems_KeyDown(object sender, KeyEventArgs e)
        {
            Label targetLabel = ats.GetLabelByPosition(_position);
            if (e.KeyCode == Keys.ControlKey)
            {
                ctrl = true;
            }else if(e.KeyCode == Keys.S)
            {
                if (ctrl)
                {
                    if(ats._Labelstatus[targetLabel] != Status.TESTING)
                    {
                        ReAddTestItem();
                    }
                }
            }
            else if (e.KeyCode == Keys.D)
            {
                if (ctrl)
                {
                    if (ats._Labelstatus[targetLabel] != Status.TESTING)
                    {
                        rtbLog.Clear();
                        List<int> arr = new List<int>();
                        int selectedRowCount = dgvItems.Rows.GetRowCount(DataGridViewElementStates.Selected);
                        if (selectedRowCount > 0)
                        {
                            for (int i = 0; i < selectedRowCount; i++)
                            {
                                arr.Add(Convert.ToInt32(dgvItems.SelectedRows[i].Cells[0].Value)-1);
                                dgvItems.SelectedRows[i].Cells[2].Value = "";
                                dgvItems.SelectedRows[i].Cells[3].Value = "0 s";
                            }
                        }
                        Thread t = new Thread(() =>
                        {
                            ats.TestDebug(arr, _position);
                        });
                        t.IsBackground = true;
                        t.Start();
                    }
                }
            }

        }

        private void dgvItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                ctrl = false;
            }
        }

        private void ReAddTestItem()
        {
            try
            {
                List<ItemInfo> ListItems = ats.GetItemListByPosition(_position);
                dgvItems.Rows.Clear();
                for(int i = 0; i < ListItems.Count; i++)
                {
                    AddItem(i + 1, ListItems[i].ItemName);
                }
            }
            catch(Exception ex)
            {
                Addlog(ex.Message);
                Addlog(ex.StackTrace);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshUI();
        }

        private void RefreshUI()
        {
            tabControl1.Update();
            tabControl1.Refresh();
            tabItem.Update();
            tabItem.Refresh();
            tabLog.Update();
            tabLog.Refresh();
        }

        private void Detail_Activated(object sender, EventArgs e)
        {
            RefreshUI();
        }

        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string item_id = dgvItems[0,e.RowIndex].Value.ToString();
            GotoItemLog(item_id);
        }

        private void GotoItemLog(string item_id)
        {
            try
            {
                if (rtbLog.InvokeRequired)
                {
                    int index = rtbLog.Find("ITEM = ID[" + item_id + "]");
                    rtbLog.SelectionStart = index;
                    rtbLog.ScrollToCaret();
                    tabControl1.SelectedTab = tabControl1.TabPages[1];
                }
                else
                {
                    int index = rtbLog.Find("ITEM = ID[" + item_id + "]");
                    rtbLog.SelectionStart = index;
                    rtbLog.ScrollToCaret();
                    tabControl1.SelectedTab = tabControl1.TabPages[1];
                }
                //int index = rtbLog.Find("ITEM = ID["+item_id+"]");
                //rtbLog.SelectionStart = index;
                //rtbLog.ScrollToCaret();
                //tabControl1.SelectedTab = tabControl1.TabPages[1];
            }catch(Exception ex)
            {

            }
        }

        private void tXTLOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ats.OpenLog("txt");
        }

        private void bLELOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ats.OpenLog("ble");
        }

        private void wIFILOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ats.OpenLog("wifi");
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
