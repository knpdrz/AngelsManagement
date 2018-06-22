using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelsManagement
{
    public static class Globals
    {
        public static string appDataFolderPath = Environment.GetFolderPath(
               Environment.SpecialFolder.CommonApplicationData) + "\\AngelsManagement";

        public static string dbFileName = "dbFile.db";
        public static string dbFilePath = appDataFolderPath + "\\" + dbFileName;
        public static string connectionString = "Data source=" + dbFilePath + ";Version=3";

    }
}
