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
    /// Interaction logic for VolunteerDetailsWindow.xaml
    /// </summary>
    public partial class VolunteerDetailsWindow : Window
    {
        private DataManager dataManager;
        private Volunteer volunteer;

        public VolunteerDetailsWindow(DataManager dataManager, 
            Volunteer volunteer)
        {
            InitializeComponent();

            this.dataManager = dataManager;
            this.volunteer = volunteer;

            SetVolunteerData();
        }

        private void SetVolunteerData()
        {
            FirstNameTextBox.Text = volunteer.FirstName;
            LastNameTextBox.Text = volunteer.LastName;
            BirthYearTextBox.Text = volunteer.BirthYear.ToString();
            CityComboBox.SelectedItem = volunteer.City;

            var volunteerStudents = dataManager.GetVolunteerStudents(volunteer);
            StudentsDataGrid.ItemsSource = volunteerStudents;
        }
    }
}
