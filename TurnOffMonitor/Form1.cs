using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurnOffMonitor
{
    public partial class Form1 : Form
    {
		private bool minimizeToTray = false;

		public Form1()
        {
            InitializeComponent();
			LoadOptions();
		}

		private void LoadOptions()
		{
			if (File.Exists("MonitorOptions.txt"))
			{
				
			}
			else
			{
				File.Create("MonitorOptions.txt");
			}
		}







		#region UI handlers
		private void button1_Click(object sender, EventArgs e)
		{
			SetMonitorState(OFF);

		}
		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			SetMonitorState(ON);
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			if (FormWindowState.Minimized == this.WindowState && minimizeToTray)
			{
				notifyIcon.Visible = true;
				notifyIcon.Icon = SystemIcons.Application;
				this.Hide();
			}
			else if (FormWindowState.Normal == this.WindowState)
			{
				notifyIcon.Visible = false;
			}

		}

		//Evoked by double tap
		private void turnOffFromTray(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				SetMonitorState(OFF);
			}
		}

		#endregion


		#region Monitor code
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
		private int ON = -1;
		private int OFF = 2;
		private int STANDBY = 1;

		enum MonitorEnum
		{
			On = -1,
			Off = 2,
			StandBy = 1
		}

		private void SetMonitorState(int state)
		{
			SendMessage(this.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, state);
		}


		public void MonitorOff(IntPtr handle)
		{
			SendMessage(handle, WM_SYSCOMMAND, SC_MONITORPOWER, OFF);
		}

		#endregion

		private void checkBoxTray_CheckedChanged(object sender, EventArgs e)
		{
			minimizeToTray = checkBoxTray.Checked;
		}

	}
}
