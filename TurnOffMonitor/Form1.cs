using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurnOffMonitor
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        
        [DllImport("user32")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lpRect, MonitorEnumProc callback, int dwData);

        private delegate bool MonitorEnumProc(IntPtr hDesktop, IntPtr hdc, ref Rect pRect, int dwData);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        private const int MOUSEEVENTF_MOVE = 0x0001;

        private const int HWND_BROADCAST = 0xFFFF;//the message is sent to all    

        //top-level windows in the system    
        private const int SC_MONITORPOWER = 0xF170;
        private const int WM_SYSCOMMAND = 0x112;

        enum MonitorEnum
        {
            On = -1,
            Off = 2,
            StandBy = 1
        }

        private int ON = -1;
        private int OFF = 2;
        private int STANDBY = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void SetMonitorState(int state)
        {
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, state);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            SetMonitorState(ON);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetMonitorState(OFF);
            
        }

        public void MonitorOff(IntPtr handle)
        {
            SendMessage(handle, WM_SYSCOMMAND, SC_MONITORPOWER, OFF);
        }
        
    }
}
