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
            InitializeGuardiansTab();
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
            MouseButtonEventHandler doubleClickMouseButtonEventHandler,
            MouseButtonEventHandler rightClickMouseButtonEventHandler)
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
            foreach (var columnName in columnNames)
            {
                AddColumnWithBinding(columnName.Key, columnName.Value, dataGrid);
            }

            Style rowStyle = new Style(typeof(DataGridRow));

            if (doubleClickMouseButtonEventHandler != null)
            {
                //adding double-click event to the datagrid
                //if user double clicks a row, doubleClickMouseButtonEventHandler will be called
                rowStyle.Setters.Add(new EventSetter(Window.MouseDoubleClickEvent,
                                         new MouseButtonEventHandler(doubleClickMouseButtonEventHandler)));
            }

            if (rightClickMouseButtonEventHandler != null)
            {
                //adding right-click event to the datagrid
                //if user right clicks a row, rightClickMouseButtonEventHandler will be called
                rowStyle.Setters.Add(new EventSetter(Window.MouseRightButtonDownEvent,
                                         new MouseButtonEventHandler(rightClickMouseButtonEventHandler)));
            }

            dataGrid.RowStyle = rowStyle;

            return dataGrid;
        }



        private void CreateCityTab(TabControl tabControl, IEnumerable<object> itemsSource,
            String city, Dictionary<String, String> columnNames,
            MouseButtonEventHandler doubleClickMouseButtonEventHandler,
            MouseButtonEventHandler rightClickMouseButtonHandler)
        {
            //for a city create a TabItem
            TabItem tabItem = new TabItem();
            tabItem.Header = city;

            //add that tab item to tabControl 
            tabControl.Items.Add(tabItem);

            //attach created datagrid to its city tab
            DataGrid dataGrid = CreateDataGrid(itemsSource,
                columnNames, doubleClickMouseButtonEventHandler,
                rightClickMouseButtonHandler);
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
                        VolunteerRow_DoubleClick, VolunteerRow_RightClick);
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

        private void VolunteerRow_RightClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Volunteer doubleClickedVolunteer = (Volunteer)row.Item;

            //showing options for double-clicked volunteer

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
                        StudentRow_DoubleClick, StudentRow_RightClick);
                }
            }
        }

        private void StudentRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Student doubleClickedStudent = (Student)row.Item;

            //showing details of double-clicked student
            StudentDetailsWindow studentDetailsWindow =
                new StudentDetailsWindow(dataManager, doubleClickedStudent);
            studentDetailsWindow.Show();
        }
        private void StudentRow_RightClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Student doubleClickedStudent = (Student)row.Item;

            //showing options for double-clicked student

        }

        private void InitializeGuardiansTab()
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
                        GuardianRow_DoubleClick, GuardianRow_RightClick);
                }
            }
        }

        private void GuardianRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Guardian doubleClickedGuardian = (Guardian)row.Item;

            //showing details of double-clicked guardian
            GuardianDetailsWindow guardianDetailsWindow =
                new GuardianDetailsWindow(dataManager, doubleClickedGuardian);
            guardianDetailsWindow.Show();
        }

        private void GuardianRow_RightClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Guardian rightClickedGuardian = (Guardian)row.Item;

            //showing options for double-clicked guardian
            ContextMenu contextMenu = new ContextMenu();
            MenuItem item = new MenuItem
            {
                Header = "Delete"
            };
            item.Click += new RoutedEventHandler(DeleteGuardian);
            contextMenu.Items.Add(item);

            contextMenu.IsOpen = true;
        }

        private void DeleteGuardian(object sender, RoutedEventArgs e)
        {
            //todo- get guardian data, delete guardian
            Console.WriteLine("deleting guardian");
        }

    }
}
