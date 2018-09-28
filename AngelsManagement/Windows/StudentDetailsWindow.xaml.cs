using AngelsManagement.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static AngelsManagement.Globals;

namespace AngelsManagement.Windows
{
    /// <summary>
    /// Interaction logic for StudentDetailsWindow.xaml
    /// </summary>
    public partial class StudentDetailsWindow : Window
    {
        private DataManager dataManager;
        private Student student;

        public StudentDetailsWindow(DataManager dataManager, 
            Student student)
        {
            InitializeComponent();

            this.dataManager = dataManager;
            this.student = student;

            SetStudentData();
            AdjustViewToAuthorization();
        }

        private void AdjustViewToAuthorization()
        {
            //enable editing student details for admin
            if (iAmAdminFlag)
            {
                FirstNameTextBox.IsEnabled = true;
                LastNameTextBox.IsEnabled = true;
                BirthYearTextBox.IsEnabled = true;
                SchoolTextBox.IsEnabled = true;
                CityComboBox.IsEnabled = true;

                SaveChangesButton.Visibility = Visibility.Visible;
                AddGuardianButton.Visibility = Visibility.Visible;
            }
        }

        private void SetStudentData()
        {
            FirstNameTextBox.Text = student.FirstName;
            LastNameTextBox.Text = student.LastName;
            BirthYearTextBox.Text = student.BirthYear.ToString();
            SchoolTextBox.Text = student.School;

            //set city in combobox
            foreach (ComboBoxItem item in CityComboBox.Items)
            {
                if (item.Content.ToString().Equals(student.City))
                {
                    item.IsSelected = true;
                }
            }

            PrepareGuardiansDataGrid();
            UpdateGuardiansList();
        }

        private void AddColumnWithBinding(string header, string bindingName, DataGrid dataGrid)
        {
            var col = new DataGridTextColumn();
            col.Header = header;
            col.Binding = new Binding(bindingName);
            dataGrid.Columns.Add(col);
        }

        private void PrepareGuardiansDataGrid()
        {
            //selecting columns we want to see in the datagrid
            //and giving them names taken from columnNames dictionary
            //entries in the dictionary are in form 
            //<column name to be displayed, column name in the db>
            foreach (var columnName in GuardiansColumnNamesBindings)
            {
                AddColumnWithBinding(columnName.Key, columnName.Value, GuardiansDataGrid);
            }
        }

        public void UpdateGuardiansList()
        {
            var studentGuardians = dataManager.GetStudentGuardians(student);
            GuardiansDataGrid.ItemsSource = studentGuardians;
        }

        private void AddGuardianButton_Click(object sender, RoutedEventArgs e)
        {
            AddGuardianToStudentWindow addStudentToVWindow =
              new AddGuardianToStudentWindow(this, dataManager, student);

            //make this window unclickable
            IsEnabled = false;
            addStudentToVWindow.Show();
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            //get data from the form
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string birthYear = BirthYearTextBox.Text;
            string city = ((ComboBoxItem)CityComboBox.SelectedItem).Content.ToString();
            string school = SchoolTextBox.Text;

            List<String> errorsList =
               Student.FindStudentValidationErrors(firstName, lastName,
               birthYear, school);

            if (errorsList.Count() == 0)//if there are no errors
            {
                Student updatedStudent = new Student(student.StudentId, firstName,
                                    lastName, birthYear, city, school);
                UpdateStudent(updatedStudent);

            }
            else
            {
                //show dialog with information which data were incorrect
                var errorString = String.Join("\n", errorsList.ToArray());
                MessageBoxResult result = System.Windows.MessageBox.Show(errorString,
                        ErrorText,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
        }

        private void UpdateStudent(Student updatedStudent)
        {
            dataManager.UpdateStudent(student.City, updatedStudent);
            student = updatedStudent;
        }
    }
}
