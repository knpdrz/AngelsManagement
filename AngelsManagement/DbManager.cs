using AngelsManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AngelsManagement.Globals;
namespace AngelsManagement
{
    public class DbManager
    {
        private static string volunteersTableName = "Volunteers";
        private static string studentsTableName = "Students";
        private static string parentsTableName = "Parents";

        //creates app data directory, db file and db tables if they don't exist
        //tables for:
        //Volunteers, Students, Parents, VS and SP 
        //(as volo-student and student-parent relationships are many-to-many)
        //db file is located in dbFilePath
        //app data directory is located in appDataFolderPath
        public void PrepareDb()
        {
            //create app data directory (it won't if it already exists)
            Directory.CreateDirectory(appDataFolderPath);

            // create the file which will be hosting the database
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }

            string createVolunteersTableQuery = @"CREATE TABLE IF NOT EXISTS "
                                        + volunteersTableName
                                        + @"(
                                        [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                        [first_name] varchar(255) NULL,
                                        [last_name] varchar(255) NULL,
                                        [birth_year] INTEGER NULL,
                                        [city] varchar(255) NOT NULL
                                         )";

            string createStudentsTableQuery = @"CREATE TABLE IF NOT EXISTS "
                                        + volunteersTableName
                                        + @"(
                                        [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                        [first_name] varchar(255) NULL,
                                        [last_name] varchar(255) NULL,
                                        [school_name] varchar(1024) NULL,
                                        [birth_year] INTEGER NULL,
                                        [city] varchar(255) NOT NULL
                                         )";

            string createParentsTableQuery = @"CREATE TABLE IF NOT EXISTS "
                                        + volunteersTableName
                                        + @"(
                                        [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                        [first_name] varchar(255) NULL,
                                        [last_name] varchar(255) NULL,
                                        [birth_year] INTEGER NULL,
                                        [city] varchar(255) NOT NULL
                                         )";

            //TODO- many to many- missing vs and sp
            //connect to the db
            using (var conn = new SQLiteConnection(connectionString))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();

                    //create all needed tables if they don't exist
                    //volunteers
                    cmd.CommandText = createVolunteersTableQuery;
                    cmd.ExecuteNonQuery();

                    //students
                    cmd.CommandText = createVolunteersTableQuery;
                    cmd.ExecuteNonQuery();

                    //parents
                    cmd.CommandText = createVolunteersTableQuery;
                    cmd.ExecuteNonQuery();

                    conn.Close(); // Close the connection to the database
                }
            }
        }

        public List<Volunteer> GetVolunteersByCity(string city)
        {
            List<Volunteer> volunteers = new List<Volunteer>();
            //connect to the db
            using (var conn = new SQLiteConnection(connectionString))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();


                    string selectQueryText = "SELECT * FROM " + volunteersTableName
                        + " WHERE city='" + city + "'";

                    Console.WriteLine("querying " + selectQueryText);
                    // get matching db entries
                    cmd.CommandText = selectQueryText;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fName = (string)reader["first_name"];
                            string lName = (string)reader["last_name"];
                            Int64 bYear = (Int64)reader["birth_year"];


                            volunteers.Add(new Volunteer
                            {
                                FirstName = fName,
                                LastName = lName,
                                BirthYear = bYear,
                                City = city
                            });
                        }
                    }
                    conn.Close(); // Close the connection to the database
                }
            }

            return volunteers;
        }

        public void InsertIntoVolunteers(Volunteer v)//todo city as enum
        {

            string insertCmdText = "INSERT INTO " + volunteersTableName
                       + " (first_name, last_name, birth_year, city) VALUES ("
                       + "'" + v.FirstName + "', "
                       + "'" + v.LastName + "', "
                       + v.BirthYear + ", "
                       + "'" + v.City + "'"
                       + ")";

            Console.WriteLine("inserting " + insertCmdText);

            ExecuteNonQuery(insertCmdText);
        }

        private static void ExecuteNonQuery(string commandText)
        {
            //connect to the db
            using (var conn = new SQLiteConnection(connectionString))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();

                    cmd.CommandText = commandText;
                    cmd.ExecuteNonQuery();

                    conn.Close(); // close the connection to the database
                }
            }
        }
    }
}
