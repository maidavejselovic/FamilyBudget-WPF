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
    /// Interaction logic for SavingsGoals.xaml
    /// </summary>
    public partial class SavingsGoals : Page
    {
        private Member _member;
        public SavingsGoals(Member member)
        {
            InitializeComponent();
            _member = member;
            LoadNavbar();
            LoadSavingsGoals();
        }
        private void LoadNavbar()
        {
            Navbar navbar = new Navbar(_member);
            NavbarFrame.Content = navbar;
        }
        private void LoadSavingsGoals()
        {
            List<SavingGoal> savingGoals = DatabaseManager.GetSavingGoals(_member.id);
            GoalsItemsControl.ItemsSource = savingGoals;
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
                    LoadSavingsGoals(); // Osvežavanje liste ciljeva štednje
                }
                else
                {
                    MessageBox.Show(errorMessage, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
}
}
