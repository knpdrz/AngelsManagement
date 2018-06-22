using AngelsManagement.DataModels;
using AngelsManagement.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static AngelsManagement.Globals;

namespace AngelsManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataManager dataManager;

        public MainWindow()
        {
            InitializeComponent();

            PrepareEverything();
            
        }

        private void PrepareEverything()
        {
            dataManager = new DataManager();
            InitializeTabs();
        }

        private void InitializeTabs()
        {
            InitializeVolunteersTab();
            InitializeStudentsTab();
            //manage parents tab        
        }

        private void InitializeVolunteersTab()
        {
            TabItem tabItem;
            DataGrid dataGrid;

            //manage volunteers tab (called VolunteersTabControl, has tabs with all cities)
            Dictionary<String, ObservableCollection<Volunteer>> volunteersDict = 
                dataManager.VolunteersDict;

            foreach(KeyValuePair<String, ObservableCollection<Volunteer>> cityCollection in volunteersDict)
            {
                //for each city create a TabItem
                tabItem = new TabItem();
                tabItem.Header = cityCollection.Key;

                //add that tab item to volunteers TabControl 
                VolunteersTabControl.Items.Add(tabItem);

                //create DataGrid for volunteers from that particular city
                dataGrid = new DataGrid();
                dataGrid.IsReadOnly = true;
                dataGrid.ItemsSource = cityCollection.Value;

                //adding double-click event to volunteers datagrid
                //if user double clicks a row, VolunteerRow_DoubleClick will be called
                Style rowStyle = new Style(typeof(DataGridRow));
                rowStyle.Setters.Add(new EventSetter(DataGridRow.MouseDoubleClickEvent,
                                         new MouseButtonEventHandler(VolunteerRow_DoubleClick)));
                dataGrid.RowStyle = rowStyle;

                //attach that datagrid to its city tab
                tabItem.Content = dataGrid;
            }
        }

        private void VolunteerRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Volunteer doubleClickedVolunteer = (Volunteer)row.Item;

            //showing details of doubleclicked volunteer
            VolunteerDetailsWindow volunteerDetailsWindow = 
                new VolunteerDetailsWindow(dataManager, doubleClickedVolunteer);
            volunteerDetailsWindow.Show();
        }

        private void InitializeStudentsTab()
        {
            TabItem tabItem;
            DataGrid dataGrid;

            //manage students tab (called StudentsTabControl, has tabs with all cities)
            Dictionary<String, ObservableCollection<Student>> studentsDict =
                dataManager.StudentsDict;

            foreach (KeyValuePair<String, ObservableCollection<Student>> cityCollection in studentsDict)
            {
                //for each city create a TabItem
                tabItem = new TabItem();
                tabItem.Header = cityCollection.Key;

                //add that tab item to students TabControl 
                StudentsTabControl.Items.Add(tabItem);

                //create DataGrid for students from that particular city
                dataGrid = new DataGrid();
                dataGrid.IsReadOnly = true;
                dataGrid.ItemsSource = cityCollection.Value;
               
                //attach that datagrid to its city tab
                tabItem.Content = dataGrid;
            }
        }



        private void OnAddVolunteerButtonClick(object sender, RoutedEventArgs e)
        {
            AddVolunteerWindow addVolunteerWindow = new AddVolunteerWindow(dataManager);
            addVolunteerWindow.Show();
        }
        private void OnAddStudentButtonClick(object sender, RoutedEventArgs e)
        {
            AddStudentWindow addStudentWindow = new AddStudentWindow(dataManager);
            addStudentWindow.Show();
        }
    }
}
