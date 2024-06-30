using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FamilyBudgetApp.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }
        private void email_block_MouseDown(object sender, MouseButtonEventArgs e)
        {
            email_box.Focus();
        }

        private void email_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(email_box.Text) && email_box.Text.Length > 0)
            {
                email_block.Visibility = Visibility.Collapsed;
            }
            else
            {
                email_block.Visibility = Visibility.Visible;
            }
        }
        private void password_block_MouseDown(object sender, MouseButtonEventArgs e)
        {
            password_box.Focus();

        }
        private void password_box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(password_box.Password) && password_box.Password.Length > 0)
            {
                password_block.Visibility = Visibility.Collapsed;
            }
            else
            {
                password_block.Visibility = Visibility.Visible;
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Register());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Provera da li su svi podaci uneseni
            if (string.IsNullOrEmpty(email_box.Text) || string.IsNullOrEmpty(password_box.Password))
            {
                MessageBox.Show("Molimo popunite sva polja.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Autentifikacija korisnika
            var member = DatabaseManager.LoginMember(email_box.Text, password_box.Password, out string errorMessage);

            if (member != null)
            {
                MessageBox.Show("Logovanje uspešno!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                // Resetovanje polja
                email_box.Text = "";
                password_box.Password = "";

                // Navigacija nakon uspešnog logovanja
                //this.NavigationService.Navigate(new AddIncome(member));
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.Navigate(new Home(member));
            }
            else
            {
                MessageBox.Show(errorMessage, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

