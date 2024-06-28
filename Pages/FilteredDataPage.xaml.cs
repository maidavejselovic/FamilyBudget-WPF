using FamilyBudgetApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FamilyBudgetApp.Pages
{
    public partial class FilteredDataPage : Page
    {
        private Member _member;

        public FilteredDataPage(Member member)
        {
            InitializeComponent();
            DataContext = new FilteredDataViewModel(); // Postavljanje DataContext-a na instancu FilteredDataViewModel-a

            _member = member;
            LoadNavbar();
            LoadCategories();
            LoadAllData(); // Učitaj sve transakcije na početku
        }

        private void LoadNavbar()
        {
            Navbar navbar = new Navbar(_member);
            NavbarFrame.Content = navbar;
        }

        private void LoadCategories()
        {
            var categories = DatabaseManager.GetCategoriesForMember(_member.id);
            categoryComboBox.ItemsSource = categories;
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryComboBox.SelectedItem is string selectedCategory)
            {
                LoadFilteredData(selectedCategory);
            }
            else
            {
                LoadAllData(); // Ako ništa nije izabrano, učitaj sve transakcije
            }
        }

        private void LoadAllData()
        {
            List<Expense> expenses = DatabaseManager.GetExpensesForMember(_member.id);
            List<Income> incomes = DatabaseManager.GetIncomesForMember(_member.id);

            var combinedList = expenses.Select(expense => new
            {
                Date = expense.date,
                Description = expense.description,
                Amount = -expense.amount, // Dodavanje minusa ispred iznosa
                Category = expense.category, // Dodajte kategoriju
                Type = "Trošak"
            })
            .Concat(incomes.Select(income => new
            {
                Date = income.date,
                Description = income.description,
                Amount = income.amount,
                Category = income.category, // Dodajte kategoriju
                Type = "Prihod"
            }))
            .OrderBy(item => item.Date)
            .ToList();

            transactionsListView.ItemsSource = combinedList;
        }

        private void LoadFilteredData(string category)
        {
            List<Expense> expenses = DatabaseManager.GetExpensesByCategory(_member.id, category);
            List<Income> incomes = DatabaseManager.GetIncomesByCategory(_member.id, category);

            var combinedList = expenses.Select(expense => new
            {
                Date = expense.date,
                Description = expense.description,
                Amount = -expense.amount, // Dodavanje minusa ispred iznosa
                Category = category, // Dodajte kategoriju
                Type = "Trošak"
            })
            .Concat(incomes.Select(income => new
            {
                Date = income.date,
                Description = income.description,
                Amount = income.amount,
                Category = category, // Dodajte kategoriju
                Type = "Prihod"
            }))
            .OrderBy(item => item.Date)
            .ToList();

            transactionsListView.ItemsSource = combinedList;
        }
    }
}
