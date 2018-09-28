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
    /// Interaction logic for VolunteerDetailsWindow.xaml
    /// </summary>
    public partial class GuardianDetailsWindow : Window
    {
        private DataManager dataManager;
        private Guardian guardian;

        public GuardianDetailsWindow(DataManager dataManager,
            Guardian guardian)
        {
            InitializeComponent();

            this.dataManager = dataManager;
            this.guardian = guardian;

            SetGuardianData();
            AdjustViewToAuthorization();
        }

        private void AdjustViewToAuthorization()
        {
            //enable editing student details for admin
            if (iAmAdminFlag)
            {
                FirstNameTextBox.IsEnabled = true;
                LastNameTextBox.IsEnabled = true;
                PhoneTextBox.IsEnabled = true;
                CityComboBox.IsEnabled = true;

                SaveChangesButton.Visibility = Visibility.Visible;
            }
        }


        private void SetGuardianData()
        {
            FirstNameTextBox.Text = guardian.FirstName;
            LastNameTextBox.Text = guardian.LastName;
            PhoneTextBox.Text = guardian.PhoneNumber;

            //set city in combobox
            foreach(ComboBoxItem item in CityComboBox.Items)
            {
                if (item.Content.ToString().Equals(guardian.City))
                {
                    item.IsSelected = true;
                }
            }

            UpdateStudentsList();
        }

        public void UpdateStudentsList()
        {
            var guardianStudents = dataManager.GetGuardianStudents(guardian);
            StudentsDataGrid.ItemsSource = guardianStudents;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            //get data from the form
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string city = ((ComboBoxItem)CityComboBox.SelectedItem).Content.ToString();

            List<String> errorsList =
                Guardian.FindGuardianValidationErrors(firstName, lastName, phone);

            if (errorsList.Count() == 0)//if there are no errors
            {
                Guardian updatedGuardian = new Guardian(guardian.GuardianId, firstName, lastName, phone, city);
                UpdateGuardian(updatedGuardian);
            }
            else
            {
                //show dialog with information which data were incorrect
                var errorString = String.Join("\n", errorsList.ToArray());
                MessageBoxResult result = MessageBox.Show(errorString,
                        ErrorText,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
        }

        private void UpdateGuardian(Guardian updatedGuardian)
        {
            dataManager.UpdateGuardian(guardian.City, updatedGuardian);
            guardian = updatedGuardian;
        }
    }
}
