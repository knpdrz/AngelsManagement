using AngelsManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static AngelsManagement.Globals;

namespace AngelsManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Volunteer> Volunteers { get; set; }
        string volunteersTableName = "Volunteers";

        public MainWindow()
        {
            InitializeComponent();

            TabItem StudentsTabItem1 = new TabItem();
            StudentsTabControl.Items.Add(StudentsTabItem1);
            StudentsTabItem1.Header = "Gdańsk";


            //InitializeViews();
        }

        private void InitializeViews()
        {

            Volunteers = new ObservableCollection<Volunteer>();
            PrepareDb();
            VolunteersDataGridGda.ItemsSource = Volunteers;

        }

        private void PrepareDb()
        {
            //create app data directory (it won't if it already exists)
            Directory.CreateDirectory(appDataFolderPath);

            // create the file which will be hosting the database
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }

            string createTableQuery = @"CREATE TABLE IF NOT EXISTS "
                                        + volunteersTableName
                                        + @"(
                                        [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                        [first_name] varchar(255) NULL,
                                        [last_name] varchar(255) NULL,
                                        [birth_year] INTEGER NULL
                                         )";

            //connect to the db
            using (var conn = new SQLiteConnection(connectionString))
            {
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();

                    //create table if it doesn't exist
                    cmd.CommandText = createTableQuery;
                    cmd.ExecuteNonQuery();

                    // Select and display database entries
                    cmd.CommandText = "SELECT * FROM " + volunteersTableName;

                    Volunteers.Clear();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fName = (string)reader["first_name"];
                            string lName = (string)reader["last_name"];
                            Int64 bYear = (Int64)reader["birth_year"];

                            Console.WriteLine("person " + fName + "," + lName +
                                ", " + bYear);
                            Volunteers.Add(new Volunteer
                            {
                                FirstName = fName,
                                LastName = lName,
                                BirthYear = bYear
                            });
                        }
                    }
                    conn.Close(); // Close the connection to the database
                    Console.WriteLine("number = " + Volunteers.Count());
                }
            }
        }

        private void InsertIntoVolunteers(string firstName, string lastName, int birthYear)
        {
            string insertCmdText = "INSERT INTO " + volunteersTableName
                       + " (first_name, last_name, birth_year) VALUES ("
                       + "'" + firstName + "', "
                       + "'" + lastName + "', "
                       + birthYear
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
           
            
        }
    }
}
