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
using NLog;
using NLog.Fluent;

namespace EFaturaApp
{
    public partial class TopluFatura : BaseForm
    {
        EKSPRES2017Entities dbEntities = new EKSPRES2017Entities();
        private Configuration configuration;
        Logger logger = LogManager.GetCurrentClassLogger();

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

        string SqlQuery()
        {
            try
            {
                string sorgu = "";

                #region baslikSorgusu
                sorgu = "select cast(ISNULL(t.fatisaret,0) as bit) as isaret," +
                    "tasirsno1  =cast(substring(tasirsno,13,10) as int) ,t.tarih,t.takipno," +
                    "miktar =(select sum(h.miktar) from tesellumhar H where H.fatno=t.takipno AND h.takipseri=t.takipseri),    " +
                    "kg =(select sum(h.agirlik) from tesellumhar H where H.fatno=t.takipno AND h.takipseri=t.takipseri), " +
                    "t.carikod,t.adi,t.carikod1,t.adi1,t.tkodu,t.tadi, " +
                    "Yekun1 =(select sum(Tutari) + SUM(ISNULL(H.DereceTutar,0)) from tesellumhar H where H.fatno=t.takipno AND h.takipseri=t.takipseri), " +
                    "Yekun2 =(SELECT SUM(CASE WHEN ISNULL(H.tutari1,0)<>0 THEN H.tutari1 ELSE H.tutari END) FROM tesellumhar H  " +
                    "         WHERE H.fatno=t.takipno AND h.takipseri=t.takipseri  " +
                    "         AND EXISTS(SELECT 1 FROM tesellumhar HH WHERE HH.fatno=H.fatno AND HH.takipseri=H.takipseri AND ISNULL(HH.tutari1,0)<>0)),  " +
                    "ilce=(select top 1 kr.ilce from krmuste kr where kr.kodu=t.carikod1), " +
                    "il=(select top 1 kr.il from krmuste kr where kr.kodu=t.carikod1), " +
                    "asube=(select top 1 kh.subeismi from krhatsub KH where kh.sube=t.alicisube), " +
                    "musirsno =(select top 1 h.musirsno from tesellumhar H where h.fatno=t.takipno AND h.takipseri=t.takipseri), " +
                    "aracirsno =(select top 1 h.tasirsno from tesellumhar H where h.fatno=t.takipno AND h.takipseri=t.takipseri), " +
                    "ir.plaka,ir.kimbosalt,kr.belgetipi ,t.ref,t.alicisube,t.varissube,t.tasirsno, t.fatseri, " +
                    "t.gonderensube,t.alisaret,t.gonisaret,t.fatno, t.fatisaret,t.MalGetirenKod,t.MalalanKod,t.MalAlanAdi,t.MalGetirenAdi,t.emanetno,t.GGDegisiklik, " +
                    "t.takipseri,t.yekun,t.toplamkdv,t.toplam, t.odemetipi," +
                    "HarTop =(select count(*) from tesellumhar H where H.fatno=t.takipno AND h.takipseri=t.takipseri), " +
                    "KaraListe=ISNULL((SELECT TOP 1 ISNULL(tt.Fatura,0) FROM tblTipTanim tt WHERE tt.Durum=1 AND tt.Tip=1 AND tt.Value=kr.krliste),1), " +
                    "KaraListeAciklamaG=(SELECT TOP 1 tt.Aciklama FROM tblTipTanim tt WHERE tt.Durum=1 AND tt.Tip=1 AND tt.Value=kr.krliste), " +
                    "EmanetToplam=(select top 1 toplam from emanetfatura e where e.EmanetID=t.EmanetID and isnull(e.eno,'')<>'' )" +
                    "from tesellum t  " +
                    "INNER JOIN krmuste kr ON kr.kodu = t.tkodu ";
                #endregion

                sorgu += "AND ISNULL(kr.EFatura,0)=1  ";
                sorgu += "left JOIN irsaliye ir ON ir.irsno = t.tasirsno where ";

                if (radDropDownList1.SelectedIndex == 1)
                {
                    if (radDropDownList2.SelectedIndex != -1)
                    {
                        sorgu += "t.basildimi=1 and ((t.fatno is null) or  (t.fatno='')) and (t.turu<>'M') and (t.tti='E') and ((t.iptal<>1)or (t.iptal is null) ) and (t.odemetipi<>'5') and ( (Kr.TopluFatura='0') or  (Kr.TopluFatura='') or (kr.toplufatura is null)) " +
                            "and (kr.belgetipi<>'2') and (KR.belgetipi<>'4')  " +
                            " and ((t.etf='H') or (isnull(t.etf,'')=''))  ";

                        sorgu += " and (t.odemetipi='1') " +
                            " and (t.tarih between convert(datetime,'" + radDateTimePicker1.Text.ToString() + "',103) and convert(datetime,'" + radDateTimePicker2.Text.ToString() + "',103))" +
                            "order by T.tadi,T.takipno";
                        //	dataGridView1.DataSource = DataBaseSorgu.VeriIsle.data_table(sorgu.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Lütfen Gönderen Şubeyi Seçiniz.", "Dikkat !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (radDropDownList1.SelectedIndex == 2)
                {
                    if (radDropDownList3.SelectedIndex != -1)
                    {
                        sorgu += "t.basildimi=1 and ((t.fatno is null) or  (t.fatno='')) and (t.turu<>'M') and (t.tti='E') and ((t.iptal<>1)or (t.iptal is null) ) and (t.odemetipi<>'5') and ( (Kr.TopluFatura='0') or  (Kr.TopluFatura='') or (kr.toplufatura is null)) " +
                            "and (kr.belgetipi<>'2') and (KR.belgetipi<>'4')  " +
                            " and ((t.etf='H') or (isnull(t.etf,'')=''))  ";


                        sorgu += " and (t.odemetipi='2') " +
                            " and (t.tarih between convert(datetime,'" + radDateTimePicker1.Text.ToString() + "',103) and convert(datetime,'" + radDateTimePicker2.Text.ToString() + "',103))" +
                            "order by T.tadi,T.takipno";

                    }
                    else
                    {
                        MessageBox.Show("Lütfen Alıcı Şubeyi Seçiniz.", "Dikkat !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                return sorgu;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }

        }
        private void TopluFatura_Load(object sender, EventArgs e)
        {
            sayfaYukle();

        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var s = SqlQuery();
                var veriLst = dbEntities.Database.SqlQuery<TesellumClass>(SqlQuery()).ToList();
                radGridView1.DataSource = veriLst;
                radGridView1.Refresh();
            }
            catch (Exception exception)
            {
               logger.Error(exception);
            }

        }
    }
}
