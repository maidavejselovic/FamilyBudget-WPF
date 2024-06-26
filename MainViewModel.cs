using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FamilyBudgetApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public SeriesCollection ChartSeries { get; set; }
        public List<string> Labels { get; set; }

        public MainViewModel()
        {
            LoadData(); // Metoda za učitavanje podataka
        }

        private void LoadData()
        {
            // Primer učitavanja podataka iz baze ili nekog izvora
            List<Expense> expenses = GetExpensesFromDatabase(); // Metoda za dobijanje troškova iz baze

            // Kreiranje serije za grafikon
            ChartSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Expenses",
                    Values = new ChartValues<double>(expenses.Select(e => e.amount))
                }
            };

            // Postavljanje etiketa za osu X (kategorije)
            Labels = expenses.Select(e => e.category).ToList();
        }

        // Primer metode za dobijanje troškova iz baze (može se prilagoditi vašem okruženju)
        private List<Expense> GetExpensesFromDatabase()
        {
            using (var context = new budgetEntities()) // Zamenite sa vašim DbContext-om
            {
                return context.Expenses.ToList();
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
