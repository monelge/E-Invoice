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
    public partial class FaturaDurum : BaseForm
    {

        EKSPRES2017Entities dbEntities = new EKSPRES2017Entities();
        public FaturaDurum()
        {
            InitializeComponent();
        }

        private void FaturaDurum_Load(object sender, EventArgs e)
        {

        }

        private void Listeleme()
        {
            var ftrList = dbEntities.fatura.Where(x => x.EFaturaDurum == 1).Select(x => new
            {
                x.@ref,
                x.takipseri,
                x.TakipNo,
                x.tarih,
                x.carikod,
                x.adi,
                x.alicisube,
                x.gonderensube,
                x.yekun,
                x.toplamkdv,
                x.toplam,
                x.adi1,
                x.soyadi1,
                x.EFaturaDurum,
                x.EFatura,
                x.EFaturaNo,
                x.EFaturaTipi

            });
            radGridView1.DataSource = ftrList.ToList();
            radGridView1.Refresh();
        }
        private void commandBarButton1_Click(object sender, EventArgs e)
        {
           Listeleme();
        }

        private void commandBarButton2_Click(object sender, EventArgs e)
        {
          
        }
    }
}
