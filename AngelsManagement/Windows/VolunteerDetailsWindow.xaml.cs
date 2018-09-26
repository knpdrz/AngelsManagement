﻿using AngelsManagement.Model;
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

            UpdateStudentsList();
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            AddStudentToVWindow addStudentToVWindow =
               new AddStudentToVWindow(this, dataManager, volunteer);

            //make this window unclickable
            IsEnabled = false;
            addStudentToVWindow.Show();
        }

        public void UpdateStudentsList()
        {
            var volunteerStudents = dataManager.GetVolunteerStudents(volunteer);
            StudentsDataGrid.ItemsSource = volunteerStudents;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            //get data from the form
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string birthYear = BirthYearTextBox.Text;
            string email = "a@a.com";//todo EmailTextBox.Text;
            string address = "todo! address"; //todo AddressTextBox.Text;
            string city = ((ComboBoxItem)CityComboBox.SelectedItem).Content.ToString();

            List<String> errorsList =
                Volunteer.FindVolunteerValidationErrors(
                    firstName, lastName, birthYear, address, email);

            if (errorsList.Count() == 0)//if there are no errors
            {
                Volunteer updatedVolunteer = new Volunteer(volunteer.VolunteerId, firstName,
                    lastName, birthYear, city, address, email);
                UpdateVolunteer(updatedVolunteer);

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

        private void UpdateVolunteer(Volunteer updatedVolunteer)
        {
            dataManager.UpdateVolunteer(volunteer.City, updatedVolunteer);
        }
    }
}
