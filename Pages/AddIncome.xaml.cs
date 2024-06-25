using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AddIncome.xaml
    /// </summary>
    public partial class AddIncome : Page
    {
        private Member _member;
        private List<string> _categories;

        public AddIncome(Member member)
        {
            InitializeComponent();
            _member = member;
            LoadNavbar();
            LoadIncomeData(); // Dodaj ovu liniju za učitavanje podataka
            LoadCategories();
        }

        private void LoadNavbar()
        {
            Navbar navbar = new Navbar(_member);
            NavbarFrame.Content = navbar;
        }

        private void LoadIncomeData()
        {
            List<Income> incomes = DatabaseManager.GetIncomesForMember(_member.id);
            incomeDataGrid.ItemsSource = incomes;
        }
        private void LoadCategories()
        {
            // Definišimo kategorije unapred
            _categories = new List<string>
            {
                "Zdravlje",
                "Plata",
                "Namirnice",
                "Investicija",
                "Šminka",
                "Prevoz",
                "Poklon",
                "Ostalo"
            };

            category_comboBox.ItemsSource = _categories;
        }

        private void amount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            amount_box.Focus();
        }
        private void amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(amount_box.Text) && amount_box.Text.Length > 0)
            {
                amount_block.Visibility = Visibility.Collapsed;
            }
            else
            {
                amount_block.Visibility = Visibility.Visible;
            }
        }

        private void category_MouseDown(object sender, MouseButtonEventArgs e)
        {         
            category_block.Visibility = Visibility.Collapsed;
            category_comboBox.Visibility = Visibility.Visible;
            category_comboBox.Focus();
        }
        private void CategoryComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategories();
        }

        private void date_MouseDown(object sender, MouseButtonEventArgs e)
        {
            date_block.Visibility = Visibility.Collapsed;
            date_picker.Visibility = Visibility.Visible;
            date_picker.IsDropDownOpen = true; // Automatski otvori DatePicker kad se klikne na TextBlock
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (date_picker.SelectedDate.HasValue)
            {
                // Format the date to Serbian format
                date_block.Text = date_picker.SelectedDate.Value.ToString("dd.MM.yyyy", new CultureInfo("sr-SP"));
                date_block.Visibility = Visibility.Visible;
                date_picker.Visibility = Visibility.Collapsed;
            }
            else
            {
                date_block.Visibility = Visibility.Visible;
                date_picker.Visibility = Visibility.Collapsed;
            }
        }

        private void description_MouseDown(object sender, MouseButtonEventArgs e)
        {
            description_box.Focus();
        }
        private void description_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(description_box.Text) && description_box.Text.Length > 0)
            {
                description_block.Visibility = Visibility.Collapsed;
            }
            else
            {
                description_block.Visibility = Visibility.Visible;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage;

            if (string.IsNullOrWhiteSpace(amount_box.Text) || string.IsNullOrWhiteSpace(category_comboBox.Text) ||
                string.IsNullOrWhiteSpace(date_picker.Text) || string.IsNullOrWhiteSpace(description_box.Text))
            {
                MessageBox.Show("Sva polja moraju biti popunjena.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(amount_box.Text, out double amount))
            {
                MessageBox.Show("Iznos mora biti broj.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!DateTime.TryParse(date_picker.Text, out DateTime date))
            {
                MessageBox.Show("Neispravan datum.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int memberId = _member.id;

            if (DatabaseManager.AddIncome(memberId, amount, category_comboBox.Text, date, description_box.Text, out errorMessage))
            {
                MessageBox.Show("Vaš prihod je uspešno dodat.", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadIncomeData();
            }
            else
            {
                MessageBox.Show(errorMessage, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
