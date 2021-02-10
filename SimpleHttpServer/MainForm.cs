using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimpleHttpServer
{
    public partial class MainForm : Form,IView
    {
        public event EventHandler DetectClicked;
        public event EventHandler StartClicked;
        public MainForm()
        {
            InitializeComponent();
        }

        public void SetServerList(IEnumerable<string> l)
        {
            cmbServerList.DataSource = l;
        }


        public int GetPort() => (int) NupPort.Value;

        public void SetPort(int value)
        {
            NupPort.Value = value;
        }

        public string GetIp()
        {
            return cmbServerList.SelectedItem.ToString();
        }

        public void SetStartText(string text)
        {
            Start.Text = text;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartClicked?.Invoke(sender,e);
        }
    }
}
