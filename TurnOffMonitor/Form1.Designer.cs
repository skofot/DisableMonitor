﻿namespace TurnOffMonitor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.button1 = new System.Windows.Forms.Button();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.checkBoxTray = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(57, 100);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(121, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Turn off monitor";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// notifyIcon
			// 
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "notifyIcon1";
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.turnOffFromTray);
			// 
			// checkBoxTray
			// 
			this.checkBoxTray.AutoSize = true;
			this.checkBoxTray.Location = new System.Drawing.Point(67, 46);
			this.checkBoxTray.Name = "checkBoxTray";
			this.checkBoxTray.Size = new System.Drawing.Size(98, 17);
			this.checkBoxTray.TabIndex = 1;
			this.checkBoxTray.Text = "Minimize to tray";
			this.checkBoxTray.UseVisualStyleBackColor = true;
			this.checkBoxTray.CheckedChanged += new System.EventHandler(this.checkBoxTray_CheckedChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(239, 144);
			this.Controls.Add(this.checkBoxTray);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Monitor";
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.CheckBox checkBoxTray;
	}
}

