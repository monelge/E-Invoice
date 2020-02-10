using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFMSystem
{
  public  partial class ConvFaturaHar
    {

        public int @ref { get; set; }
        public Nullable<int> fatno { get; set; }
        public string takipseri { get; set; }
        public string cinsi { get; set; }
        public Nullable<float> miktar { get; set; }
        public string birimi { get; set; }
        public string olcu { get; set; }
        public Nullable<double> agirlik { get; set; }
        public Nullable<int> en { get; set; }
        public Nullable<int> boy { get; set; }
        public Nullable<int> yukseklik { get; set; }
        public Nullable<double> desi_kg { get; set; }
        public Nullable<decimal> tutari { get; set; }
        public Nullable<decimal> fiyati { get; set; }
        public string aciklama { get; set; }
        public Nullable<int> tesno1 { get; set; }
        public string tesno2 { get; set; }
        public string isaret { get; set; }
        public string tesseri { get; set; }
        public Nullable<int> fatisaret { get; set; }
        public Nullable<int> carpan { get; set; }
        public string CarpanDesi { get; set; }
        public Nullable<int> FiyatFlag { get; set; }
        public Nullable<decimal> Tutari1 { get; set; }
        public Nullable<decimal> Fiyati1 { get; set; }
        public string AktarimRef { get; set; }
        public string tesmusirsno { get; set; }
        public string testasirsno { get; set; }
        public Nullable<short> HarTipi { get; set; }
    }
}
