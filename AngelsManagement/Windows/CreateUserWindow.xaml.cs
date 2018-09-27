using AngelsManagement.Managers;
using AngelsManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;
using static AngelsManagement.Globals;

namespace AngelsManagement.Windows
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        public CreateUserWindow()
        {
            InitializeComponent();
            LoginTextBox.Focus();
        }
        
        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserCredentialsValidationManager userCredentialsValidationManager =
                new UserCredentialsValidationManager(LoginTextBox.Text, PasswordBox.Password, true);

            if (userCredentialsValidationManager.ValidationOk)
            {
                //create new user
                UserManager.CreateUser(LoginTextBox.Text, PasswordBox.Password);

                //show dialog with success info
                MessageBoxResult result = MessageBox.Show(UserSuccessfullyCreatedText,
                        SuccessText,
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                Close();
            }
            else
            {
                //show dialog with information which data were incorrect
                MessageBoxResult result = MessageBox.Show(userCredentialsValidationManager.GetValidationErrorString(),
                        ErrorText,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
        }
    }
}
