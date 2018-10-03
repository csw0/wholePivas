using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace PivasLabelSelect
{
    public partial class ChoseLabel : Form
    {
        int IsHis = 0;
        public ChoseLabel()
        {
            InitializeComponent();
        }

        public ChoseLabel(int re6)
        {
            InitializeComponent();
            this.IsHis = re6;
        }

        public delegate bool NewDelegate(string LabelNo);
        public static event NewDelegate Choselb;
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                if (IsHis == 1)
                {
                    string text = string.Empty;
                    if (textBox1.Text.Length >= 14)
                    { text = textBox1.Text.Substring(0, 14); }
                    if (Choselb(textBox1.Text))
                    {
                        PlaySound("正常.wav");
                        textBox1.ForeColor = Color.Green;
                        label1.Visible = false;
                    }
                    else if (Choselb(text))
                    {
                        PlaySound("正常.wav");
                        textBox1.ForeColor = Color.Green;
                        label1.Visible = false;
                    }
                    else
                    {
                        PlaySound("不正常.wav");
                        textBox1.ForeColor = Color.Red;
                        label1.Visible = true;
                        listBox1.Items.Add(textBox1.Text);
                    }
                }
                else
                    if (textBox1.Text.Length >= 14)
                {
                    string text = textBox1.Text.Substring(0, 14);


                    if (Choselb(text))
                    {
                        PlaySound("正常.wav");
                        textBox1.ForeColor = Color.Green;
                        label1.Visible = false;
                    }
                    else
                    {
                        PlaySound("不正常.wav");
                        textBox1.ForeColor = Color.Red;
                        label1.Visible = true;
                        listBox1.Items.Add(text);
                    }

                }

                else
                {
                    PlaySound("不正常.wav");
                    textBox1.ForeColor = Color.Red;
                    label1.Visible = true;
                    listBox1.Items.Add(textBox1.Text);

                }
                 textBox1.SelectAll();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PlaySound(string name)
        {
            try
            {
                SoundPlayer media = new System.Media.SoundPlayer(Application.StartupPath + "\\Sound" + "\\" + name);
                media.Play();
            }
            catch { }
        }
    }
}
