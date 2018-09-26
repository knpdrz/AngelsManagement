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

namespace AngelsManagement
{
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        private DataManager dataManager;

        public AddStudentWindow(DataManager dataManager)
        {
            InitializeComponent();
            this.dataManager = dataManager;

        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
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
                Student student = new Student(firstName,
                                    lastName, birthYear, city, school);
                AddStudent(student);

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

        private void AddStudent(Student student)
        {
            dataManager.AddStudent(student);
            Close();
        }

      
    }
}
