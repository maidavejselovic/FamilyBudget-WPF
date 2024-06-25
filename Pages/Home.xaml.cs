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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private Member _member;

        public Home(Member member)
        {
            InitializeComponent();
            _member = member;
            LoadNavbar();
            LoadSavingGoals();
            LoadTotalBudget();

        }

        private void LoadNavbar()
        {
            Navbar navbar = new Navbar(_member);
            NavbarFrame.Content = navbar;
        }
        private void LoadSavingGoals()
        {
            List<SavingGoal> savingGoals = DatabaseManager.GetSavingGoals(_member.id);
            GoalsItemsControl.ItemsSource = savingGoals;
        }
        private void LoadTotalBudget()
        {
            string errorMessage;
            double budget = DatabaseManager.GetMemberBudget(_member.id, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
            {
                TotalBudgetTextBlock.Text = $"Ukupan budžet: \n {budget} RSD";
            }
            else
            {
                TotalBudgetTextBlock.Text = $"Greška prilikom učitavanja budžeta: {errorMessage}";
            }
        }

        private void AddSavingGoalsButton_Click(object sender, RoutedEventArgs e)
        {
            AddSavingGoal addSavingGoal = new AddSavingGoal(_member);
            if (addSavingGoal.ShowDialog() == true)
            {
                string errorMessage;

                if (DatabaseManager.AddSavingGoal(_member.id, addSavingGoal.GoalAmount, addSavingGoal.CurrentAmount, addSavingGoal.TargetDate, addSavingGoal.Description, out errorMessage))
                {
                    MessageBox.Show("Cilj štednje uspešno dodat!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadSavingGoals(); // Osvežavanje liste ciljeva štednje
                }
                else
                {
                    MessageBox.Show(errorMessage, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

}
