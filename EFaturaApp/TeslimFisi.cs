using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFaturaApp
{
    public partial class TeslimFisi : BaseForm
    {
        string _tseri;
        string _tesno;

        public TeslimFisi(string tseri, string tesno)
        {
            InitializeComponent();
            _tseri = tseri;
            _tesno = tesno;
        }

        byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void TeslimFisi_Load(object sender, EventArgs e)
        {
            DataTable resim = DataBaseSorgu.VeriIsle.data_table_3("select image from TblScanner where takipseri='" + _tseri.ToString() + "' and takipno='" + _tesno.ToString() + "'");
            if (resim.Rows.Count != 0)
            {
                pictureBox1.Image = ByteArrayToImage((byte[])resim.Rows[0][0]);
            }
        }

        private void TeslimFisi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
