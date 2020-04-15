using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseSorgu;
using EFaturaApp.Func;
using EfatWebservis;
using EntFMSystem;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace EFaturaApp
{
    public partial class FaturaGonder : BaseForm
    {
        EKSPRES2017Entities ekspres2017Entities = new EKSPRES2017Entities();
        BackgroundWorker worker = new BackgroundWorker();

        public FaturaGonder()
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
            Gonder(1);
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
                    string FaturaNO = Fatura.takipseri+ Fatura.TakipNo.ToString().PadLeft(9, '0');
                    this.Invoke(new Action(() =>
                    {
                        radProgressBar1.Maximum = dataTable.Rows.Count;
                        radProgressBar1.Minimum = 0;
                        radLabel1.Text = "Gönderilen Fatura No : " + FaturaNO;
                        radProgressBar1.Value1 = i+1;
                        radProgressBar1.Text = "Toplam :" + i + " / " + (dataTable.Rows.Count);
                    }));
                    string sonuc = EfatWebservis.FaturaIslem.FaturaXml(Fatura, FatLst, FtTur);
                    if (sonuc.Length > 2)
                    {
                        RadMessageBox.Show(sonuc, "Hata Oluştu", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }

                this.Invoke(new Action(() =>
                {
                    listele();
                    radPanel1.Visible = false;
                    RadMessageBox.Show("Faturalar başarılı şekilde gönderildi", "Tebrikler", MessageBoxButtons.OK, RadMessageIcon.Info);
                }));
                listele();
               // radPanel1.Invoke(new Action(() => { radPanel1.Visible = false; }));
                //RadMessageBox.Show("Faturalar başarılı şekilde gönderildi", "Tebrikler", MessageBoxButtons.OK,RadMessageIcon.Info);
                return true;
            }
            catch (Exception e)
            {
                LoggerClass.logger.Error(e.Message);
                return false;
            }
        }

        bool listele()
        {
            int sube = Convert.ToInt32(FuncClass.SubeKoduNo);
            var getir = ekspres2017Entities.fatura.Where(x => x.adi1 == null && (x.takipseri == FuncClass.FSeriNO || x.takipseri == FuncClass.FSerbestSeriNO) && x.iptal != "1" && FuncClass.SubeKoduNoLst.Contains(x.gonderensube ?? 0)).Select(x => new FaturaClass 
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
        private void FaturaGonder_Load(object sender, EventArgs e)
        {
            radPanel1.Visible = false;
        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            listele();
            EfatWebservis.FaturaIslem.folderkontrol();
        }


        private void commandBarButton3_Click(object sender, EventArgs e)
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
                LoggerClass.logger.Error(exception.Message);
            }

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

        private void commandBarButton2_Click(object sender, EventArgs e)
        {
           worker.RunWorkerAsync();
           

        }

        private void temelFaturaGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gonder(0);
            RadMessageBox.Show("Faturalar başarılı şekilde gönderildi", "Tebrikler", MessageBoxButtons.OK,
                RadMessageIcon.Info);
        }

        private void ticariFataruGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gonder(1);
            RadMessageBox.Show("Faturalar başarılı şekilde gönderildi", "Tebrikler", MessageBoxButtons.OK,
                RadMessageIcon.Info);
        }

        private void commandBarButton4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void önİzlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sTakipSeri = dataGridView1.CurrentRow.Cells["takipseri"].Value.ToString();
            int iTakipNO =Convert.ToInt32(dataGridView1.CurrentRow.Cells["TakipNo"].Value.ToString());
            var FtrDty = ekspres2017Entities.fatura.FirstOrDefault(f => f.takipseri == sTakipSeri && f.TakipNo == iTakipNO);
            var FtrFryList = ekspres2017Entities.faturahar
                .Where(h => h.takipseri == FtrDty.takipseri && h.fatno == FtrDty.TakipNo).ToList();
            string xml = FaturaIslem.FaturaXmlString(FtrDty, FtrFryList, 1);
            string template = File.ReadAllText(@"GeneralFormFatura.xslt");
            string html = FuncClass.ConvertToHtml(template, xml);
            OnIzleme onIzleme = new OnIzleme(html);
            onIzleme.ShowDialog();
        }

        private void kamuFaturaGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gonder(3);
            RadMessageBox.Show("Faturalar başarılı şekilde gönderildi", "Tebrikler", MessageBoxButtons.OK,
                RadMessageIcon.Info);
        }
    }
}
