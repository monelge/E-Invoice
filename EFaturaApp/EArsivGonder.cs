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
using EntFMSystem;
using FastReport;
using hm.common;
using Telerik.WinControls;

namespace EFaturaApp
{
    public partial class EArsivGonder : BaseForm
    {
        EKSPRES2017Entities ekspres2017Entities = new EKSPRES2017Entities();
        public EArsivGonder()
        {
            InitializeComponent();
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
                DataTable dataTable = ViewToTable(dataGridView1);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    int _ref = Convert.ToInt32(dataTable.Rows[i]["ref"].ToString());
                    var Fatura = ekspres2017Entities.fatura.FirstOrDefault(f => f.@ref == _ref);
                    var FatLst =
                        ekspres2017Entities.faturahar.Where(h =>
                            h.takipseri == Fatura.takipseri && h.fatno == Fatura.TakipNo).ToList();
                    //bool sonuc = EfatWebservis.FaturaIslem.FaturaXml(Fatura, FatLst, FtTur);
                    EarasivYazdir(Fatura, FatLst);
                }

                listele();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        bool listele()
        {
            int sube = Convert.ToInt32(FuncClass.SubeKoduNo);
            var getir = ekspres2017Entities.fatura.Where(x => x.EFaturaNo == null && x.takipseri == FuncClass.FArsivNO && x.alicisube == sube && x.iptal != "1").Select(x => new FaturaClass
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

        bool EarasivYazdir(fatura Fatura, IList<faturahar> FatHark)
        {
            try
            {

                Report report = new Report();

                report.RegisterData(FuncClass.ToDataTable(FatHark), "bilgi");
                report.Load(@"Earsiv.frx");
                (report.FindObject("Data1") as DataBand).DataSource = report.GetDataSource("bilgi");

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
                AciklamaTxt.Text = FuncClass.ibanNo.ToString() + " \r\nFaturayı Kesen Şube : " + Fatura.alicisube.ToString()+"\r\nYazı İle "+ 
                                   Tools.GetCurrencyText(Convert.ToDouble(Fatura.toplam), "TRY") + 
                                   "\r\nİş bu fatura muhteviyatına 7 gün içerisinde itiraz edilmediği taktirde aynen kabul edilmiş sayılır.";



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
            Gonder(3);

        }

        private void commandBarButton4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
