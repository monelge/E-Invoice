using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFaturaApp.EntFM;

namespace EFaturaApp
{
    public partial class TopluFatura : BaseForm
    {
        EKSPRES2017Entities dbEntities = new EKSPRES2017Entities();
        public TopluFatura()
        {
            InitializeComponent();
        }

        void sayfaYukle()
        {
            var subeListesi = dbEntities.krhatsub.ToList();

            radDropDownList2.DataSource = subeListesi;
            radDropDownList2.ValueMember = "sube";
            radDropDownList2.DisplayMember = "subeismi";
        }
        private void TopluFatura_Load(object sender, EventArgs e)
        {

        }
    }
}
