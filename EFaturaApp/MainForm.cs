using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFaturaApp
{
    public partial class MainForm : BaseForm
    {
        public MainForm()
        {
            InitializeComponent();
            radDock1.AutoDetectMdiChildren = true;
            documentContainer1.SendToBack();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            TopluFatura frmTopluFatura = new TopluFatura();
            frmTopluFatura.MdiParent = this;
            frmTopluFatura.Show();
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            FaturaDurum faturaDurum = new FaturaDurum();
            faturaDurum.MdiParent = this;
            faturaDurum.Show();
        }
    }
}
