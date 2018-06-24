using AngelsManagement.Model;
using AngelsManagement.Windows;
using Microsoft.EntityFrameworkCore;
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

        private void InitializeFromEF()
        {
            TabItem tabItem;
            DataGrid dataGrid;

            string city = "Gdansk";
            //for each city create a TabItem
            tabItem = new TabItem();
            tabItem.Header = city;

            //add that tab item to students TabControl 
            StudentsTabControl.Items.Add(tabItem);

            //create DataGrid for students from that particular city
            dataGrid = new DataGrid();
            dataGrid.IsReadOnly = true;

            using(var ctx = new PeopleContext())
            {
                var volos = ctx.Volunteers
                    .Where(v => v.City.Equals(city)).ToList();
                //Console.WriteLine("-----" + volos.Count());
                
                dataGrid.ItemsSource = volos;

            }

            //attach that datagrid to its city tab
            tabItem.Content = dataGrid;
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
            InitializeParentsTab();
        }

        private void InitializeVolunteersTab()//todo- refactor- code duplication
        {
            TabItem tabItem;
            DataGrid dataGrid;

            String[] cities = dataManager.Cities;
            //manage volunteers tab (called VolunteersTabControl, has tabs with all cities)
            Dictionary<String, ObservableCollection<Volunteer>> volunteersDict = 
                dataManager.VolunteersDict;

            foreach(string city in cities)
            {
                if (volunteersDict.ContainsKey(city))
                {
                    //for each city that has volunteers create a TabItem
                    tabItem = new TabItem();
                    tabItem.Header = city;

                    //add that tab item to volunteers TabControl 
                    VolunteersTabControl.Items.Add(tabItem);

                    //create DataGrid for volunteers from that particular city
                    dataGrid = new DataGrid();
                    dataGrid.IsReadOnly = true;
                    dataGrid.ItemsSource = volunteersDict[city];

                    //adding double-click event to volunteers datagrid
                    //if user double clicks a row, VolunteerRow_DoubleClick will be called
                    Style rowStyle = new Style(typeof(DataGridRow));
                    rowStyle.Setters.Add(new EventSetter(MouseDoubleClickEvent,
                                             new MouseButtonEventHandler(VolunteerRow_DoubleClick)));
                    dataGrid.RowStyle = rowStyle;

                    //attach that datagrid to its city tab
                    tabItem.Content = dataGrid;
                }
                
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

            String[] cities = dataManager.Cities;

            //manage students tab (called StudentsTabControl, has tabs with all cities)
            Dictionary<String, ObservableCollection<Student>> studentsDict =
                dataManager.StudentsDict;

            foreach (string city in cities)
            {
                if (studentsDict.ContainsKey(city))
                {
                    //for each city create a TabItem
                    tabItem = new TabItem();
                    tabItem.Header = city;

                    //add that tab item to students TabControl 
                    StudentsTabControl.Items.Add(tabItem);

                    //create DataGrid for students from that particular city
                    dataGrid = new DataGrid();
                    dataGrid.IsReadOnly = true;
                    dataGrid.ItemsSource = studentsDict[city];

                    //attach that datagrid to its city tab
                    tabItem.Content = dataGrid;
                }
            }
        }

        private void InitializeParentsTab()
        {
            TabItem tabItem;
            DataGrid dataGrid;

            String[] cities = dataManager.Cities;

            //manage students tab (called ParentsTabControl, has tabs with all cities)
            Dictionary<String, ObservableCollection<Parent>> parentsDict =
                dataManager.ParentsDict;

            foreach (string city in cities)
            {
                if (parentsDict.ContainsKey(city))
                {
                    //for each city create a TabItem
                    tabItem = new TabItem();
                    tabItem.Header = city;

                    //add that tab item to parents TabControl 
                    ParentsTabControl.Items.Add(tabItem);

                    //create DataGrid for students from that particular city
                    dataGrid = new DataGrid();
                    dataGrid.IsReadOnly = true;
                    dataGrid.ItemsSource = parentsDict[city];

                    //attach that datagrid to its city tab
                    tabItem.Content = dataGrid;
                }
            }
        }

        private void OnAddVolunteerButtonClick(object sender, RoutedEventArgs e)
        {
            AddVolunteerWindow addVolunteerWindow = new AddVolunteerWindow(dataManager);
            addVolunteerWindow.Show();
        }
        private void OnAddStudentButtonClick(object sender, RoutedEventArgs e)
        {
            AddStudentWindow addStudentWindow = 
                new AddStudentWindow(dataManager);
            addStudentWindow.Show();
        }
    }
}
