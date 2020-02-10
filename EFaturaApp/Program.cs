using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFaturaApp.Func;
using NLog;
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
                Logger log = LogManager.GetCurrentClassLogger();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                LoggerClass.logger.Warn("Program Acilisi");
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
