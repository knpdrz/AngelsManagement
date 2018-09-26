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
            SchoolTextBox.Text = student.School;
            //set city in combobox
            foreach (ComboBoxItem item in CityComboBox.Items)
            {
                if (item.Content.ToString().Equals(student.City))
                {
                    item.IsSelected = true;
                }
            }
            UpdateGuardiansList();
        }

        public void UpdateGuardiansList()
        {
            var studentGuardians = dataManager.GetStudentGuardians(student);
            guardiansDataGrid.ItemsSource = studentGuardians;
        }

        private void AddGuardianButton_Click(object sender, RoutedEventArgs e)
        {
            AddGuardianToStudentWindow addStudentToVWindow =
              new AddGuardianToStudentWindow(this, dataManager, student);

            //make this window unclickable
            IsEnabled = false;
            addStudentToVWindow.Show();
        }
    }
}
