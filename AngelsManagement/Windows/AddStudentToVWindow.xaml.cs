using AngelsManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddStudentToVWindow.xaml
    /// </summary>
    public partial class AddStudentToVWindow : Window
    {
        private DataManager dataManager;
        private Volunteer volunteer;
        private VolunteerDetailsWindow volunteerDetailsWindow;

        public AddStudentToVWindow(VolunteerDetailsWindow volunteerDetailsWindow,
            DataManager dataManager, Volunteer volunteer)
        {
            InitializeComponent();
            this.volunteerDetailsWindow = volunteerDetailsWindow;
            this.dataManager = dataManager;
            this.volunteer = volunteer;

            SetStudentsData();
        }

        //set name of the volunteer for whom user is now selecting students
        //and set source of students that user can choose from
        private void SetStudentsData()
        {
            //todo maybe display more data about volunteer?
            VolunteerTextBlock.Text += volunteer.FirstName + " " + volunteer.LastName;

            List<Student> students = 
                dataManager.GetNotVolunteersStudents(volunteer);

            StudentsDataGrid.ItemsSource = new ObservableCollection<Student>(students);
        }

        //when AddSelectedButton is clicked, students selected by the user
        //are added to that volunteer's students list
        //then calls update on previous window- VolunteerDetailsWindow
        private void AddSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            List<Student> selectedStudents =
                StudentsDataGrid.SelectedItems.OfType<Student>().ToList();

            dataManager.AddStudentsToVolunteer(volunteer, selectedStudents);

            //update list of students on the volunteer details window
            //and make it clickable again
            volunteerDetailsWindow.UpdateStudentsList();
            volunteerDetailsWindow.IsEnabled = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
