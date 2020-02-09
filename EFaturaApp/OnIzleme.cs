using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFaturaApp
{
    public partial class OnIzleme : BaseForm
    {
        private string _htmltxt;
        public OnIzleme(string htmltxt)
        {
            InitializeComponent();
            this._htmltxt = htmltxt;
        }

        private void OnIzleme_Load(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = _htmltxt;
        }

        private void OnIzleme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            if (e.KeyCode == Keys.F10)
            {
                ((WebBrowser)sender).Print();

            }
        }
    }
}
