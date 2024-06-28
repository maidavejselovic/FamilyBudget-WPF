using FamilyBudgetApp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FamilyBudgetApp.Pages
{
    public partial class AddExpense : Page
    {
        private Member _member;
        private List<string> _categories;
        private List<Member> Members;

        public AddExpense(Member member)
        {
            InitializeComponent();
            _member = member;
            LoadNavbar();
            LoadExpenseData();
            LoadFamilyMembers(); // Load members from database

            // Set DataContext for this page to enable Binding
            this.DataContext = this;

            // Load categories on initialization
            LoadCategories();

            // Set ItemsSource for membersSharesControl
            membersSharesControl.ItemsSource = Members;
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
                "Health",
                "Salary",
                "Groceries",
                "Investment",
                "Makeup",
                "Transportation",
                "Gift",
                "Other"
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

            // Input validation
            if (string.IsNullOrWhiteSpace(amount_box.Text) || string.IsNullOrWhiteSpace(category_comboBox.Text) ||
                string.IsNullOrWhiteSpace(date_picker.Text) || string.IsNullOrWhiteSpace(description_box.Text))
            {
                MessageBox.Show("All fields must be filled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Amount validation
            if (!double.TryParse(amount_box.Text, out double amount))
            {
                MessageBox.Show("Amount must be a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Date validation
            if (!DateTime.TryParse(date_picker.Text, out DateTime date))
            {
                MessageBox.Show("Invalid date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int memberId = _member.id; // Assuming _member.id is of type int

            // Save expense in the database
            int expenseId = DatabaseManager.AddExpenseAndGetId(memberId, amount, category_comboBox.Text, date, description_box.Text, out errorMessage);

            if (expenseId > 0)
            {
                // Save shares for each family member
                List<MemberExpense> memberExpenses = new List<MemberExpense>();

                foreach (MemberExpense memberExpense in memberExpenses)
                {
                    double sharePercentage = memberExpense.sharePercentage; // Default vrednost ako je null

                    // Provera da li je udeo validan
                    if (sharePercentage < 0 || sharePercentage > 100)
                    {
                        MessageBox.Show("Udeo mora biti između 0% i 100%.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Izračunaj iznos za ovog člana
                    double memberAmount = amount * (sharePercentage / 100.0);

                    // Dodaj u listu za čuvanje u bazi
                    memberExpenses.Add(new MemberExpense
                    {
                        memberId = memberExpense.memberId,
                        expenseId = expenseId,
                        sharePercentage = sharePercentage,
                        amount = memberAmount
                    });
                }


                // Save in the database
                if (DatabaseManager.AddMemberExpenses(memberExpenses))
                {
                    MessageBox.Show("Vaš toršak je uspešno sačuvan", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadExpenseData(); // Reload expense data
                }
                else
                {
                    MessageBox.Show(errorMessage ?? "Greška", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
