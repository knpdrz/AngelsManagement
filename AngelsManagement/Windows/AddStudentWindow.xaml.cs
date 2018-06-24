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
            string birthYearString = BirthYearTextBox.Text;

            int birthYear = Int32.Parse(birthYearString);//todoo
            string city = ((ComboBoxItem)CityComboBox.SelectedItem).Content.ToString();
            string school = SchoolTextBox.Text;

            if (IsInputValid())
            {
                Student student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthYear = birthYear,
                    City = city,
                    School = school
                };

                AddStudent(student);
                Close();
            }

        }

        private void AddStudent(Student student)
        {
            dataManager.AddStudent(student);
        }

        private bool IsInputValid()//todo validation
        {
            return true;
        }
    }
}
