using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFaturaApp.Func;

namespace EFaturaApp
{
    public partial class LogKayitlari : BaseForm
    {
        public SQLiteConnection con = null;
        public LogKayitlari()
        {
            InitializeComponent();
        }
        void baglan()
        {
            con = new SQLiteConnection("Data Source=Logger.s3db;Version=3;");
            con.Open();
        }

        List<LogerModel> LogList()
        {
            baglan();
            List<LogerModel> logKaList = new List<LogerModel>();

            using (con)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = @"Select * from Log order by Timestamp desc";
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            LogerModel logerModel = new LogerModel();
                            logerModel.Timestamp = rdr.GetDateTime(0);
                            logerModel.Loglevel = rdr.GetString(1);
                            logerModel.Logger = rdr.GetString(2);
                            logerModel.Callsite = rdr.GetString(3);
                            logerModel.Message = rdr.GetString(4);
                            logKaList.Add(logerModel);

                        }
                        con.Close();
                    }
                }
            }

            return logKaList;
        }
        private void LogKayitlari_Load(object sender, EventArgs e)
        {
            radGridView1.DataSource = LogList();
            radGridView1.Refresh();
            radGridView1.BestFitColumns();
        }
    }
}
