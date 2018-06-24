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

namespace AngelsManagement.Windows
{
    /// <summary>
    /// Interaction logic for AddParentToStudentWindow.xaml
    /// </summary>
    public partial class AddParentToStudentWindow : Window
    {
        private DataManager dataManager;
        private Student student;
        private StudentDetailsWindow studentDetailsWindow;

        public AddParentToStudentWindow(StudentDetailsWindow studentDetailsWindow,
            DataManager dataManager, Student student)
        {
            InitializeComponent();
            this.studentDetailsWindow = studentDetailsWindow;
            this.student = student;
            this.dataManager = dataManager;

            SetStudentData();
        }

        private void SetStudentData()
        {
            InfoTextBlock.Text += student.FirstName + " " + student.LastName;


        }

        private void AddParentButton_Click(object sender, RoutedEventArgs e)
        {//todo this doesn't look nice- flow: click -> validate- > add?
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string phone = PhoneTextBox.Text;//todo phone number

            string city = ((ComboBoxItem)CityComboBox.SelectedItem).Content.ToString();

            if (IsInputValid())
            {
                Parent parent = new Parent
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phone,
                    City = city
                };

                AddParent(parent);
                studentDetailsWindow.UpdateParentsList();
                studentDetailsWindow.IsEnabled = true;
                Close();
            }
        }

        private void AddParent(Parent parent)
        {
            dataManager.AddParentToStudent(student, parent);
        }

        private bool IsInputValid()//todo validation
        {
            return true;
        }
    }
}
