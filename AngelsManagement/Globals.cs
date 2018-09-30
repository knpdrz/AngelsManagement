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

        public static String[] Cities = { "Gdansk", "Wroclaw", "Poznan" };

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

        public static Dictionary<String, String> GuardiansColumnNamesBindings =
            new Dictionary<string, string>
        {
            { "id", "GuardianId" },
            { "Imię", "FirstName" },
            { "Nazwisko", "LastName" },
            { "Miasto", "City" },
            { "Numer telefonu", "PhoneNumber" }
        };

        public static String DeleteString = "Usuń";

        public static String FirstNameErrorText = "Błędne imię: ";
        public static String LastNameErrorText = "Błędne nazwisko: ";
        public static String YearErrorText = "Błędny rok urodzenia: ";
        public static String PhoneErrorText = "Błędny numer telefonu: ";
        public static String SchoolErrorText = "Błędna nazwa szkoły: ";
        public static String EmailErrorText = "Błędny adres email: ";
        public static String AddressErrorText = "Błędny adres: ";
        public static String ErrorText = "Wprowadzono błędne dane";

        public static String WrongLoginErrorText = "Nieprawidłowy login";
        public static String WrongPasswordErrorText = "Nieprawidłowe hasło";
        public static String WrongLoginOrPasswordErrorText = "Błędny login lub hasło";
        public static String UserAlreadyExistsErrorText = "Użytkownik o podanym loginie już istnieje";
        public static String UserDoesNotExistsErrorText = "Użytkownik o podanym loginie nie istnieje";

        public static String SuccessText = "Sukces";
        public static String UserSuccessfullyCreatedText = "Użytkownik utworzony!";
        public static String UserPasswordSuccessfullyUpdatedText = "Zmieniono hasło użytkownika ";

        public static Int64 MinPersonYear = 1900;
        //current year
        public static Int64 MaxPersonYear = Int64.Parse(DateTime.Now.Year.ToString());

        public static string AdminUsername = "admin";
        public static string AdminDefaultPassword = "admin";

        public static bool iAmAdminFlag = false;
    }
}
