namespace PivasBatchCommon
{
    partial class Wait
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PB_GetOrder = new System.Windows.Forms.ProgressBar();
            this.Label_Order = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PB_GetOrder
            // 
            this.PB_GetOrder.Location = new System.Drawing.Point(42, 34);
            this.PB_GetOrder.Name = "PB_GetOrder";
            this.PB_GetOrder.Size = new System.Drawing.Size(212, 36);
            this.PB_GetOrder.TabIndex = 0;
            this.PB_GetOrder.Visible = false;
            // 
            // Label_Order
            // 
            this.Label_Order.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Label_Order.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label_Order.Font = new System.Drawing.Font("华文楷体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Order.Location = new System.Drawing.Point(0, 0);
            this.Label_Order.Name = "Label_Order";
            this.Label_Order.Size = new System.Drawing.Size(298, 57);
            this.Label_Order.TabIndex = 1;
            this.Label_Order.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 30000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Image = global::PivasBatchCommon.Properties.Resources.关闭;
            this.button1.Location = new System.Drawing.Point(272, -1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 23);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Wait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(298, 57);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Label_Order);
            this.Controls.Add(this.PB_GetOrder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Wait";
            this.Opacity = 0.8;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wait";
            this.Load += new System.EventHandler(this.Wait_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar PB_GetOrder;
        private System.Windows.Forms.Label Label_Order;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button button1;
    }
}