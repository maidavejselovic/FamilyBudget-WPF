using FamilyBudgetApp.Windows;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
            LoadFamilyNames();
        }

        private void LoadFamilyNames()
        {
            string errorMessage;
            List<string> familyNames = DatabaseManager.GetFamilyNames(out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            familyNameComboBox.ItemsSource = familyNames;
        }
        private void AddFamilyButton_Click(object sender, RoutedEventArgs e)
        {
            AddFamily addFamily = new AddFamily();
            if (addFamily.ShowDialog() == true)
            {
                LoadFamilyNames(); // Ponovo učitajte porodice
                familyNameComboBox.SelectedItem = addFamily.FamilyName; // Odaberite novu porodicu
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Provera da li su svi podaci uneseni
            if (string.IsNullOrEmpty(firstName_box.Text) || string.IsNullOrEmpty(lastName_box.Text) ||
                string.IsNullOrEmpty(email_box.Text) || string.IsNullOrEmpty(password_box.Password) ||
                familyNameComboBox.SelectedItem == null)
                
            {
                MessageBox.Show("Molimo popunite sva polja.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Validacija email adrese
            if (!System.Text.RegularExpressions.Regex.IsMatch(email_box.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Molimo unesite ispravnu email adresu.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Provera da li se lozinke poklapaju
            if (password_box.Password != rptPassword_box.Password)
            {
                MessageBox.Show("Lozinke se ne poklapaju.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int familyId = new DatabaseManager().GetSelectedFamilyId(familyNameComboBox);
            if (familyId == -1)
            {
                MessageBox.Show("Molimo odaberite porodicu.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string status = "odobren"; // Početni status je odobren
            string errorMessage;
            // Provera da li postoji već neki član u odabranoj porodici
            List<Member> familyMembers = DatabaseManager.GetFamilyMembers(familyId, out errorMessage);
            if (familyMembers.Any())
            {
                status = "na čekanju"; // Ako postoje drugi članovi u porodici, status je na čekanju
            }

            // Registracija novog člana
            if (DatabaseManager.RegisterMember(firstName_box.Text, lastName_box.Text, email_box.Text, password_box.Password, familyNameComboBox.SelectedItem.ToString(), status, out errorMessage))
            {
                MessageBox.Show("Registracija uspešna!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                // Resetovanje polja
                firstName_box.Text = "";
                lastName_box.Text = "";
                email_box.Text = "";
                password_box.Password = "";
                rptPassword_box.Password = "";
                familyNameComboBox.SelectedIndex = -1;

                // Navigacija na Login stranicu
                this.NavigationService.Navigate(new Login());
            }
            else
            {
                MessageBox.Show(errorMessage, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //block  text
        private void firstName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            firstName_box.Focus();
        }

        //box  txt 
        private void firstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(firstName_box.Text) && firstName_box.Text.Length > 0)
            {
                firstName_block.Visibility = Visibility.Collapsed;
            }
            else
            {
                firstName_block.Visibility = Visibility.Visible;
            }

        }
        private void lastName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lastName_box.Focus();
        }

        private void lastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lastName_box.Text) && lastName_box.Text.Length > 0)
            {
                lastName_block.Visibility = Visibility.Collapsed;
            }
            else
            {
                lastName_block.Visibility = Visibility.Visible;
            }
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

        private void rptPassword_block_MouseDown(object sender, MouseButtonEventArgs e)
        {
            rptPassword_box.Focus();
        }

        private void rptPassword_box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(rptPassword_box.Password) && rptPassword_box.Password.Length > 0)
            {
                rptPassword_block.Visibility = Visibility.Collapsed;
            }
            else
            {
                rptPassword_block.Visibility = Visibility.Visible;
            }
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }
    }
}
