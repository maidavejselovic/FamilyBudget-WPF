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
        private List<Member> FamilyMembers;
        private List<MemberIncome> MemberIncomes;

        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }


        public AddIncome(Member member)
        {
            InitializeComponent();
            _member = member;
            LoadNavbar();
            LoadIncomeData(); // Dodaj ovu liniju za učitavanje podataka
            LoadCategories();
            LoadFamilyMembers();

            MaxDate = DateTime.Today;
            MinDate = DateTime.Today.AddDays(-30);

            // Postavite DataContext za ovu stranicu da omogućite Binding
            this.DataContext = this;

            // Initialize MemberIncomes list
            MemberIncomes = new List<MemberIncome>();
            foreach (var familyMember in FamilyMembers)
            {
                MemberIncomes.Add(new MemberIncome
                {
                    memberId = familyMember.id,
                    sharePercentage = 0,//Initial value; will be updated via binding
                    Member = familyMember // Postavljanje Member objekta
                });
            }
            // Set ItemsSource for membersSharesControl
            membersSharesControl.ItemsSource = MemberIncomes;
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
        private void LoadFamilyMembers()
        {
            string errorMessage;
            if (_member.familyId != null)
            {
                int familyId = _member.familyId.Value; // Eksplicitna konverzija nullable int u int

                FamilyMembers = DatabaseManager.GetFamilyMembers1(familyId, out errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show($"Error loading family members: {errorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    FamilyMembers = new List<Member>();
                }
            }
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
                MessageBox.Show("Nevažeći datum.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!ValidateSharePercentages())
            {
                MessageBox.Show("Ukupni udeo mora biti 100%.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int memberId = _member.id;

            List<MemberIncome> memberIncomes = new List<MemberIncome>();

            foreach (MemberIncome memberIncome in MemberIncomes)
            {
                double sharePercentage = memberIncome.sharePercentage;
                double memberAmount = amount * (sharePercentage / 100.0);

                memberIncomes.Add(new MemberIncome
                {
                    memberId = memberIncome.memberId,
                    sharePercentage = sharePercentage,
                    memberAmount = memberAmount
                });
            }

            if (DatabaseManager.AddIncomeAndMemberIncomes(memberId, amount, category_comboBox.Text, date, description_box.Text, memberIncomes, out errorMessage))
            {
                MessageBox.Show("Vaš prihod je uspešno sačuvan.", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadIncomeData();
            }
            else
            {
                MessageBox.Show(errorMessage ?? "Greška pri čuvanju prihoda i udeljenih prihoda.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool ValidateSharePercentages()
        {
            double totalPercentage = 0;
            foreach (var memberIncome in MemberIncomes)
            {
                totalPercentage += memberIncome.sharePercentage;
            }
            return totalPercentage == 100;
        }
    }
}
