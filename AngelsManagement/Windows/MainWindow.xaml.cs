using AngelsManagement.Managers;
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
        private TabsManager tabsManager;

        public MainWindow()
        {
            InitializeComponent();
            PrepareEverything();

        }
        
        private void PrepareEverything()
        {
            dataManager = new DataManager();
            tabsManager = new TabsManager(this, dataManager);
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
