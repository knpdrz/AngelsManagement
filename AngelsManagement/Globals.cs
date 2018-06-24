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
        public static string connectionString = "Data source=" + dbFilePath;

        public static Dictionary<String, String> VolunteersColumnNamesBindings = 
            new Dictionary<string, string>
        {
            { "id", "VolunteerId" },
            { "Imię", "FirstName" },
            { "Nazwisko", "LastName" },
            { "Rok urodzenia", "BirthYear" },
            { "Miasto", "City" },
            { "Adres", "Address" },
            { "Email", "Email" }
        };

        public static Dictionary<String, String> StudentsColumnNamesBindings =
            new Dictionary<string, string>
        {
            { "id", "StudentId" },
            { "Imię", "FirstName" },
            { "Nazwisko", "LastName" },
            { "Rok urodzenia", "BirthYear" },
            { "Miasto", "City" },
            { "Szkoła", "School" }
        };

        public static Dictionary<String, String> ParentsColumnNamesBindings =
            new Dictionary<string, string>
        {
            { "id", "ParentId" },
            { "Imię", "FirstName" },
            { "Nazwisko", "LastName" },
            { "Miasto", "City" },
            { "Numer telefonu", "PhoneNumber" }
        };

    }
}
