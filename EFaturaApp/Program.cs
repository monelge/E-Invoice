using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

namespace EFaturaApp
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (File.Exists(@"SystemCnTr.dll"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                RadMessageBox.Show("Gerekli Dosyalar Bulunamıyor. Lütfen Sistem Yöneticinizle Görüşünüz.", "Hata Oluştu");
                Application.Exit();
            }
        }
    }
}
