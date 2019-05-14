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
		private ContextMenu cont;
		private MenuItem exit;
		private MenuItem open;

		public Form1()
        {
            InitializeComponent();
			LoadOptions();

			cont = new ContextMenu();
			exit = new MenuItem();
			exit.Text = "Exit";
			exit.Click += new System.EventHandler(this.exit_Click);

			open = new MenuItem();
			open.Text = "Reopen";
			open.Click += new System.EventHandler(this.open_Click);

			cont.MenuItems.AddRange(
					new System.Windows.Forms.MenuItem[] { open, exit });

			notifyIcon.ContextMenu = cont;

		}

		private void exit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void open_Click(object sender, EventArgs e)
		{
			notifyIcon.Visible = false;
			this.Show();
			this.WindowState = FormWindowState.Normal;

			this.Focus();
		}

		private void LoadOptions()
		{

			if (File.Exists("MonitorOptions.txt"))
			{
				string[] options = File.ReadAllLines("MonitorOptions.txt");

				if(options.Length > 0 && options[0] == "True")
				{
					checkBoxTray.Checked = true;
				}
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
			else
			{
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

			StreamWriter sw = new StreamWriter("MonitorOptions.txt");

			sw.Write(minimizeToTray);
			sw.Close();
			sw.Dispose();


		}

	}
}
