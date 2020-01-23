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
using EFaturaApp.Func;
using EfatWebservis;
using NLog;

namespace EFaturaApp
{
    public partial class FaturaDurum : BaseForm
    {

        EKSPRES2017Entities dbEntities = new EKSPRES2017Entities();
        private EdmServisClass currentEdm;
        private Logger logger;

        public FaturaDurum()
        {
            InitializeComponent();
        }

        private void FaturaDurum_Load(object sender, EventArgs e)
        {

        }

        private void Listeleme()
        {
            var ftrList = dbEntities.fatura.Where(x => x.EFaturaDurum != 1300 && x.takipseri=="AEK2020").Select(x => new
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
                x.aciklama2

            });
            radGridView1.DataSource = ftrList.ToList();
            radGridView1.Refresh();
        }
        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            Listeleme();
            WebserviseLoginOl();
        }
        private bool WebserviseLoginOl()
        {
            string edmServiceUrl = "https://portal2.edmbilisim.com.tr/EFaturaEDM/EFaturaEDM.svc?wsdl";
            currentEdm = new EdmServisClass(edmServiceUrl);
            currentEdm.EDMLogin = "monelge";
            currentEdm.EDMPassw = "123654";
            var sessionID = currentEdm.EDMGetSession();
            return true;
        }

        private bool EFaturaDurumEkle(string sFatNo, int sreff)
        {
            try
            {
                var dd = currentEdm.CheckInvoiceStatus(sFatNo, null);
                var lFat = dbEntities.fatura.FirstOrDefault(x => x.@ref == sreff);
                lFat.adi1 = dd.ENVELOPE_IDENTIFIER;
                lFat.EFaturaNo = dd.ID;
                lFat.soyadi1 = dd.RESPONSE_CODE;
                lFat.aciklama2 = dd.STATUS_DESCRIPTION;
                lFat.EFaturaDurum = (short)dd.GIB_STATUS_CODE;
                dbEntities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LoggerClass.ERROR = e.Message;
                return false;
            }
        }
        private void commandBarButton2_Click(object sender, EventArgs e)
        {
            DataTable veri = Func.FuncClass.GridViewToTable(radGridView1);
            for (int i = 0; i < veri.Rows.Count; i++)
            {
                string FaturaNO = veri.Rows[i][1] + veri.Rows[i][2].ToString().PadLeft(9, '0');
                EFaturaDurumEkle(FaturaNO, Convert.ToInt32(veri.Rows[i][0].ToString()));
            }
        }
    }
}
