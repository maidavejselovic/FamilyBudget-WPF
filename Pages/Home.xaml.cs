using FamilyBudgetApp.Windows;
using LiveCharts.Wpf;
using LiveCharts;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private Member _member;
        private Family _family;

        public Home(Member member)
        {
            InitializeComponent();
            _member = member;
            LoadNavbar();
            //LoadTotalBudget();
            LoadTotalFamilyBudget();
            LoadCategories();
            LoadAllData(); // Učitaj sve transakcije na početku
            LoadChartData();
        }

        private void LoadNavbar()
        {
             Navbar navbar = new Navbar(_member);
            NavbarFrame.Content = navbar;
        }
        private void LoadTotalFamilyBudget()
        {
            string errorMessage;
            if (_member.familyId != null)
            {
                int familyId = _member.familyId.Value; // Eksplicitna konverzija nullable int u int
                double familyBudget = DatabaseManager.GetFamilyBudget(familyId, out errorMessage);

                TotalFamilyBudgetTextBlock.Text = $"Porodični budžet: \n {familyBudget} RSD";
            }
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
            string errorMessage;
            if (_member.familyId != null)
            {
                int familyId = _member.familyId.Value; // Eksplicitna konverzija nullable int u int

                List<Expense> expenses = DatabaseManager.GetExpensesForMember(familyId);
                List<Income> incomes = DatabaseManager.GetIncomesForMember(familyId);

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
        }


        private void LoadFilteredData(string category)
        {
            string errorMessage;
            if (_member.familyId != null)
            {
                int familyId = _member.familyId.Value; // Eksplicitna konverzija nullable int u int

                List<Expense> expenses = DatabaseManager.GetExpensesByCategory(familyId, category);
                List<Income> incomes = DatabaseManager.GetIncomesByCategory(familyId, category);

                var combinedList = expenses.Select(expense => new
                {
                    Date = expense.date,
                    Description = expense.description,
                    Amount = -expense.amount, // minus ispred iznosa
                    Category = category,
                    Type = "Trošak"
                })
                .Concat(incomes.Select(income => new
                {
                    Date = income.date,
                    Description = income.description,
                    Amount = income.amount,
                    Category = category,
                    Type = "Prihod"
                }))
                .OrderByDescending(item => item.Date)
                .ToList();

                transactionsListView.ItemsSource = combinedList;
            }
        }
        private void LoadChartData()
        {
            // int memberId = _member.id;
            string errorMessage;
            if (_member.familyId != null)
            {
                int familyId = _member.familyId.Value; // Eksplicitna konverzija nullable int u int


                // Uzimanje svih kategorija za datog člana
                List<string> categories = DatabaseManager.GetCategoriesForMember(familyId);

                // Uzimanje ukupnih troškova po kategorijama
                SeriesCollection expensesSeries = new SeriesCollection();
                foreach (string category in categories)
                {
                    List<Expense> expenses = DatabaseManager.GetExpensesByCategory(familyId, category);
                    double totalExpense = expenses.Sum(e => e.amount);
                    expensesSeries.Add(new PieSeries
                    {
                        Title = category,
                        Values = new ChartValues<double> { totalExpense },
                        DataLabels = true,
                        LabelPoint = chartPoint => chartPoint.Participation > 0 ? $"{chartPoint.Participation:P}" : string.Empty
                    });
                }

                // Uzimanje ukupnih prihoda po kategorijama
                SeriesCollection incomesSeries = new SeriesCollection();
                foreach (string category in categories)
                {
                    List<Income> incomes = DatabaseManager.GetIncomesByCategory(familyId, category);
                    double totalIncome = incomes.Sum(i => i.amount);
                    incomesSeries.Add(new PieSeries
                    {
                        Title = category,
                        Values = new ChartValues<double> { totalIncome },
                        DataLabels = true,
                        LabelPoint = chartPoint => chartPoint.Participation > 0 ? $"{chartPoint.Participation:P}" : string.Empty
                    });
                }

                // Postavljanje serija za troškove grafikona
                expensesPieChart.Series = expensesSeries;

                // Postavljanje serija za prihode grafikona
                incomesPieChart.Series = incomesSeries;
            }
        }

    }

}
