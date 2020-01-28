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
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace EFaturaApp
{
    public partial class FaturaGonder : BaseForm
    {
        EKSPRES2017Entities ekspres2017Entities = new EKSPRES2017Entities();
        public FaturaGonder()
        {
            InitializeComponent();
        }

        bool listele()
        {
            int sube = Convert.ToInt32(FuncClass.SubeKoduNo);
            var getir = ekspres2017Entities.fatura.Where(x => x.EFaturaNo == null && x.takipseri == "AEK2020" && x.alicisube == sube).Select(x => new
            {
                x.isaret,
                x.takipseri,
                x.TakipNo,
                x.tarih,
                x.carikod,
                x.adi,
                x.yekun,
                x.toplamkdv,
                x.toplam,
                x.alicisube,
                x.@ref
            }).ToList();
            radGridView1.DataSource = getir;
            radGridView1.Refresh();
            return true;

        }
        private void FaturaGonder_Load(object sender, EventArgs e)
        {

        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void radGridView1_CellValueChanged(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {

                if (e.Column.Name == "isaret")
                {
                    var d = e.Value;
                }



                bool tt = false;
                try
                {
                    tt = (bool)(radGridView1.CurrentRow.Cells[0].Value.Equals("1") ? true : false);
                }
                catch (Exception exception)
                {
                    tt = false;
                }
                int refValue = (int)radGridView1.CurrentRow.Cells[10].Value;

                var ftIsaret =
                    ekspres2017Entities.fatura.FirstOrDefault(x => x.@ref == refValue);
                if (tt == true)
                {
                    ftIsaret.isaret = "1";
                }
                else
                {
                    ftIsaret.isaret = "0";
                }
                ekspres2017Entities.SaveChanges();
            }
            catch (Exception exception)
            {
                LoggerClass.ERROR = exception.Message;
            }
        }

        private void commandBarButton3_Click(object sender, EventArgs e)
        {

        }

        private void radGridView1_RowFormatting(object sender, Telerik.WinControls.UI.RowFormattingEventArgs e)
        {
            bool tt = false;
            //!string.IsNullOrEmpty(
            try
            {
                tt = (bool)(e.RowElement.RowInfo.Cells[0].Value.Equals("1") ? true : false);
            }
            catch (Exception exception)
            {
                tt = false;
            }

            if (tt == true)
            {
                e.RowElement.DrawFill = true;
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = Color.Red;
            }

            if (tt == false)
            {
                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
            }
        }

        private void radGridView1_CurrentCellChanged(object sender, CurrentCellChangedEventArgs e)
        {
           

        }

        private void radGridView1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void radGridView1_Leave(object sender, EventArgs e)
        {
           
        }

        private void radGridView1_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            
        }

        private void radGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            
        }
    }
}
