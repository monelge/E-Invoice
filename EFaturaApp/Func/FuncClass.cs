using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace EFaturaApp.Func
{
    public static class FuncClass
    {
        static AppSettingsReader settingsReader = new AppSettingsReader();

      
        public static string ibanNo
        {
            get
            {
                string skodukull = (string)settingsReader.GetValue("iban", typeof(String));
                return skodukull;
            }

        }

        private static string FSeri;
        public static string FSeriNO
        {
            get
            {
                string skodukull = (string)settingsReader.GetValue("fSeri", typeof(String));
                return skodukull;
            }

        }

        private static string FSerbestSeri;
        public static string FSerbestSeriNO
        {
            get
            {
                string skodukull = (string)settingsReader.GetValue("fSerbet", typeof(String));
                return skodukull;
            }

        }
        private static string FArsiv;
        public static string FArsivNO
        {
            get
            {
                string skodukull = (string)settingsReader.GetValue("fArsiv", typeof(String));
                return skodukull;
            }

        }
        private static string SubeKodu;
        public static string SubeKoduNo
        {
            get
            {
                string skodukull = (string)settingsReader.GetValue("sube", typeof(String));
                return skodukull;
            }

        }
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
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
