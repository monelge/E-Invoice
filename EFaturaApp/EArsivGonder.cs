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
using EFaturaApp.Func;
using EfatWebservis;
using EntFMSystem;
using FastReport;
using hm.common;
using Telerik.WinControls;

namespace EFaturaApp
{
    public partial class EArsivGonder : BaseForm
    {
        EKSPRES2017Entities ekspres2017Entities = new EKSPRES2017Entities();
        BackgroundWorker worker = new BackgroundWorker();
        private bool Yazdirma;

        public EArsivGonder()
        {
            InitializeComponent();
            worker.DoWork += WorkerOnDoWork;
            loadinPanelOrtala();
        }
        void loadinPanelOrtala()
        {

            radPanel1.Location = new Point(
                this.ClientSize.Width / 2 - radPanel1.Size.Width / 2,
                this.ClientSize.Height / 2 - radPanel1.Size.Height / 2);
            radPanel1.Anchor = AnchorStyles.None;
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            Gonder(3);
        }
        public DataTable ViewToTable(DataGridView gridView)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in gridView.Columns)
            {
                dt.Columns.Add(col.HeaderText);
            }

            foreach (DataGridViewRow row in gridView.Rows)
            {
                if ((bool)row.Cells["isaret"].Value == true)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }

                    dt.Rows.Add(dRow);
                }
            }

            return dt;
        }

        bool Gonder(int FtTur)
        {
            try
            {
                radPanel1.Invoke(new Action(() => { radPanel1.Visible = true; }));

                DataTable dataTable = ViewToTable(dataGridView1);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {

                    int _ref = Convert.ToInt32(dataTable.Rows[i]["ref"].ToString());
                    var Fatura = ekspres2017Entities.fatura.FirstOrDefault(f => f.@ref == _ref);
                    var FatLst =
                        ekspres2017Entities.faturahar.Where(h =>
                            h.takipseri == Fatura.takipseri && h.fatno == Fatura.TakipNo).ToList();
                    string FaturaNO = Fatura.takipseri + Fatura.TakipNo.ToString().PadLeft(9, '0');
                    this.Invoke(new Action(() =>
                    {
                        radProgressBar1.Maximum = dataTable.Rows.Count;
                        radProgressBar1.Minimum = 0;
                        radLabel1.Text = "Gönderilen Fatura No : " + FaturaNO;
                        radProgressBar1.Value1 = i + 1;
                        radProgressBar1.Text = "Toplam :" + i + " / " + (dataTable.Rows.Count);
                    }));
                    string sonuc = EfatWebservis.FaturaIslem.FaturaXml(Fatura, FatLst, FtTur);
                    if (sonuc.Length > 2)
                    {
                       RadMessageBox.Show(sonuc, "Hata Oluştu", MessageBoxButtons.OK, RadMessageIcon.Error); 
                    }

                    if (Yazdirma == true)
                    {
                        EarasivYazdir(Fatura, FatLst);
                    }
                }
                this.Invoke(new Action(() =>
                {
                    listele();
                    radPanel1.Visible = false;
                    RadMessageBox.Show("Faturalar başarılı şekilde gönderildi", "Tebrikler", MessageBoxButtons.OK, RadMessageIcon.Info);
                }));
                listele();
                return true;
            }
            catch (Exception e)
            {
                FuncClass.hataKaydet(e, this);
                return false;
            }
        }

        bool listele()
        {
            int sube = Convert.ToInt32(FuncClass.SubeKoduNo);
            var getir = ekspres2017Entities.fatura.Where(x => x.EFaturaNo == null && x.takipseri == FuncClass.FArsivNO && x.gonderensube == sube && x.iptal != "1").Select(x => new FaturaClass
            {
                isaret = (bool)(x.isaret == null || x.isaret == "0" ? false : true),
                takipseri = x.takipseri,
                TakipNo = x.TakipNo,
                tarih = x.tarih,
                carikod = x.carikod,
                adi = x.adi,
                yekun = x.yekun,
                toplamkdv = x.toplamkdv,
                toplam = x.toplam,
                alicisube = x.alicisube,
                @ref = x.@ref
            }).ToList();
            dataGridView1.DataSource = getir;
            dataGridView1.Refresh();
            return true;

        }
        private void EArsivGonder_Load(object sender, EventArgs e)
        {
            radPanel1.Visible = false;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool tt = (bool)dataGridView1.CurrentRow.Cells[10].Value;
                int _ref = (int)dataGridView1.CurrentRow.Cells[0].Value;

                if (tt == true)
                {
                    dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.Red;
                    var cellValeu = ekspres2017Entities.fatura.FirstOrDefault(f => f.@ref == _ref);
                    cellValeu.isaret = "1";
                    ekspres2017Entities.SaveChanges();
                }
                if (tt == false)
                {
                    var cellValeu = ekspres2017Entities.fatura.FirstOrDefault(f => f.@ref == _ref);
                    cellValeu.isaret = "0";
                    ekspres2017Entities.SaveChanges();
                    dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.White;
                }
            }
            catch (Exception exception)
            {

            }
        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            listele();
        }

        bool EarasivYazdir(fatura Fatura, List<faturahar> FatHark)
        {
            try
            {
                DataTable ftrLStTable = FuncClass.ToDataTable(FatHark);
                ftrLStTable.TableName = "bilgi";
                Report report = new Report();
                report.Load(@"Earsiv.frx");
                // (report.FindObject("Data1") as DataBand).DataSource = report.GetDataSource("bilgi");

                report.RegisterData(ftrLStTable, ftrLStTable.TableName);

                report.GetDataSource(ftrLStTable.TableName).Enabled = true;
                DataBand dataBand = (DataBand)report.FindObject("Data1");
                dataBand.DataSource = report.GetDataSource(ftrLStTable.TableName);
                //  DataBand databand = (DataBand)report.FindObject("Data1");
                //   databand.DataSource = report.GetDataSource("bilgi");


                //baslik
                TextObject fatNO = report.FindObject("text6") as TextObject;
                fatNO.Text = Fatura.takipseri + Fatura.TakipNo.ToString().PadLeft(9, '0');

                TextObject fatTrh = report.FindObject("text7") as TextObject;
                fatTrh.Text = Fatura.tarih.Value.ToShortDateString();


                TextObject fatYekun = report.FindObject("text31") as TextObject;
                fatYekun.Text = Fatura.yekun.ToString();
                TextObject fatIsk = report.FindObject("text33") as TextObject;
                fatIsk.Text = "0";
                TextObject fatKDV = report.FindObject("text35") as TextObject;
                fatKDV.Text = Fatura.toplamkdv.ToString();
                TextObject fatGenTop = report.FindObject("text37") as TextObject;
                fatGenTop.Text = Fatura.toplam.ToString();
                TextObject fatOdnTutar = report.FindObject("text39") as TextObject;
                fatOdnTutar.Text = Fatura.toplam.ToString();

                TextObject AciklamaTxt = report.FindObject("text40") as TextObject;
                AciklamaTxt.Text = FuncClass.ibanNo.ToString() + " \r\nFaturayı Kesen Şube : " + Fatura.alicisube.ToString() + "\r\nYazı İle " +
                                   Tools.GetCurrencyText(Convert.ToDouble(Fatura.toplam), "TRY") +
                                   "\r\nİş bu fatura muhteviyatına 7 gün içerisinde itiraz edilmediği taktirde aynen kabul edilmiş sayılır.";

                var KrMuste = ekspres2017Entities.krmuste.FirstOrDefault(x => x.kodu == Fatura.carikod);
                TextObject MusteriTxt = report.FindObject("text2") as TextObject;
                MusteriTxt.Text = KrMuste.adi + "\r\n\r" + KrMuste.adres1 + " " + KrMuste.adres2 +
                                  "\r\n\r" + KrMuste.ilce + "/" + KrMuste.il +
                                  "\r\n\r" + KrMuste.tel1 + " - " + KrMuste.tel4 +
                                  "\r\n\r" + KrMuste.email +
                                  "\r\n\r" + KrMuste.vdairesi + " / " + KrMuste.vno;
                TextObject ettnTxt = report.FindObject("text13") as TextObject;
                string Fseri = Fatura.takipseri.ToString();
                int FatNO = Fatura.TakipNo;
                var Ftr = ekspres2017Entities.fatura.FirstOrDefault(f => f.takipseri == Fseri && f.TakipNo == FatNO);
                ettnTxt.Text = "ETTN : " + OrtakClass.ettn ?? "0";
                Ftr.basildimi = "1";
                ekspres2017Entities.SaveChanges();


                report.PrintSettings.ShowDialog = false;
                // report.Show();
                report.Prepare();
                report.PrintPrepared();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        private void commandBarButton2_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = RadMessageBox.Show("E-Arşiv Faturaları yazdırmak istiyormusunuz?", "EfaturaApp", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                Yazdirma = true;
            }
            else
            {
                Yazdirma = false;
            }
            worker.RunWorkerAsync();

        }

        private void commandBarButton4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool tt = (bool)dataGridView1.Rows[i].Cells[10].Value;
                if (tt == true)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = Color.Red;
                }
            }
        }

        private void önİzlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sTakipSeri = dataGridView1.CurrentRow.Cells["takipseri"].Value.ToString();
            int iTakipNO = Convert.ToInt32(dataGridView1.CurrentRow.Cells["TakipNo"].Value.ToString());
            var FtrDty = ekspres2017Entities.fatura.FirstOrDefault(f => f.takipseri == sTakipSeri && f.TakipNo == iTakipNO);
            var FtrFryList = ekspres2017Entities.faturahar
                .Where(h => h.takipseri == FtrDty.takipseri && h.fatno == FtrDty.TakipNo).ToList();
            string xml = FaturaIslem.FaturaXmlString(FtrDty, FtrFryList, 1);
            string template = File.ReadAllText(@"GeneralFormArsiv.xslt");
            string html = FuncClass.ConvertToHtml(template, xml);
            OnIzleme onIzleme = new OnIzleme(html);
            onIzleme.ShowDialog();
        }
    }
}
