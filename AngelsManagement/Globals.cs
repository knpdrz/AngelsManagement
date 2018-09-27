﻿using System;
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

        public static String FirstNameErrorText = "Błędne imię: ";
        public static String LastNameErrorText = "Błędne nazwisko: ";
        public static String YearErrorText = "Błędny rok urodzenia: ";
        public static String PhoneErrorText = "Błędny numer telefonu: ";
        public static String SchoolErrorText = "Błędna nazwa szkoły: ";
        public static String EmailErrorText = "Błędny adres email: ";
        public static String AddressErrorText = "Błędny adres: ";
        public static String ErrorText = "Wprowadzono błędne dane";

        public static String EmptyLoginErrorText = "Login nie może być pusty";
        public static String EmptyPasswordErrorText = "Hasło nie może być puste";
        public static String WrongLoginOrPasswordErrorText = "Błędny login lub hasło";

        public static Int64 MinPersonYear = 1900;
        //current year
        public static Int64 MaxPersonYear = Int64.Parse(DateTime.Now.Year.ToString());

    }
}
