using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFaturaApp.Func
{
    public static class FuncClass
    {
        static AppSettingsReader settingsReader = new AppSettingsReader();
        private static string SubeKodu;

        public static string SubeKoduNo
        {
            get
            {
                string skodukull = (string)settingsReader.GetValue("sube", typeof(String));
                return skodukull;
            }

        }

    }
}
