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
    public partial class Loading : Form
    {
        public bool bClose = false;
        private Form1 _form1;
        public Loading(Form1 form)
        {
            _form1 = form;
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            this.Activate();
            Thread CheckToCloseThread = new Thread(new ThreadStart(CheckToClose));
            CheckToCloseThread.Start();
        }

        public void CheckToClose()
        {
            while (!bClose)
            {
                Thread.Sleep(10);
            }
            SetFormCloseCallback(this);
        }

        delegate void dSetFormCloseCallback(Form form);

        private void SetFormCloseCallback(Form form)
        {
            try
            {
                if (form.InvokeRequired)
                {
                    dSetFormCloseCallback d = new dSetFormCloseCallback(SetFormCloseCallback);
                    form.Invoke(d, new object[] { form });
                }
                else
                {
                    _form1.Active();
                    form.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
