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
                    bool sonuc = EfatWebservis.FaturaIslem.FaturaXml(Fatura, FatLst, FtTur);
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

        private void commandBarButton2_Click(object sender, EventArgs e)
        {
            Gonder(3);
        }
    }
}
