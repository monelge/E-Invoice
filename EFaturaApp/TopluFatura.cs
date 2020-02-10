using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EFaturaApp.Func;
using EntFMSystem;
using Microsoft.VisualBasic;
using NLog;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace EFaturaApp
{
    public partial class TopluFatura : BaseForm
    {
        EKSPRES2017Entities dbEntities = new EKSPRES2017Entities();
        private Configuration configuration;
        private Logger logger;
        private int listedurum;
        private int iIsaretDurum;
        private int formDurum;

        public TopluFatura(int _formDurum)
        {
            InitializeComponent();
            this.formDurum = _formDurum;
            logger = LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();
        }

        void sayfaYukle()
        {
            int d = Convert.ToInt32(FuncClass.SubeKoduNo);
            var subeGon = dbEntities.krhatsub.OrderBy(x => x.subeismi).ToList();
            var subeAlici = dbEntities.krhatsub.OrderBy(x => x.subeismi).ToList();
            int subekodd = Convert.ToInt32(FuncClass.SubeKoduNo);

            var tahKoduaList = dbEntities.personel.Where(x => x.subekodu == subekodd && FuncClass.TahKoduLst.Contains(x.pkod)).ToList();

            
            radDropDownList5.DataSource = tahKoduaList;
            radDropDownList5.ValueMember = "tahkodu";
            radDropDownList5.DisplayMember = "tahkodu";
            radDropDownList5.SelectedIndex = -1;

            radDropDownList2.DataSource = subeGon;
            radDropDownList2.ValueMember = "sube";
            radDropDownList2.DisplayMember = "subeismi";

            radDropDownList3.DataSource = subeAlici;
            radDropDownList3.ValueMember = "sube";
            radDropDownList3.DisplayMember = "subeismi";
            int index = radDropDownList3.Items.IndexOf((dbEntities.krhatsub.FirstOrDefault(x => x.sube == d).subeismi
                .ToString())); //comboBox1.Items.IndexOf(a);
            radDropDownList3.SelectedIndex = index;

            radDateTimePicker1.Value = DateTime.Today.AddDays(-10);
            radDateTimePicker2.Value = DateTime.Today;
            radLabel6.Text = "";
            radLabel7.Text = "";
            radLabel8.Text = "";

            radGridView1.GridViewElement.Text = "";
            radGridView2.GridViewElement.Text = "";
            radDropDownList4.SelectedIndex = 3;
            if (formDurum == 0)
            {
                radGroupBox2.Visible = true;
                radGroupBox5.Visible = false;


            }

            if (formDurum == 1)
            {
                radGroupBox2.Visible = false;
                radGroupBox5.Location = new Point(219, 3);
            }


        }
        void Listeleme(int durum, int tesellum)
        {
            try
            {
                string sorgu = "";
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
                if (listedurum == 0)
                {
                    sorgu += "AND ISNULL(kr.EFatura,0)=1  ";
                }

                if (listedurum == 1)
                {
                    sorgu += "AND ISNULL(kr.EFatura,0)=0  ";
                }

                sorgu += "left JOIN irsaliye ir ON ir.irsno = t.tasirsno where ";


                #region peşin havle şubeye göre

                if (formDurum == 0)
                {
                    if (radDropDownList1.SelectedIndex != -1)
                    {
                        if (radDropDownList1.SelectedIndex == 1)
                        {
                            if (radDropDownList2.SelectedIndex != -1)
                            {
                                sorgu +=
                                    "t.basildimi=1 and ((t.fatno is null) or  (t.fatno='')) and (t.turu<>'M') and (t.tti='E') and ((t.iptal<>1)or (t.iptal is null) ) and (t.odemetipi<>'5') and ( (Kr.TopluFatura='0') or  (Kr.TopluFatura='') or (kr.toplufatura is null)) " +
                                    "and (kr.belgetipi<>'2') and (KR.belgetipi<>'4')  " +
                                    " and ((t.etf='H') or (isnull(t.etf,'')=''))  ";



                                sorgu += " and (t.gonderensube='" + radDropDownList2.SelectedValue.ToString() + "') ";
                                if (tesellum != 0)
                                {
                                    sorgu += " and (t.takipno='" + tesellum.ToString() + "') ";
                                }
                                sorgu += " and (t.odemetipi='1') " +
                                         " and (t.tarih between convert(datetime,'" + radDateTimePicker1.Text.ToString() +
                                         "',103) and convert(datetime,'" + radDateTimePicker2.Text.ToString() + "',103))" +
                                         "order by T.tadi,T.takipno";
                            }
                            else
                            {
                                RadMessageBox.Show("Lütfen Gönderen Şubeyi Seçiniz.", "Dikkat !!", MessageBoxButtons.OK,
                                    RadMessageIcon.Error);
                            }
                        }

                        if (radDropDownList1.SelectedIndex == 2)
                        {
                            if (radDropDownList2.SelectedIndex != -1)
                            {
                                sorgu +=
                                    "t.basildimi=1 and ((t.fatno is null) or  (t.fatno='')) and (t.turu<>'M') and (t.tti='E') and ((t.iptal<>1)or (t.iptal is null) ) and (t.odemetipi<>'5') and ( (Kr.TopluFatura='0') or  (Kr.TopluFatura='') or (kr.toplufatura is null)) " +
                                    "and (kr.belgetipi<>'2') and (KR.belgetipi<>'4')  " +
                                    " and ((t.etf='H') or (isnull(t.etf,'')=''))  ";
                                sorgu += " and (t.alicisube='" + radDropDownList3.SelectedValue.ToString() + "') ";
                                if (tesellum != 0)
                                {
                                    sorgu += " and (t.takipno='" + tesellum.ToString() + "') ";
                                }
                                sorgu += " and (t.odemetipi='2') " +
                                         " and (t.tarih between convert(datetime,'" + radDateTimePicker1.Text.ToString() +
                                         "',103) and convert(datetime,'" + radDateTimePicker2.Text.ToString() + "',103))" +
                                         "order by T.tadi,T.takipno";
                            }
                            else
                            {
                                RadMessageBox.Show("Lütfen Alıcı Şubeyi Seçiniz.", "Dikkat !!", MessageBoxButtons.OK,
                                    RadMessageIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        RadMessageBox.Show("Lütfen Ödeme Tipini Seçiniz.", "Dikkat !!", MessageBoxButtons.OK,
                            RadMessageIcon.Error);
                    }
                }


                #endregion

                #region Müşetiye Göre Arama

                 if (formDurum == 1)
                {

                    sorgu +=
                        "t.basildimi=1 and ((t.fatno is null) or  (t.fatno='')) and (t.turu<>'M') and (t.tti='E') and ((t.iptal<>1)or (t.iptal is null) ) and (t.odemetipi<>'5') and ( (Kr.TopluFatura='0') or  (Kr.TopluFatura='') or (kr.toplufatura is null)) " +
                        "and (kr.belgetipi<>'2') and (KR.belgetipi<>'4')  " +
                        " and ((t.etf='H') or (isnull(t.etf,'')=''))  ";

                    switch (radDropDownList4.SelectedIndex)
                    {
                        case 1:
                            sorgu += " and t.carikod='" + textBox1.Text + "'";
                            break;
                        case 2:
                            sorgu += " and t.carikod1='" + textBox1.Text + "'";
                            break;
                        case 3:
                            sorgu += " and t.tkodu='" + textBox1.Text + "'";
                            break;

                    }
                    sorgu+= " order by T.tadi,T.takipno";
                }

                #endregion

                radGridView1.DataSource = DataBaseSorgu.VeriIsle.data_table(sorgu.ToString());
                radLabel6.Text = "Listelenen Tesellüm Adedi : " + radGridView1.Rows.Count.ToString();

                double sum = 0;
                for (int i = 0; i < radGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToDouble((!radGridView1.Rows[i].Cells["yekun1"].Value.Equals(DBNull.Value))
                        ? radGridView1.Rows[i].Cells["yekun1"].Value
                        : "0");

                }

                radLabel7.Text = "Toplam Kesilecek Tes. Tutarı : " + sum.ToString("C");
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
            }
        }

        void Satirgetir(int satirno)
        {
            try
            {
                string sorguhar =
                    "select miktar,birimi,agirlik,carpan,cinsi,fiyati,tutari,musirsno,ref from tesellumhar where takipseri='" +
                    radGridView1.Rows[satirno].Cells["takipseri"].Value.ToString() + "' and fatno='" +
                    radGridView1.Rows[satirno].Cells["takipno"].Value.ToString() + "'";
                radGridView2.DataSource = DataBaseSorgu.VeriIsle.data_table(sorguhar.ToString());
                //  toolStripStatusLabel3.Text = radGridView1.CurrentRow.Cells["takipno"].Value.ToString();
                // toolStripStatusLabel4.Text = radGridView1.CurrentRow.Cells["adi"].Value.ToString();
                // toolStripStatusLabel5.Text = radGridView1.CurrentRow.Cells["adi1"].Value.ToString();
                // toolStripStatusLabel6.Text = radGridView1.CurrentRow.Cells["il"].Value.ToString();
            }
            catch (Exception)
            {
            }
        }
        void fatolustur()
        {
            try
            {

                int faturaref;
                DataTable dt = new DataTable();
                foreach (GridViewColumn col in radGridView1.Columns)
                {
                    dt.Columns.Add(col.HeaderText);
                }

                foreach (GridViewRowInfo row in radGridView1.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (GridViewCellInfo cell in row.Cells)
                    {
                        dRow[cell.ColumnInfo.Index] = cell.Value;
                    }
                    dt.Rows.Add(dRow);
                }

                int dtsay = dt.Rows.Count;

                DataTable veridt = dt;
                veridt.DefaultView.RowFilter = "isaret='True'";
                veridt.DefaultView.Sort = "tkodu";

                int veridtsay = veridt.Rows.Count;

                var qryLatestInterview = from rows in veridt.AsEnumerable()
                                         where rows.Field<string>("isaret") == "True"
                                         orderby rows["tkodu"] descending
                                         group rows by new { tkodu = rows["tkodu"] } into grp
                                         select grp.First();

                int qryLatestInterviewsay = qryLatestInterview.CopyToDataTable().Rows.Count;

                DataTable grupdt_ = qryLatestInterview.CopyToDataTable();
                for (int i = 0; i < grupdt_.Rows.Count; i++)
                {
                    DataTable tbl = (from DataRow dr in veridt.Rows
                                     where dr["isaret"].ToString() == "True" && dr["tkodu"].ToString() == grupdt_.Rows[i][10].ToString()
                                     select dr).CopyToDataTable();

                    faturaref = faturaekle(tbl);
                    int saymaca = 0;
                    for (int k = 0; k < tbl.Rows.Count; k++)
                    {
                        saymaca++;
                        faturaharekle(faturaref.ToString(), tbl.Rows[k]["takipseri"].ToString(), tbl.Rows[k]["takipno"].ToString());
                        tesellumisaret(faturaref.ToString(), tbl.Rows[k]["takipseri"].ToString(), tbl.Rows[k]["takipno"].ToString());
                        if (saymaca == 30)
                        {
                            bakiyehesapla(faturaref.ToString());
                            faturaref = faturaekle(tbl);
                            saymaca = 0;
                        }
                    }
                    bakiyehesapla(faturaref.ToString());
                }
                Application.DoEvents();
                System.Threading.Thread.Sleep(3000);

                RadMessageBox.Show("İşaretlenmiş Tüm Faturalar Kesildi.", "Bilgilendirme", MessageBoxButtons.OK, RadMessageIcon.Info);
                Listeleme(listedurum, 0);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                RadMessageBox.Show("Sistemsel bir hata oluştu. Lütfen Tekrar deneyiniz. \r\n" + ex.Message, "Hata Oluştu", MessageBoxButtons.OK, RadMessageIcon.Error);
                Listeleme(listedurum, 0);
            }
        }
        int faturaekle(DataTable fatDt)
        {
            string krKodu = fatDt.Rows[0]["tkodu"].ToString();
            var KrMuste = dbEntities.krmuste.FirstOrDefault(x => x.kodu == krKodu);
            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            string kull = (string)settingsReader.GetValue("kull", typeof(String));
            string iban = (string)settingsReader.GetValue("iban", typeof(String));

            int returnid;
            int odemetipi = Convert.ToInt32(fatDt.Rows[0]["odemetipi"].ToString());
            string takipseri = DataBaseSorgu.VeriIsle.data_table("select resmiserifat from personel where pkod='" + kull.ToString() + "'").Rows[0][0].ToString();
            string takipno = DataBaseSorgu.VeriIsle.data_table("select ensonfatno from personel where pkod='" + kull.ToString() + "'").Rows[0][0].ToString();
            DataTable musteriBilg = DataBaseSorgu.VeriIsle.data_table("select * from krmuste where kodu='" + fatDt.Rows[0]["tkodu"].ToString() + "'");

            SqlCommand cmd = new SqlCommand("INSERT INTO fatura(TakipNo, takipseri, kim, tarih, carikod, adi, alicisube, gonderensube, yekun, " +
                                            "toplam,toplamkdv,iskontob,tahkodu,odemetipi,tipi,belgetarihi,kdvdahil,vergidairesi,vergino,ackap," +
                                            "vadegun,vadetar,adres1,adres2,il,ilce,tel1,tel2,tahisaret,zimmettarih,islemtipi,KesilenEkran," +
                                            "DegisimTarih,Modul,kontrolaciklama,aciklama) VALUES (" +
                                            "@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20," +
                                            "@P21,@P22,@P23,@P24,@P25,@P26,@P27,@P28,@P29,@P30,@P31,@P32,@P33,@P34,@P35,@P36); SELECT CAST(scope_identity() AS int)", DataBaseSorgu.VeriIsle.local);

            cmd.Parameters.AddWithValue("@P1", Convert.ToInt32(takipno)).SqlDbType = SqlDbType.Int;
            cmd.Parameters.AddWithValue("@P2", takipseri).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P3", "EFATURA").SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P4", DateTime.Now.ToShortDateString()).SqlDbType = SqlDbType.DateTime;
            cmd.Parameters.AddWithValue("@P5", fatDt.Rows[0]["tkodu"].ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P6", fatDt.Rows[0]["tadi"].ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P7", musteriBilg.Rows[0][6].ToString()).SqlDbType = SqlDbType.Int;
            cmd.Parameters.AddWithValue("@P8", musteriBilg.Rows[0][6].ToString()).SqlDbType = SqlDbType.Int;
            cmd.Parameters.AddWithValue("@P9", fatDt.Rows[0]["yekun"].ToString()).SqlDbType = SqlDbType.Float;
            cmd.Parameters.AddWithValue("@P10", fatDt.Rows[0]["toplam"].ToString()).SqlDbType = SqlDbType.Float;
            cmd.Parameters.AddWithValue("@P11", fatDt.Rows[0]["toplamkdv"].ToString()).SqlDbType = SqlDbType.Float;
            cmd.Parameters.AddWithValue("@P12", "0").SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P13", KrMuste.tahkodu??"").SqlDbType = SqlDbType.VarChar;

            if (odemetipi == 1)
            {
                cmd.Parameters.AddWithValue("@P14", "1").SqlDbType = SqlDbType.VarChar;
            }

            if (odemetipi == 2)
            {
                cmd.Parameters.AddWithValue("@P14", "2").SqlDbType = SqlDbType.VarChar;
            }

            cmd.Parameters.AddWithValue("@P15", "G").SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P16", DateTime.Now.ToShortDateString()).SqlDbType = SqlDbType.DateTime;
            cmd.Parameters.AddWithValue("@P17", "Y").SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P18", musteriBilg.Rows[0][28].ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P19", musteriBilg.Rows[0][27].ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P20", "A").SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P21", "0").SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P22", DateTime.Now.ToShortDateString()).SqlDbType = SqlDbType.DateTime;
            cmd.Parameters.AddWithValue("@P23", musteriBilg.Rows[0][15].ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P24", musteriBilg.Rows[0][16].ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P25", musteriBilg.Rows[0][9].ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P26", musteriBilg.Rows[0][8].ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P27", musteriBilg.Rows[0][19].ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P28", musteriBilg.Rows[0][20].ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P29", "1").SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P30", DateTime.Now.ToShortDateString()).SqlDbType = SqlDbType.DateTime;
            cmd.Parameters.AddWithValue("@P31", "G").SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P32", "M").SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P33", DateTime.Now.ToShortDateString()).SqlDbType = SqlDbType.DateTime;
            cmd.Parameters.AddWithValue("@P34", "T").SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P35", iban.ToString()).SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.AddWithValue("@P36", radTextBox1.Text).SqlDbType = SqlDbType.VarChar;

            DataBaseSorgu.VeriIsle.local.Open();
            returnid = (int)cmd.ExecuteScalar();
            DataBaseSorgu.VeriIsle.local.Close();

            DataBaseSorgu.VeriIsle.ekle_duz_sil("update personel set ensonfatno=ensonfatno+1 where pkod='" + kull.ToString() + "'");

            return returnid;
        }
        void faturaharekle(string faturaref, string tseri, string tesno)
        {

            string fatno = DataBaseSorgu.VeriIsle.data_table("select takipno from fatura where ref='" + faturaref.ToString() + "'").Rows[0][0].ToString();
            string takipseri = DataBaseSorgu.VeriIsle.data_table("select takipseri from fatura where ref='" + faturaref.ToString() + "'").Rows[0][0].ToString();
            DataTable tesellum = DataBaseSorgu.VeriIsle.data_table("select * from tesellum where takipseri='" + tseri.ToString() + "' and takipno='" + tesno.ToString() + "'");
            DataTable tesellumhar = DataBaseSorgu.VeriIsle.data_table("select * from tesellumhar where takipseri='" + tseri.ToString() + "' and fatno='" + tesno.ToString() + "'");

            for (int i = 0; i < tesellumhar.Rows.Count; i++)
            {
                double tutari = 0;
                double fiyati = 0;

                if (!tesellumhar.Rows[i][12].Equals(DBNull.Value))
                {
                    tutari = Convert.ToDouble(tesellumhar.Rows[i][12].ToString());
                }
                if (!tesellumhar.Rows[i][11].Equals(DBNull.Value))
                {
                    fiyati = Convert.ToDouble(tesellumhar.Rows[i][11].ToString());
                }

                double agirlik = Convert.ToDouble((tesellumhar.Rows[i][6].ToString() != "") ? tesellumhar.Rows[i][6].ToString() : "0");
                double desi_kg = Convert.ToDouble((tesellumhar.Rows[i][10].ToString() != "") ? tesellumhar.Rows[i][6].ToString() : "0");

                int odemet = Convert.ToInt32(tesellum.Rows[0][60].ToString());
                string aciklama = "";
                if (odemet == 1)
                {
                    aciklama = tesellum.Rows[0][34].ToString().Trim().ToString();//aciklama
                }

                if (odemet == 2)
                {
                    aciklama = tesellum.Rows[0][31].ToString().Trim().ToString();//aciklama
                }

                string listeno = tesellum.Rows[0][70].ToString();
                string listeno2 = "";

                if (listeno.Length > 17)
                {
                    listeno2 = (listeno != "") ? listeno.ToString().Trim().Substring(12) : "";
                }
                else
                {
                    listeno2 = listeno;
                }

                int carpan = 0;
                if (!tesellumhar.Rows[i][14].Equals(DBNull.Value))
                {
                    carpan = Convert.ToInt32(tesellumhar.Rows[i][14].ToString().Trim().ToString());
                }

                string musirsno = "";
                if (!tesellumhar.Rows[i][15].Equals(DBNull.Value))
                {
                    musirsno = tesellumhar.Rows[i][15].ToString().Trim().ToString();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO faturahar (fatno,takipseri,cinsi,miktar,birimi,olcu,agirlik,desi_kg,tutari,fiyati," +
                                                "aciklama,tesno1,tesno2,tesseri,carpan,CarpanDesi,FiyatFlag,tesmusirsno,testasirsno) VALUES (" +
                                                "@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19);" +
                                                "SELECT CAST(scope_identity() AS int)", DataBaseSorgu.VeriIsle.local);

                cmd.Parameters.AddWithValue("@P1", Convert.ToInt32(fatno)).SqlDbType = SqlDbType.Int;//fatno
                cmd.Parameters.AddWithValue("@P2", takipseri).SqlDbType = SqlDbType.VarChar;//takipseri
                cmd.Parameters.AddWithValue("@P3", tesellumhar.Rows[i][2].ToString()).SqlDbType = SqlDbType.VarChar;//cinsi
                cmd.Parameters.AddWithValue("@P4", Convert.ToInt32(tesellumhar.Rows[i][3].ToString())).SqlDbType = SqlDbType.Real;//miktari
                cmd.Parameters.AddWithValue("@P5", tesellumhar.Rows[i][4].ToString().Trim().ToString()).SqlDbType = SqlDbType.VarChar;//birim
                cmd.Parameters.AddWithValue("@P6", 0).SqlDbType = SqlDbType.VarChar;//olcu
                cmd.Parameters.AddWithValue("@P7", agirlik).SqlDbType = SqlDbType.Float;//agirlik
                cmd.Parameters.AddWithValue("@P8", desi_kg).SqlDbType = SqlDbType.Float;//desi_kg
                cmd.Parameters.AddWithValue("@P9", tutari).SqlDbType = SqlDbType.Decimal;//tutari
                cmd.Parameters.AddWithValue("@P10", fiyati).SqlDbType = SqlDbType.Decimal;//fiyati
                cmd.Parameters.AddWithValue("@P11", aciklama.ToString()).SqlDbType = SqlDbType.VarChar;//aciklama
                cmd.Parameters.AddWithValue("@P12", tesellum.Rows[0][1].ToString().Trim().ToString()).SqlDbType = SqlDbType.Int;//tesno1
                cmd.Parameters.AddWithValue("@P13", listeno2.ToString()).SqlDbType = SqlDbType.VarChar;//tesno2
                cmd.Parameters.AddWithValue("@P14", tesellum.Rows[0][2].ToString().Trim().ToString()).SqlDbType = SqlDbType.VarChar;//tesseri
                cmd.Parameters.AddWithValue("@P15", carpan).SqlDbType = SqlDbType.Int;//çarpan
                cmd.Parameters.AddWithValue("@P16", "0").SqlDbType = SqlDbType.VarChar;//carpandesi
                cmd.Parameters.AddWithValue("@P17", 1).SqlDbType = SqlDbType.Int;//fiyatflag
                cmd.Parameters.AddWithValue("@P18", musirsno.ToString()).SqlDbType = SqlDbType.VarChar;//tesmusirsno
                cmd.Parameters.AddWithValue("@P19", "").SqlDbType = SqlDbType.VarChar;//testasirsno

                DataBaseSorgu.VeriIsle.local.Open();
                int returnid = (int)cmd.ExecuteScalar();
                DataBaseSorgu.VeriIsle.local.Close();

            }
        }
        void bakiyehesapla(string fatref)
        {
            DataTable faturalar = DataBaseSorgu.VeriIsle.data_table("select takipseri,takipno from fatura where ref='" + fatref.ToString() + "'");
            DataBaseSorgu.VeriIsle.ekle_duz_sil("update fatura set yekun=(select sum(tutari) from faturahar where fatno='" + faturalar.Rows[0][1].ToString() + "' and takipseri='" + faturalar.Rows[0][0].ToString() + "'), " +
                                                "toplam=(select sum(tutari) from faturahar where fatno='" + faturalar.Rows[0][1].ToString() + "' and takipseri='" + faturalar.Rows[0][0].ToString() + "')*1.18, " +
                                                "toplamkdv=(select sum(tutari) from faturahar where fatno='" + faturalar.Rows[0][1].ToString() + "' and takipseri='" + faturalar.Rows[0][0].ToString() + "')*0.18 " +
                                                "where ref='" + fatref.ToString() + "'");
        }
        void tesellumisaret(string fatref, string tseri, string tesno)
        {
            DataTable faturalar = DataBaseSorgu.VeriIsle.data_table("select takipseri,takipno from fatura where ref='" + fatref.ToString() + "'");
            DataBaseSorgu.VeriIsle.ekle_duz_sil("UPDATE tesellum SET kim='EFATURA'," +
                                                "fatseri='" + faturalar.Rows[0][0].ToString() + "'," +
                                                "fatno='" + faturalar.Rows[0][1].ToString() + "'," +
                                                "DegisimTarih=convert(datetime,'" + DateTime.Now.ToShortDateString() + "',103) " +
                                                "WHERE takipno='" + tesno.ToString() + "' AND takipseri='" + tseri.ToString() + "'");
        }

        private void TopluFatura_Load(object sender, EventArgs e)
        {
            sayfaYukle();
        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            try
            {
                listedurum = 0;
                Listeleme(listedurum, 0);
                this.radGridView1.Columns[1].BestFit();
                this.radGridView1.Columns[2].BestFit();
                this.radGridView1.Columns[3].BestFit();
                this.radGridView1.Columns[4].BestFit();
                this.radGridView1.Columns[5].BestFit();
                this.radGridView1.Columns[7].BestFit();
                this.radGridView1.Columns[9].BestFit();

            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
            }

        }
        private void commandBarButton2_Click(object sender, EventArgs e)
        {
            if (RadMessageBox.Show("Seçili Faturaları Kesmek istediğinize emin misiniz ?\n\n", "Bilgilendirme", MessageBoxButtons.YesNo, RadMessageIcon.Question) == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;
                this.radGridView1.Enabled = false;
                this.radGridView2.Enabled = false;
                radDateTimePicker1.Focus();
                radDateTimePicker1.Select();
                fatolustur();
                Listeleme(listedurum, 0);
                this.radGridView1.Enabled = true;
                this.radGridView2.Enabled = true;
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }
        private void radGridView1_CellClick(object sender, GridViewCellEventArgs e)
        {
            Satirgetir(e.RowIndex);
        }
        private void radGridView1_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            try
            {


                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                double tutar = (double)settingsReader.GetValue("tutar", typeof(double));

                bool tt = (bool)radGridView1.CurrentRow.Cells[0].Value;
                int veri = Convert.ToInt16(tt);

                string ve = radGridView1.CurrentRow.Cells["Yekun1"].Value.ToString();
                if (ve != "" && Convert.ToDouble(ve) != tutar)
                {
                    DataBaseSorgu.VeriIsle.ekle_duz_sil("UPDATE tesellum SET fatisaret='" + veri + "' WHERE takipno='" +
                                                        radGridView1.CurrentRow.Cells["takipno"].Value.ToString() +
                                                        "' AND takipseri='" +
                                                        radGridView1.CurrentRow.Cells["takipseri"].Value.ToString() + "'");

                }
                else
                {
                    radGridView1.CurrentRow.Cells["isaret"].Value = false;
                }
            }
            catch (Exception exception)
            {
                logger.Error(exception.Message);
            }
        }
        private void radGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            bool tt = (bool)e.RowElement.RowInfo.Cells[0].Value;
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
        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int veri = radGridView1.CurrentRow.Index;
                Satirgetir(veri);
            }
            catch (Exception)
            {

            }
        }

        private void commandBarButton3_Click(object sender, EventArgs e)
        {
            int iTotal = radGridView1.Rows.Count;
            switch (iIsaretDurum)
            {
                case 0:
                    for (int i = 0; i < iTotal; i++)
                    {
                        try
                        {
                            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                            double tutar = (double)settingsReader.GetValue("tutar", typeof(double));

                            bool tt = (bool)radGridView1.Rows[i].Cells[0].Value;
                            int veri = Convert.ToInt16(tt);

                            string ve = radGridView1.Rows[i].Cells["Yekun1"].Value.ToString();
                            if (ve != "" && Convert.ToDouble(ve) != tutar)
                            {
                                DataBaseSorgu.VeriIsle.ekle_duz_sil("UPDATE tesellum SET fatisaret='1' WHERE takipno='" + radGridView1.Rows[i].Cells["takipno"].Value.ToString() + "' AND takipseri='" + radGridView1.Rows[i].Cells["takipseri"].Value.ToString() + "'");
                            }
                            else
                            {
                                radGridView1.Rows[i].Cells["isaret"].Value = false;
                            }

                        }
                        catch (Exception exception)
                        {
                            logger.Error(exception.Message);
                        }
                    }
                    Listeleme(listedurum, 0);
                    radGridView1.Refresh();
                    commandBarButton3.Text = "Tümünü Kaldır";
                    iIsaretDurum = 1;
                    break;
                case 1:
                    for (int i = 0; i < iTotal; i++)
                    {
                        try
                        {
                            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                            double tutar = (double)settingsReader.GetValue("tutar", typeof(double));

                            bool tt = (bool)radGridView1.Rows[i].Cells[0].Value;
                            int veri = Convert.ToInt16(tt);

                            string ve = radGridView1.Rows[i].Cells["Yekun1"].Value.ToString();
                            if (ve != "" && Convert.ToDouble(ve) != tutar)
                            {
                                DataBaseSorgu.VeriIsle.ekle_duz_sil("UPDATE tesellum SET fatisaret='0' WHERE takipno='" + radGridView1.Rows[i].Cells["takipno"].Value.ToString() + "' AND takipseri='" + radGridView1.Rows[i].Cells["takipseri"].Value.ToString() + "'");
                            }
                            else
                            {
                                radGridView1.Rows[i].Cells["isaret"].Value = false;
                            }

                        }
                        catch (Exception exception)
                        {
                            logger.Error(exception.Message);
                        }
                    }
                    Listeleme(listedurum, 0);
                    radGridView1.Refresh();
                    commandBarButton3.Text = "Tümünü Işaretle";
                    iIsaretDurum = 0;
                    break;

            }

        }
        private void commandBarButton4_Click(object sender, EventArgs e)
        {
            var getir = Interaction.InputBox("Tesellüm No : ", "Tesellüme Göre Ara");
            listedurum = 0;
            Listeleme(0, Convert.ToInt32(getir));
        }

        private void radGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                TeslimFisi frmtesres = new TeslimFisi(radGridView1.CurrentRow.Cells["takipseri"].Value.ToString(),
                    radGridView1.CurrentRow.Cells["takipno"].Value.ToString());
                frmtesres.ShowDialog();
            }
        }

        private void radGridView2_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            string veri = "";
            int carpan = 0;
            try
            {
                veri = radGridView2.Rows[e.RowIndex].Cells[5].Value.ToString().Replace(",", ".");
                carpan = Convert.ToInt16(radGridView2.Rows[e.RowIndex].Cells[3].Value);
            }
            catch (Exception)
            {

            }

            if (veri != "")
            {
                if (carpan == 1)
                {
                    radGridView2.Rows[e.RowIndex].Cells[6].Value = (Convert.ToDouble(radGridView2.Rows[e.RowIndex].Cells[2].Value) * Convert.ToDouble(radGridView2.Rows[e.RowIndex].Cells[5].Value)).ToString();

                }
                else
                {
                    radGridView2.Rows[e.RowIndex].Cells[6].Value = (Convert.ToDouble(radGridView2.Rows[e.RowIndex].Cells[0].Value) * Convert.ToDouble(radGridView2.Rows[e.RowIndex].Cells[5].Value)).ToString();
                }
            }
            else
            {
                radGridView2.Rows[e.RowIndex].Cells[5].Value = DBNull.Value;
            }

            DataBaseSorgu.VeriIsle.ekle_duz_sil("UPDATE tesellumhar SET " +
                                                "cinsi = '" + radGridView2.Rows[e.RowIndex].Cells[4].Value + "', " +
                                                "miktar = '" + radGridView2.Rows[e.RowIndex].Cells[0].Value + "', " +
                                                "birimi = '" + radGridView2.Rows[e.RowIndex].Cells[1].Value + "'," +
                                                "agirlik = '" + radGridView2.Rows[e.RowIndex].Cells[2].Value + "'," +
                                                "tutari = (CASE WHEN '" + radGridView2.Rows[e.RowIndex].Cells[6].Value.ToString().Replace(",", ".") + "' <> '' THEN '" + radGridView2.Rows[e.RowIndex].Cells[6].Value.ToString().Replace(",", ".") + "' ELSE null END)," +
                                                "fiyati = (CASE WHEN '" + radGridView2.Rows[e.RowIndex].Cells[5].Value.ToString().Replace(",", ".") + "' <> '' THEN '" + radGridView2.Rows[e.RowIndex].Cells[5].Value.ToString().Replace(",", ".") + "' ELSE null END)," +
                                                "carpan = '" + carpan.ToString() + "'," +
                                                "musirsno = '" + radGridView2.Rows[e.RowIndex].Cells[7].Value + "' " +
                                                "WHERE ref='" + radGridView2.Rows[e.RowIndex].Cells[8].Value + "' ");

            double sum = 0;
            for (int i = 0; i < radGridView2.Rows.Count; ++i)
            {
                sum += Convert.ToDouble((!radGridView2.Rows[i].Cells[6].Value.Equals(DBNull.Value)) ? radGridView2.Rows[i].Cells[6].Value : "0");

            }

            double sumtop = sum * 1.18;
            double sumkdv = sum * 0.18;
            try
            {
                DataBaseSorgu.VeriIsle.ekle_duz_sil("update tesellum set yekun='" + sum.ToString("##.###").Replace(",", ".") + "'," +
                                                    "toplam='" + sumtop.ToString("##.###").Replace(",", ".") + "'," +
                                                    "toplamkdv='" + sumkdv.ToString("##.###").Replace(",", ".") + "' " +
                                                    "where takipseri='" + radGridView2.CurrentRow.Cells["takipseri"].Value.ToString() + "' and " +
                                                    "takipno='" + radGridView2.CurrentRow.Cells["takipno"].Value.ToString() + "'");

            }
            catch (Exception)
            {
                DataBaseSorgu.VeriIsle.local.Close();
            }
        }

        private void commandBarButton5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MusteriListesi musteriListesi =
                    new MusteriListesi(this, textBox2.Text, "textBox1", "textBox2");
                musteriListesi.ShowDialog();
            }
        }
    }
}
