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
    /// Interaction logic for StudentDetailsWindow.xaml
    /// </summary>
    public partial class StudentDetailsWindow : Window
    {
        private DataManager dataManager;
        private Student student;

        public StudentDetailsWindow(DataManager dataManager, 
            Student student)
        {
            InitializeComponent();

            this.dataManager = dataManager;
            this.student = student;

            SetStudentData();
        }

        private void SetStudentData()
        {
            FirstNameTextBox.Text = student.FirstName;
            LastNameTextBox.Text = student.LastName;
            BirthYearTextBox.Text = student.BirthYear.ToString();
            CityComboBox.SelectedItem = student.City;

            UpdateParentsList();
        }

        public void UpdateParentsList()
        {
            var studentParents = dataManager.GetStudentParents(student);
            ParentsDataGrid.ItemsSource = studentParents;
        }

        private void AddParentButton_Click(object sender, RoutedEventArgs e)
        {
            AddParentToStudentWindow addStudentToVWindow =
              new AddParentToStudentWindow(this, dataManager, student);

            //make this window unclickable
            IsEnabled = false;
            addStudentToVWindow.Show();
        }
    }
}
