using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFaturaApp.EntFM;
using EFaturaApp.Func;

namespace EFaturaApp
{
    public partial class TopluFatura : BaseForm
    {
        EKSPRES2017Entities dbEntities = new EKSPRES2017Entities();
        private Configuration configuration;
        public TopluFatura()
        {
            InitializeComponent();
        }

        void sayfaYukle()
        {
            int d = Convert.ToInt32(FuncClass.SubeKoduNo);
            var subeGon = dbEntities.krhatsub.OrderBy(x => x.subeismi).ToList();
            var subeAlici = dbEntities.krhatsub.OrderBy(x => x.subeismi).ToList();

            radDropDownList2.DataSource = subeGon;
            radDropDownList2.ValueMember = "sube";
            radDropDownList2.DisplayMember = "subeismi";

            radDropDownList3.DataSource = subeAlici;
            radDropDownList3.ValueMember = "sube";
            radDropDownList3.DisplayMember = "subeismi";
            int index = radDropDownList3.Items.IndexOf((dbEntities.krhatsub.FirstOrDefault(x => x.sube == d).subeismi.ToString()));//comboBox1.Items.IndexOf(a);
            radDropDownList3.SelectedIndex = index;

            radDateTimePicker1.Value = DateTime.Today.AddDays(-10);
            radDateTimePicker2.Value = DateTime.Today;
        }
        private void TopluFatura_Load(object sender, EventArgs e)
        {
            sayfaYukle();
        }
    }
}
