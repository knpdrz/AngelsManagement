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
        private static string volStudTableName = "VolStud";
        private static string studParTableName = "StudPar";

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

            string createVolStudTableCmd = @"CREATE TABLE IF NOT EXISTS "
                        + volStudTableName
                        + @"(
                        [vol_id] INTEGER NOT NULL,
                        [stud_id] INTEGER NOT NULL,
                        CONSTRAINT [id] PRIMARY KEY (vol_id, stud_id),
                        FOREIGN KEY (vol_id) REFERENCES "+ volunteersTableName +
                        " (ID), FOREIGN KEY (stud_id) REFERENCES " + studentsTableName +
                        "(ID)"+
                        ")";

            //todo if changes in classes- here too
            string createVolunteersTableCmd = @"CREATE TABLE IF NOT EXISTS "
                        + volunteersTableName
                        + @"(
                        [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [first_name] varchar(255) NULL,
                        [last_name] varchar(255) NULL,
                        [birth_year] INTEGER NULL,
                        [city] varchar(255) NOT NULL,
                        [address] varchar(1024) NULL,
                        [email] varchar(1024) NULL)";/*,
                        FOREIGN KEY(v_s_id) REFERENCES(" + volStudTableName + 
                        ")";*/


            string createStudentsTableCmd = @"CREATE TABLE IF NOT EXISTS "
                        + studentsTableName
                        + @"(
                        [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [first_name] varchar(255) NULL,
                        [last_name] varchar(255) NULL,
                        [school] varchar(1024) NULL,
                        [birth_year] INTEGER NULL,
                        [city] varchar(255) NOT NULL)";/*
                        FOREIGN KEY(v_s_id) REFERENCES (" + volStudTableName +"), " +
                        "FOREIGN KEY(s_p_id) REFERENCES (" + studParTableName +
                        ")";*/

            string createParentsTableCmd = @"CREATE TABLE IF NOT EXISTS "
                        + parentsTableName
                        + @"(
                        [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [first_name] varchar(255) NULL,
                        [last_name] varchar(255) NULL,
                        [birth_year] INTEGER NULL,
                        [city] varchar(255) NOT NULL)";/*
                        FOREIGN KEY(s_p_id) REFERENCES(" 
                        + studParTableName +
                            ")";*/

            //TODO- many to many- missing vs and sp
            //connect to the db
            using (var conn = new SQLiteConnection(connectionString))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();

                    //create all needed tables if they don't exist
                    //volunteers
                    cmd.CommandText = createVolunteersTableCmd;
                    cmd.ExecuteNonQuery();

                    //students
                    cmd.CommandText = createStudentsTableCmd;
                    cmd.ExecuteNonQuery();

                    //parents
                    cmd.CommandText = createParentsTableCmd;
                    cmd.ExecuteNonQuery();

                    //table expressing many-to-many relationship
                    //between volunteer and student
                    cmd.CommandText = createVolStudTableCmd;
                    cmd.ExecuteNonQuery();

                    //todo- same with student-parent

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

                    // get matching db entries
                    cmd.CommandText = selectQueryText;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Int64 id = (Int64)reader["ID"];
                            string fName = (string)reader["first_name"];
                            string lName = (string)reader["last_name"];
                            Int64 bYear = (Int64)reader["birth_year"];
                            string address = (string)reader["address"];
                            string email = (string)reader["email"];

                            volunteers.Add(new Volunteer
                            {
                                ID = id,
                                FirstName = fName,
                                LastName = lName,
                                BirthYear = bYear,
                                City = city,
                                Address = address,
                                Email = email
                            });
                        }
                    }
                    conn.Close(); // Close the connection to the database
                }
            }

            return volunteers;
        }

        public List<Student> GetStudentsByCity(string city)
        {
            List<Student> students = new List<Student>();
            //connect to the db
            using (var conn = new SQLiteConnection(connectionString))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    string selectQueryText = "SELECT * FROM " + studentsTableName
                        + " WHERE city='" + city + "'";

                    // get matching db entries
                    cmd.CommandText = selectQueryText;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Int64 id = (Int64)reader["ID"];
                            string fName = (string)reader["first_name"];
                            string lName = (string)reader["last_name"];
                            Int64 bYear = (Int64)reader["birth_year"];
                            string school = (string)reader["school"];

                            students.Add(new Student
                            {
                                ID = id,
                                FirstName = fName,
                                LastName = lName,
                                BirthYear = bYear,
                                City = city,
                                School = school
                            });
                        }
                    }
                    conn.Close(); // Close the connection to the database
                }
            }

            return students;
        }

        public void InsertIntoVolunteers(Volunteer v)//todo city as enum
        {
            string insertCmdText = "INSERT INTO " + volunteersTableName
                       + " (first_name, last_name, birth_year, city, address, email) VALUES ("
                       + "'" + v.FirstName + "', "
                       + "'" + v.LastName + "', "
                       + v.BirthYear + ", "
                       + "'" + v.City + "',"
                       + "'" + v.Address + "',"
                       + "'" + v.Email + "'"
                       + ")";

            ExecuteNonQuery(insertCmdText);
        }
         public void InsertIntoStudents(Student s)//todo city as enum
        {
            string insertCmdText = "INSERT INTO " + studentsTableName
                       + " (first_name, last_name, birth_year, city, school) VALUES ("
                       + "'" + s.FirstName + "', "
                       + "'" + s.LastName + "', "
                       + s.BirthYear + ", "
                       + "'" + s.City + "',"
                       + "'" + s.School + "'"
                       + ")";

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
