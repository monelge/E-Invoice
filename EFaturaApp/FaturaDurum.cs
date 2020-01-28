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
using Telerik.WinControls;
using Telerik.WinControls.UI;

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
            int iSbKd = Convert.ToInt32(FuncClass.SubeKoduNo);
            var ftrList = dbEntities.fatura.Where(x => x.soyadi1 != "ACCEPT" && x.takipseri == "AEK2020" && x.iptal != "1" && x.alicisube==iSbKd).Select(x => new
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
            radGridView1.Columns[0].Width = 5;
            radGridView1.Columns[1].BestFit();
            radGridView1.Columns[2].BestFit();
            radGridView1.Columns[3].BestFit();
            radGridView1.Columns[4].Width = 5;
            radGridView1.Columns[5].BestFit();
            radGridView1.Columns[6].BestFit();
            radGridView1.Columns[7].BestFit();
            radGridView1.Columns[10].BestFit();
            radGridView1.Columns[11].Width = 5;
            radGridView1.Columns[13].Width = 5;
            radGridView1.Columns[14].Width = 5;



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
                lFat.soyadi1 = dd.RESPONSE_CODE == "" ? dd.STATUS : dd.RESPONSE_CODE;
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

        private void radGridView1_RowFormatting(object sender, Telerik.WinControls.UI.RowFormattingEventArgs e)
        {
            if ((string)e.RowElement.RowInfo.Cells["aciklama2"].Value == "WAIT_APPLICATION_RESPONSE")
            {
                e.RowElement.DrawFill = true;
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = Color.Yellow;
            }
            else
            {
                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
            }
        }
    }
}
