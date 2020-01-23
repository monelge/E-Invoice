using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

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

        public static DataTable GridViewToTable(RadGridView gridView)
        {
            DataTable dt = new DataTable();
            foreach (GridViewColumn col in gridView.Columns)
            {
                dt.Columns.Add(col.HeaderText);
            }

            foreach (GridViewRowInfo row in gridView.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (GridViewCellInfo cell in row.Cells)
                {
                    dRow[cell.ColumnInfo.Index] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            return dt;
        }

    }
}
