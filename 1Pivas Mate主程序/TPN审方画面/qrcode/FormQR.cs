using System;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;

namespace qrcode
{
    public partial class frmQR : Form
    {
        private string QRCode = "";

        public frmQR()
        {
            InitializeComponent();
            lblTip.Text = "";
            this.QRCode = "";
        }

        public static void showQR(string _title, string _qr, string _tip = "")
        {
            frmQR frm = new frmQR();
            frm.Text = _title;
            frm.initQR(_qr, _tip);
            frm.ShowDialog();
        }

        public void initQR(string _qr, string _tip = "")
        {
            lblTip.Text = _tip;
            this.QRCode = _qr;

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 8;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;

            System.Drawing.Image imgQR = qrCodeEncoder.Encode(_qr);
            this.picQR.Image = imgQR;

            //imgQR.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picQR_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.QRCode))
                return;

            Clipboard.SetText(this.QRCode);
            MessageBox.Show("二维码内容已经复制到剪贴板");
        }
    }
}
