using AngelsManagement.Model;
using AngelsManagement.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using static AngelsManagement.Globals;
namespace AngelsManagement.Managers
{
    //class for managing tabs (TabControls) in the MainWindow
    //(setting volunteers, students and guardians data in the correct city tab)
    public class TabsManager
    {
        private MainWindow window;
        private DataManager dataManager;

        public TabsManager(MainWindow window, DataManager dataManager)
        {
            this.window = window;
            this.dataManager = dataManager;
            InitializeTabs();
        }

        private void InitializeTabs()
        {
            InitializeVolunteersTab();
            InitializeStudentsTab();
            InitializeguardiansTab();
        }

        private void AddColumnWithBinding(string header, string bindingName, DataGrid dataGrid)
        {
            var col = new DataGridTextColumn();
            col.Header = header;
            col.Binding = new Binding(bindingName);
            dataGrid.Columns.Add(col);
        }
        
        private DataGrid CreateDataGrid(IEnumerable<object> itemsSource, 
            Dictionary<String, String> columnNames,
            MouseButtonEventHandler mouseButtonEventHandler)
        {
            DataGrid dataGrid = new DataGrid
            {
                IsReadOnly = true,
                AutoGenerateColumns = false,
                ItemsSource = itemsSource
            };

            //selecting columns we want to see in the datagrid
            //and giving them names taken from columnNames dictionary
            //entries in the dictionary are in form 
            //<column name to be displayed, column name in the db>
            foreach(var columnName in columnNames)
            {
                AddColumnWithBinding(columnName.Key, columnName.Value, dataGrid);
            }

            if (mouseButtonEventHandler != null)
            {
                //adding double-click event to the datagrid
                //if user double clicks a row, mouseButtonEventHandler will be called
                Style rowStyle = new Style(typeof(DataGridRow));
                rowStyle.Setters.Add(new EventSetter(Window.MouseDoubleClickEvent,
                                         new MouseButtonEventHandler(mouseButtonEventHandler)));
                dataGrid.RowStyle = rowStyle;
            }
            return dataGrid;
        }

        

        private void CreateCityTab(TabControl tabControl, IEnumerable<object> itemsSource,
            String city, Dictionary<String, String> columnNames, 
            MouseButtonEventHandler mouseButtonEventHandler)
        {
            //for a city create a TabItem
            TabItem tabItem = new TabItem();
            tabItem.Header = city;

            //add that tab item to tabControl 
            tabControl.Items.Add(tabItem);

            //attach created datagrid to its city tab
            DataGrid dataGrid = CreateDataGrid(itemsSource, 
                columnNames, mouseButtonEventHandler);
            tabItem.Content = dataGrid;
        }

        private void InitializeVolunteersTab()
        {
            //manage volunteers tab (called VolunteersTabControl, has tabs with all cities)
            Dictionary<String, ObservableCollection<Volunteer>> volunteersDict =
                dataManager.VolunteersDict;

            foreach (string city in Cities)
            {
                if (volunteersDict.ContainsKey(city))
                {
                    CreateCityTab(window.VolunteersTabControl, 
                        volunteersDict[city], city, VolunteersColumnNamesBindings,
                        VolunteerRow_DoubleClick);
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
            //manage students tab (called StudentsTabControl, has tabs with all cities)
            Dictionary<String, ObservableCollection<Student>> studentsDict =
                dataManager.StudentsDict;

            foreach (string city in Cities)
            {
                if (studentsDict.ContainsKey(city))
                {
                    CreateCityTab(window.StudentsTabControl,
                        studentsDict[city], city, StudentsColumnNamesBindings,
                        StudentRow_DoubleClick);
                }
            }
        }

        private void StudentRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Student doubleClickedStudent = (Student)row.Item;

            //showing details of doubleclicked student
            StudentDetailsWindow studentDetailsWindow =
                new StudentDetailsWindow(dataManager, doubleClickedStudent);
            studentDetailsWindow.Show();
        }

        private void InitializeguardiansTab()
        {
            //manage students tab (called guardiansTabControl, has tabs with all cities)
            Dictionary<String, ObservableCollection<Guardian>> guardiansDict =
                dataManager.GuardiansDict;

            foreach (string city in Cities)
            {
                if (guardiansDict.ContainsKey(city))
                {
                    CreateCityTab(window.guardiansTabControl,
                        guardiansDict[city], city, GuardiansColumnNamesBindings,
                        null);
                }
            }
        }
    }
}
