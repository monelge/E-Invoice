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
    }
}
