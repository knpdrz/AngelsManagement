using AngelsManagement.DataModels;
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
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        private DataManager dataManager;
        public AddPersonWindow(DataManager dataManager)
        {
            InitializeComponent();
            this.dataManager = dataManager;
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string birthYearString = BirthYearTextBox.Text;

            int birthYear = Int32.Parse(birthYearString);//todoo
            string city = ((ComboBoxItem)CityComboBox.SelectedItem).Content.ToString();

            if (IsInputValid())
            {
                Volunteer volunteer = new Volunteer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthYear = birthYear,
                    City = city
                };

                AddPerson(volunteer);
                Close();
            }
            
        }

        private void AddPerson(Volunteer volunteer)
        {
            dataManager.CreateVolunteer(volunteer);
        }

        private bool IsInputValid()//todo validation
        {
            return true;
        }


    }
}
