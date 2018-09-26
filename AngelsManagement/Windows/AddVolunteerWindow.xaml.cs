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
    /// Interaction logic for AddVolunteerWindow.xaml
    /// </summary>
    public partial class AddVolunteerWindow : Window
    {
        private DataManager dataManager;
        public AddVolunteerWindow(DataManager dataManager)
        {
            InitializeComponent();
            this.dataManager = dataManager;
        }

        private void AddVolunteerButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string birthYear = BirthYearTextBox.Text;
            string email = EmailTextBox.Text;
            string address = AddressTextBox.Text;
            string city = ((ComboBoxItem)CityComboBox.SelectedItem).Content.ToString();

            List<String> errorsList =
                Volunteer.FindVolunteerValidationErrors(
                    firstName, lastName, birthYear, address, email);

            if (errorsList.Count() == 0)//if there are no errors
            {
                Volunteer volunteer = new Volunteer(firstName, 
                    lastName, birthYear, city, address, email);
                AddVolunteer(volunteer);

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

        private void AddVolunteer(Volunteer volunteer)
        {
            dataManager.AddVolunteer(volunteer);
            Close();
        }
        
    }
}
