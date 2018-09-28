﻿using AngelsManagement.Managers;
using AngelsManagement.Windows;
using System.Windows;

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

        private void OnCreateNewUserButtonClick(object sender, RoutedEventArgs e)
        {
            CreateUserWindow createUserWindow = new CreateUserWindow();
            createUserWindow.Show();
        }

        private void OnChangeUserPasswordButtonClick(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
            changePasswordWindow.Show();
        }
    }
}
