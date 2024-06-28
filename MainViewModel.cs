using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace FamilyBudgetApp.ViewModels
{
    public class FilteredDataViewModel : INotifyPropertyChanged
    {
        public SeriesCollection ChartSeries { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> FormatYLabel { get; set; }

        public FilteredDataViewModel()
        {
            FormatYLabel = value => value.ToString("N2");
            LoadData();
        }

        private void LoadData()
        {
            // Primer učitavanja podataka iz baze ili nekog izvora
            List<Expense> expenses = GetExpensesFromDatabase();
            List<Income> incomes = GetIncomesFromDatabase();

            // Grupisanje troškova po kategoriji i sumiranje iznosa
            var groupedExpenses = expenses
                .GroupBy(e => e.category)
                .Select(g => new { Category = g.Key, TotalAmount = g.Sum(e => e.amount) })
                .ToList();

            // Grupisanje prihoda po kategoriji i sumiranje iznosa
            var groupedIncomes = incomes
                .GroupBy(i => i.category)
                .Select(g => new { Category = g.Key, TotalAmount = g.Sum(i => i.amount) })
                .ToList();

            // Spojite sve kategorije iz troškova i prihoda
            var allCategories = groupedExpenses.Select(g => g.Category)
                .Union(groupedIncomes.Select(g => g.Category))
                .Distinct()
                .ToList();

            // Priprema vrednosti za troškove i prihode po kategorijama
            var expenseValues = new ChartValues<double>();
            var incomeValues = new ChartValues<double>();

            foreach (var category in allCategories)
            {
                var expense = groupedExpenses.FirstOrDefault(g => g.Category == category);
                var income = groupedIncomes.FirstOrDefault(g => g.Category == category);

                expenseValues.Add(expense?.TotalAmount ?? 0);
                incomeValues.Add(income?.TotalAmount ?? 0);
            }

            // Kreiranje serije za grafikon
            ChartSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Troškovi",
                    Values = expenseValues,
                    Fill = System.Windows.Media.Brushes.Red // Postavljanje boje za troškove
                },
                new ColumnSeries
                {
                    Title = "Prihodi",
                    Values = incomeValues,
                    Fill = System.Windows.Media.Brushes.Green // Postavljanje boje za prihode
                }
            };

            // Postavljanje etiketa za osu X (kategorije)
            Labels = allCategories;

            OnPropertyChanged(nameof(ChartSeries));
            OnPropertyChanged(nameof(Labels));
        }

        // Primer metode za dobijanje troškova iz baze (može se prilagoditi vašem okruženju)
        private List<Expense> GetExpensesFromDatabase()
        {
            using (var context = new budgetEntities()) // Zamenite sa vašim DbContext-om
            {
                return context.Expenses.ToList();
            }
        }

        // Primer metode za dobijanje prihoda iz baze (može se prilagoditi vašem okruženju)
        private List<Income> GetIncomesFromDatabase()
        {
            using (var context = new budgetEntities()) // Zamenite sa vašim DbContext-om
            {
                return context.Incomes.ToList();
            }
        }

        // Implementacija INotifyPropertyChanged interfejsa
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
