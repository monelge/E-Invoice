using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFaturaApp.Func
{
    class FaturaClass
    {
        public int @ref { get; set; }
        public int TakipNo { get; set; }
        public string takipseri { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public string carikod { get; set; }
        public string adi { get; set; }
        public Nullable<int> alicisube { get; set; }
        public Nullable<decimal> yekun { get; set; }
        public Nullable<decimal> toplam { get; set; }
        public Nullable<decimal> toplamkdv { get; set; }
        public bool isaret { get; set; }
    }
}
