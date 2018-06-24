using AngelsManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static AngelsManagement.Globals;
namespace AngelsManagement.Windows
{
    /// <summary>
    /// Interaction logic for AddGuardianToStudentWindow.xaml
    /// </summary>
    public partial class AddGuardianToStudentWindow : Window
    {
        private DataManager dataManager;
        private Student student;
        private StudentDetailsWindow studentDetailsWindow;

        public AddGuardianToStudentWindow(StudentDetailsWindow studentDetailsWindow,
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

        private void AddGuardianButton_Click(object sender, RoutedEventArgs e)
        {//todo split to functions somehow?
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string city = ((ComboBoxItem)CityComboBox.SelectedItem).Content.ToString();

            List<String> errorsList =
                Guardian.FindGuardianValidationErrors(firstName, lastName, phone);

            if (errorsList.Count() == 0)//if there are no errors
            {
                Guardian guardian = new Guardian(firstName, lastName, phone, city);
                AddGuardian(guardian);
                
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

        private void AddGuardian(Guardian guardian)
        {
            dataManager.AddGuardianToStudent(student, guardian);
            studentDetailsWindow.UpdateGuardiansList();
            studentDetailsWindow.IsEnabled = true;
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            studentDetailsWindow.IsEnabled = true;

        }


    }
}
