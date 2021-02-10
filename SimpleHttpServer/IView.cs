using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimpleHttpServer
{
    public interface IView
    {
        event EventHandler DetectClicked;

        event EventHandler StartClicked;
        void SetServerList(IEnumerable<string> l);

        DialogResult ShowDialog();

        int GetPort();

        void SetPort(int value);


        string GetIp();

        void SetStartText(string text);
    }
}
