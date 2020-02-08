using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace EFaturaApp
{
    public partial class MusteriListesi : BaseForm
    {
        Form _form;
        string _aranacak;
        string _textbox1;
        string _textbox2;
        
        public MusteriListesi(Form gform, string garan, string gtext1, string gtext2)
        {
            InitializeComponent();
            this._form = gform;
            this._aranacak = garan;
            this._textbox1 = gtext1;
            this._textbox2 = gtext2;
        }

        private void MusteriListesi_Load(object sender, EventArgs e)
        {
            this.Text = "MONELGE Production - Yazılım ve Bilgisayar Bilişim Hizmetleri Projelendirme ve Üretim Merkezi";

            radGridView1.DataSource = DataBaseSorgu.VeriIsle.data_table("select sube,subeismi,kodu,adi, adres1,adres2, ilce,il, vdairesi,vno from krmuste where adi like '" + _aranacak.ToString() + "%' order by sube");
            radGridView1.Refresh();
            radGridView1.BestFitColumns();
        }

        private void radGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                TextBox aran = (TextBox)_form.Controls.Find(_textbox1.ToString(), true)[0];
                TextBox aran2 = (TextBox)_form.Controls.Find(_textbox2.ToString(), true)[0];

                aran.Text = radGridView1.CurrentRow.Cells[2].Value.ToString();
                aran2.Text = radGridView1.CurrentRow.Cells[3].Value.ToString();

            }
            catch { }
            Close();
        }

        private void radGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    TextBox aran = (TextBox)_form.Controls.Find(_textbox1.ToString(), true)[0];
                    TextBox aran2 = (TextBox)_form.Controls.Find(_textbox2.ToString(), true)[0];

                    aran.Text = radGridView1.CurrentRow.Cells[2].Value.ToString();
                    aran2.Text = radGridView1.CurrentRow.Cells[3].Value.ToString();

                }
                catch { }
                Close();
            }
        }

        private void MusteriListesi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
