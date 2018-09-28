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
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
            LoginTextBox.Focus();
        }
        
        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserCredentialsValidationManager userCredentialsValidationManager =
                new UserCredentialsValidationManager(LoginTextBox.Text, PasswordBox.Password);

            if (userCredentialsValidationManager.AreCredentialsValidOnPasswordChange())
            {
                //change password for user with given login
                UserManager.ChangeUserPassword(LoginTextBox.Text, PasswordBox.Password);

                //show dialog with success info
                MessageBoxResult result = MessageBox.Show(UserPasswordSuccessfullyUpdatedText,
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
