using FamilyBudgetApp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FamilyBudgetApp.Pages
{
    public partial class AddExpense : Page
    {
        private Member _member;
        private List<string> _categories;
        private List<Member> Members;
        private List<MemberExpense> MemberExpenses;


        public AddExpense(Member member)
        {
            InitializeComponent();
            _member = member;
            LoadNavbar();
            LoadExpenseData();
            LoadFamilyMembers();
            LoadCategories();

            // Set DataContext for this page to enable Binding
            this.DataContext = this;

            // Initialize MemberExpenses list
            MemberExpenses = new List<MemberExpense>();
            foreach (var familyMember in Members)
            {
                MemberExpenses.Add(new MemberExpense
                {
                    memberId = familyMember.id,
                    sharePercentage = 0,//Initial value; will be updated via binding
                    Member = familyMember // Postavljanje Member objekta
                });
            }
            // Set ItemsSource for membersSharesControl
            membersSharesControl.ItemsSource = MemberExpenses;
        }

        private void LoadFamilyMembers()
        {
            string errorMessage;
            if (_member.familyId != null)
            {
                int familyId = _member.familyId.Value; // Eksplicitna konverzija nullable int u int

                Members = DatabaseManager.GetFamilyMembers(familyId, out errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show($"Error loading family members: {errorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    Members = new List<Member>();
                }
            }
        }
        private void LoadNavbar()
        {
            Navbar navbar = new Navbar(_member);
            NavbarFrame.Content = navbar;
        }

        private void LoadExpenseData()
        {
            List<Expense> expenses = DatabaseManager.GetExpensesForMember(_member.id);
            incomeDataGrid.ItemsSource = expenses;
        }

        private void LoadCategories()
        {
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
            amount_block.Visibility = string.IsNullOrEmpty(amount_box.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void category_MouseDown(object sender, MouseButtonEventArgs e)
        {
            category_block.Visibility = Visibility.Collapsed;
            category_comboBox.Visibility = Visibility.Visible;
            category_comboBox.Focus();
        }
        private void CategoryComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategories(); // Poziv funkcije koja učitava kategorije
        }

        private void date_MouseDown(object sender, MouseButtonEventArgs e)
        {
            date_block.Visibility = Visibility.Collapsed;
            date_picker.Visibility = Visibility.Visible;
            date_picker.IsDropDownOpen = true; // Automatically open DatePicker when TextBlock is clicked
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
            description_block.Visibility = string.IsNullOrEmpty(description_box.Text) ? Visibility.Visible : Visibility.Collapsed;
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

            int expenseId = DatabaseManager.AddExpense(memberId, amount, category_comboBox.Text, date, description_box.Text, out errorMessage);

            if (expenseId > 0)
            {
                List<MemberExpense> memberExpenses = new List<MemberExpense>();

                foreach (MemberExpense memberExpense in MemberExpenses)
                {
                    double sharePercentage = memberExpense.sharePercentage;
                    double memberAmount = amount * (sharePercentage / 100.0);

                    memberExpenses.Add(new MemberExpense
                    {
                        memberId = memberExpense.memberId,
                        expenseId = expenseId,
                        sharePercentage = sharePercentage,
                        amount = memberAmount
                    });
                }

                if (DatabaseManager.AddMemberExpenses(memberExpenses))
                {
                    string sharePercentages = GetSharePercentagesText();

                    MessageBox.Show($"Udeli članova:\n{sharePercentages}\n\nVaš trošak je uspešno sačuvan.", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadExpenseData();
                }
                else
                {
                    MessageBox.Show(errorMessage ?? "Greška pri čuvanju udeljenih troškova.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(errorMessage ?? "Greška pri čuvanju troška.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateSharePercentages()
        {
            double totalPercentage = 0;
            foreach (var memberExpense in MemberExpenses)
            {
                totalPercentage += memberExpense.sharePercentage;
            }
            return totalPercentage == 100;
        }

        private string GetSharePercentagesText()
        {
            List<string> percentages = new List<string>();

            foreach (MemberExpense memberExpense in MemberExpenses)
            {
                percentages.Add($"{memberExpense.Member.firstName}: {memberExpense.sharePercentage}%");
            }

            return string.Join("\n", percentages);
        }

    }
}
