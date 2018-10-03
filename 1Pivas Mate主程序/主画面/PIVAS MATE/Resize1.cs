using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace Courseware_generation
{
    public class Resize
    {

        bool IsMoving = false;
        int ctrlLastWidth = 0;
        int ctrlLastHeight = 0;
        int ctrlWidth;
        int ctrlHeight;
        int ctrlLeft;
        int ctrlTop;
        int cursorL;
        int cursorT;
        int ctrlLastLeft;
        int ctrlLastTop;
        int Htap;
        int Wtap;
        bool ctrlIsResizing = false;
        System.Drawing.Rectangle ctrlRectangle = new System.Drawing.Rectangle();
        //private Control ctrl;
        private Form ctrl;
        private Form frm;


        public Resize(Form c, Form frm)
        {
            ctrl = c;
            this.frm = frm;
            this.Htap = this.frm.Height - this.frm.ClientRectangle.Height;
            this.Wtap = this.frm.Width - this.frm.ClientRectangle.Width;
            ctrl.MouseDown += new MouseEventHandler(MouseDown);
            ctrl.MouseMove += new MouseEventHandler(MouseMove);
            ctrl.MouseUp += new MouseEventHandler(MouseUp);
        }
        private void MouseMove(object sender, MouseEventArgs e)
        {
            //   MessageBox.Show("MouseMove");
            if (frm == null)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (this.IsMoving)
                {
                    if (ctrlLastLeft == 0)
                        ctrlLastLeft = ctrlLeft;
                    if (ctrlLastTop == 0)
                        ctrlLastTop = ctrlTop;
                    int locationX = (Cursor.Position.X - this.cursorL + this.frm.DesktopLocation.X + this.Wtap + this.ctrl.Location.X);
                    int locationY = (Cursor.Position.Y - this.cursorT + this.frm.DesktopLocation.Y + this.Htap + this.ctrl.Location.Y);
                    if (locationX < this.frm.DesktopLocation.X + this.Wtap)
                        locationX = this.frm.DesktopLocation.X + this.Wtap;
                    if (locationY < this.frm.DesktopLocation.Y + this.Htap)
                        locationY = this.frm.DesktopLocation.Y + this.Htap;
                    this.ctrlLeft = locationX;
                    this.ctrlTop = locationY;
                    ctrlRectangle.Location = new System.Drawing.Point(this.ctrlLastLeft, this.ctrlLastTop);
                    ctrlRectangle.Size = new System.Drawing.Size(ctrlWidth, ctrlHeight);
                    ControlPaint.DrawReversibleFrame(ctrlRectangle, Color.Empty, System.Windows.Forms.FrameStyle.Dashed);
                    ctrlLastLeft = ctrlLeft;
                    ctrlLastTop = ctrlTop;
                    ctrlRectangle.Location = new System.Drawing.Point(ctrlLeft, ctrlTop);
                    ctrlRectangle.Size = new System.Drawing.Size(ctrlWidth, ctrlHeight);
                    ControlPaint.DrawReversibleFrame(ctrlRectangle, Color.Empty, System.Windows.Forms.FrameStyle.Dashed);
                    return;
                }
                int sizeageX = (Cursor.Position.X - this.frm.DesktopLocation.X - this.Wtap - this.ctrl.Location.X);
                int sizeageY = (Cursor.Position.Y - this.frm.DesktopLocation.Y - this.Htap - this.ctrl.Location.Y);
                if (sizeageX < 2)
                    sizeageX = 1;
                if (sizeageY < 2)
                    sizeageY = 1;
                ctrlWidth = sizeageX;
                ctrlHeight = sizeageY;
                if (ctrlLastWidth == 0)
                    ctrlLastWidth = ctrlWidth;
                if (ctrlLastHeight == 0)
                    ctrlLastHeight = ctrlHeight;
                if (ctrlIsResizing)
                {
                    ctrlRectangle.Location = new System.Drawing.Point(this.frm.DesktopLocation.X + this.ctrl.Left + this.Wtap, this.frm.DesktopLocation.Y + this.Htap + this.ctrl.Top);
                    ctrlRectangle.Size = new System.Drawing.Size(ctrlLastWidth, ctrlLastHeight);
                }
                ctrlIsResizing = true;
                ControlPaint.DrawReversibleFrame(ctrlRectangle, Color.Empty, System.Windows.Forms.FrameStyle.Dashed);
                ctrlLastWidth = ctrlWidth;
                ctrlLastHeight = ctrlHeight;
                ctrlRectangle.Location = new System.Drawing.Point(this.frm.DesktopLocation.X + this.Wtap + this.ctrl.Left, this.frm.DesktopLocation.Y + this.Htap + this.ctrl.Top);
                ctrlRectangle.Size = new System.Drawing.Size(ctrlWidth, ctrlHeight);
                ControlPaint.DrawReversibleFrame(ctrlRectangle, Color.Empty, System.Windows.Forms.FrameStyle.Dashed);
            }
        }
        private void MouseDown(object sender, MouseEventArgs e)
        {
            if (frm == null)
                return;
            if (e.X < this.ctrl.Width - 10 || e.Y < this.ctrl.Height - 10)
            {
                this.IsMoving = true;
                this.ctrlLeft = this.frm.DesktopLocation.X + this.Wtap + this.ctrl.Left;
                this.ctrlTop = this.frm.DesktopLocation.Y + this.Htap + this.ctrl.Top;
                this.cursorL = Cursor.Position.X;
                this.cursorT = Cursor.Position.Y;
                this.ctrlWidth = this.ctrl.Width;
                this.ctrlHeight = this.ctrl.Height;
            }
            ctrlRectangle.Location = new System.Drawing.Point(this.ctrlLeft, this.ctrlTop);
            ctrlRectangle.Size = new System.Drawing.Size(ctrlWidth, ctrlHeight);
            ControlPaint.DrawReversibleFrame(ctrlRectangle, Color.Empty, System.Windows.Forms.FrameStyle.Dashed);
        }
        private void MouseUp(object sender, MouseEventArgs e)
        {
            if (frm == null)
                return;
            ctrlIsResizing = false;
            if (this.IsMoving)
            {
                ctrlRectangle.Location = new System.Drawing.Point(this.ctrlLeft, this.ctrlTop);
                ctrlRectangle.Size = new System.Drawing.Size(ctrlWidth, ctrlHeight);
                ControlPaint.DrawReversibleFrame(ctrlRectangle, Color.Empty, System.Windows.Forms.FrameStyle.Dashed);
                this.ctrl.Left = this.ctrlLeft - this.frm.DesktopLocation.X - this.Wtap;
                this.ctrl.Top = this.ctrlTop - this.frm.DesktopLocation.Y - this.Htap;
                this.IsMoving = false;
                this.ctrl.Refresh();
                return;
            }
            ctrlRectangle.Location = new System.Drawing.Point(this.frm.DesktopLocation.X + this.Wtap + this.ctrl.Left, this.frm.DesktopLocation.Y + this.Htap + this.ctrl.Top);
            ctrlRectangle.Size = new System.Drawing.Size(ctrlWidth, ctrlHeight);
            ControlPaint.DrawReversibleFrame(ctrlRectangle, Color.Empty, System.Windows.Forms.FrameStyle.Dashed);
            this.ctrl.Width = ctrlWidth;
            this.ctrl.Height = ctrlHeight;
            this.ctrl.Refresh();
        }


    }
}