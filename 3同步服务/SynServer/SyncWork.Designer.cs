using System;

namespace SynServer
{
    partial class SyncWork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncWork));
            this.listBoxMsg = new System.Windows.Forms.ListBox();
            this.labelShowMsg = new System.Windows.Forms.Label();
            this.pictureBoxSynWait = new System.Windows.Forms.PictureBox();
            this.pictureBoxSuccess = new System.Windows.Forms.PictureBox();
            this.pictureBoxFail = new System.Windows.Forms.PictureBox();
            this.pictureBoxSynRuning = new System.Windows.Forms.PictureBox();
            this.lblSyncMacIPTime = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelMin = new System.Windows.Forms.Panel();
            this.labelCompany = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSynWait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSuccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSynRuning)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxMsg
            // 
            this.listBoxMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.listBoxMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.listBoxMsg.FormattingEnabled = true;
            this.listBoxMsg.ItemHeight = 12;
            this.listBoxMsg.Location = new System.Drawing.Point(294, 65);
            this.listBoxMsg.Margin = new System.Windows.Forms.Padding(1);
            this.listBoxMsg.Name = "listBoxMsg";
            this.listBoxMsg.Size = new System.Drawing.Size(288, 276);
            this.listBoxMsg.TabIndex = 19;
            // 
            // labelShowMsg
            // 
            this.labelShowMsg.AutoSize = true;
            this.labelShowMsg.ForeColor = System.Drawing.Color.Red;
            this.labelShowMsg.Location = new System.Drawing.Point(3, 308);
            this.labelShowMsg.Name = "labelShowMsg";
            this.labelShowMsg.Size = new System.Drawing.Size(221, 12);
            this.labelShowMsg.TabIndex = 20;
            this.labelShowMsg.Text = "点击启动同步，并且将关闭其他同步程序";
            // 
            // pictureBoxSynWait
            // 
            this.pictureBoxSynWait.BackgroundImage = global::SynServer.Properties.Resources.png2068;
            this.pictureBoxSynWait.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSynWait.Location = new System.Drawing.Point(33, 65);
            this.pictureBoxSynWait.Name = "pictureBoxSynWait";
            this.pictureBoxSynWait.Size = new System.Drawing.Size(257, 240);
            this.pictureBoxSynWait.TabIndex = 23;
            this.pictureBoxSynWait.TabStop = false;
            this.pictureBoxSynWait.Click += new System.EventHandler(this.pictureBoxSynWait_Click);
            // 
            // pictureBoxSuccess
            // 
            this.pictureBoxSuccess.BackgroundImage = global::SynServer.Properties.Resources.DatabaseCopyDatabaseFile2;
            this.pictureBoxSuccess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxSuccess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSuccess.Location = new System.Drawing.Point(538, 351);
            this.pictureBoxSuccess.Name = "pictureBoxSuccess";
            this.pictureBoxSuccess.Size = new System.Drawing.Size(33, 35);
            this.pictureBoxSuccess.TabIndex = 22;
            this.pictureBoxSuccess.TabStop = false;
            this.pictureBoxSuccess.Visible = false;
            // 
            // pictureBoxFail
            // 
            this.pictureBoxFail.BackgroundImage = global::SynServer.Properties.Resources.FileDropSqlDatabase2;
            this.pictureBoxFail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxFail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxFail.Location = new System.Drawing.Point(538, 351);
            this.pictureBoxFail.Name = "pictureBoxFail";
            this.pictureBoxFail.Size = new System.Drawing.Size(33, 35);
            this.pictureBoxFail.TabIndex = 1;
            this.pictureBoxFail.TabStop = false;
            this.pictureBoxFail.Click += new System.EventHandler(this.pictureBoxFail_Click);
            // 
            // pictureBoxSynRuning
            // 
            this.pictureBoxSynRuning.BackgroundImage = global::SynServer.Properties.Resources.png2071;
            this.pictureBoxSynRuning.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxSynRuning.Location = new System.Drawing.Point(33, 65);
            this.pictureBoxSynRuning.Name = "pictureBoxSynRuning";
            this.pictureBoxSynRuning.Size = new System.Drawing.Size(257, 240);
            this.pictureBoxSynRuning.TabIndex = 0;
            this.pictureBoxSynRuning.TabStop = false;
            this.pictureBoxSynRuning.Visible = false;
            // 
            // lblSyncMacIPTime
            // 
            this.lblSyncMacIPTime.AutoSize = true;
            this.lblSyncMacIPTime.ForeColor = System.Drawing.Color.White;
            this.lblSyncMacIPTime.Location = new System.Drawing.Point(3, 330);
            this.lblSyncMacIPTime.Name = "lblSyncMacIPTime";
            this.lblSyncMacIPTime.Size = new System.Drawing.Size(0, 12);
            this.lblSyncMacIPTime.TabIndex = 24;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "同步程序";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // PanelMin
            // 
            this.PanelMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.PanelMin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PanelMin.BackgroundImage")));
            this.PanelMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelMin.Location = new System.Drawing.Point(561, 1);
            this.PanelMin.Name = "PanelMin";
            this.PanelMin.Size = new System.Drawing.Size(21, 26);
            this.PanelMin.TabIndex = 26;
            this.PanelMin.Click += new System.EventHandler(this.PanelMin_Click);
            this.PanelMin.MouseLeave += new System.EventHandler(this.PanelMin_MouseLeave);
            this.PanelMin.MouseHover += new System.EventHandler(this.PanelMin_MouseHover);
            // 
            // labelCompany
            // 
            this.labelCompany.AutoSize = true;
            this.labelCompany.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCompany.ForeColor = System.Drawing.Color.Silver;
            this.labelCompany.Location = new System.Drawing.Point(1, 2);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(145, 10);
            this.labelCompany.TabIndex = 27;
            this.labelCompany.Text = "上海博龙智医科技股份有限公司";
            // 
            // SyncWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(107)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(583, 398);
            this.ControlBox = false;
            this.Controls.Add(this.labelCompany);
            this.Controls.Add(this.PanelMin);
            this.Controls.Add(this.lblSyncMacIPTime);
            this.Controls.Add(this.pictureBoxSynWait);
            this.Controls.Add(this.pictureBoxSuccess);
            this.Controls.Add(this.labelShowMsg);
            this.Controls.Add(this.listBoxMsg);
            this.Controls.Add(this.pictureBoxFail);
            this.Controls.Add(this.pictureBoxSynRuning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SyncWork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncWork_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SyncWork_FormClosed);
            this.Load += new System.EventHandler(this.SyncWork_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SyncWork_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSynWait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSuccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSynRuning)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.PictureBox pictureBoxSynRuning;
        private System.Windows.Forms.Label labelShowMsg;
        private System.Windows.Forms.PictureBox pictureBoxSynWait;
        protected internal System.Windows.Forms.PictureBox pictureBoxFail;
        protected internal System.Windows.Forms.PictureBox pictureBoxSuccess;
        private System.Windows.Forms.Label lblSyncMacIPTime;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        public System.Windows.Forms.ListBox listBoxMsg;
        private System.Windows.Forms.Panel PanelMin;
        private System.Windows.Forms.Label labelCompany;
    }
}