using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFaturaApp.Func;
using EfatWebservis;
using EntFMSystem;
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

        private Panel panel = new Panel();
        private ProgressBar progressBar = new ProgressBar();
        private Label _label = new Label();

        private BackgroundWorker worker = new BackgroundWorker();
        private int _durum;
        public FaturaDurum(int durum)
        {
            InitializeComponent();
            worker.DoWork += WorkerOnDoWork;
            this._durum = durum;
        }

        void loadinPanelOrtala()
        {

            panel1.Location = new Point(
                this.ClientSize.Width / 2 - panel1.Size.Width / 2,
                this.ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel.Anchor = AnchorStyles.None;

            _label.Location = new Point(
                panel1.ClientSize.Width / 2 - radLabel1.Size.Width / 2,
                panel1.ClientSize.Height / 2 - radLabel1.Size.Height / 2);
            _label.Anchor = AnchorStyles.None;

        }

        void FaturaKontrol()
        {
            DataTable veri = Func.FuncClass.GridViewToTable(radGridView1);

            panel1.Invoke(new Action(() => { panel1.Visible = true; }));
            for (int i = 0; i < veri.Rows.Count; i++)
            {
                string FaturaNO = veri.Rows[i][1] + veri.Rows[i][2].ToString().PadLeft(9, '0');
                //  progressBar1.Invoke(new Action(() => progressBar1.Value = i));
                this.Invoke(new Action(() =>
                {
                    progressBar1.Maximum = radGridView1.Rows.Count;
                    progressBar1.Minimum = 0;
                    radLabel1.Text = "Kontrol edilen Fatura No : " + FaturaNO;
                    progressBar1.Value = i;
                    progressBar1.Text = "Toplam :" + i + " / " + radGridView1.Rows.Count;
                }));

                EFaturaDurumEkle(FaturaNO, Convert.ToInt32(veri.Rows[i][0].ToString()));
            }

            panel1.Invoke(new Action(() => { panel1.Visible = false; }));
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            FaturaKontrol();
        }

        private void FaturaDurum_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void Listeleme()
        {
            int iSbKd = Convert.ToInt32(FuncClass.SubeKoduNo);
            if (_durum == 0)
            {
                var ftrList = dbEntities.fatura.Where(x =>
                    (x.soyadi1 != "ACCEPT" && x.soyadi1 != "SEND - SUCCEED") &&
                    (x.takipseri == FuncClass.FSeriNO || x.takipseri == FuncClass.FSerbestSeriNO) && x.iptal != "1" //&& x.alicisube == iSbKd
                    ).Select(
                    x => new
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
            }
            if (_durum == 1)
            {
                var ftrList = dbEntities.fatura.Where(x =>
                    (x.soyadi1 != "ACCEPT" && x.soyadi1 != "SEND - SUCCEED") &&
                    (x.takipseri == FuncClass.FArsivNO) && x.iptal != "1" &&
                    x.gonderensube == iSbKd).Select(
                    x => new
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
            }

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
            loadinPanelOrtala();
            commandBarButton2.Enabled = true;
            panel1.BringToFront();
            worker.RunWorkerAsync();
            commandBarButton2.Enabled = false;

        }

        private void radGridView1_RowFormatting(object sender, Telerik.WinControls.UI.RowFormattingEventArgs e)
        {

            switch ((string)e.RowElement.RowInfo.Cells["soyadi1"].Value)
            {
                case "SEND - WAIT_APPLICATION_RESPONSE":
                    e.RowElement.DrawFill = true;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                    e.RowElement.BackColor = Color.Yellow;
                    break;
                case "REJECT":
                    e.RowElement.DrawFill = true;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                    e.RowElement.BackColor = Color.Red;
                    break;
                case "LOAD - SUCCEED":
                    e.RowElement.DrawFill = true;
                    e.RowElement.GradientStyle = GradientStyles.Solid;
                    e.RowElement.BackColor = Color.DodgerBlue;
                    break;
                default:
                    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                    break;
            }
            
        }

       private void commandBarButton3_Click_1(object sender, EventArgs e)
        {
            Close();
        }

       private void commandBarButton4_Click(object sender, EventArgs e)
       {

           for (int i = 0; i < radGridView1.Rows.Count; i++)
           {
               int iRef = Convert.ToInt32(radGridView1.Rows[i].Cells["ref"].Value.ToString());
               var Ftr = dbEntities.fatura.FirstOrDefault(f => f.@ref == iRef);
               Ftr.EFaturaNo = null;
               Ftr.adi1 = null;
               Ftr.soyadi1 = null;
               dbEntities.SaveChanges();
           }
           Listeleme();
       }
    }
}
