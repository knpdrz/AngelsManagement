using AngelsManagement.DataModels;
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

                //attach that datagrid to its city tab
                tabItem.Content = dataGrid;
            }
            /*todo it was previously here- now unused
            TabItem VolunteersTab1 = new TabItem();
            VolunteersTab1.Header = "Gdańsk";

            DataGrid VolunteersTab1DataGrid = new DataGrid();
            VolunteersTab1DataGrid.ItemsSource = Volunteers;
            VolunteersTab1DataGrid.IsReadOnly = true;
            VolunteersTabControl.Items.Add(tabItem);


            VolunteersTab1.Content = VolunteersTab1DataGrid;*/

            //manage students tab
            //todo same as above
            //manage parents tab

        }
        

        private void OnAddPersonButtonClick(object sender, RoutedEventArgs e)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow(dataManager);
            addPersonWindow.Show();
        }
    }
}
