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
        //private void LoadSavingsGoals()
        //{
        //    List<SavingGoal> savingGoals = DatabaseManager.GetSavingGoals(_member.id);
        //    GoalsItemsControl.ItemsSource = savingGoals;
        //}


        /* private void LoadSavingsGoals()
         {
             string errorMessage;

             // Dohvati trenutni budžet porodice pomoću funkcije GetFamilyBudget
             double currentBudget = DatabaseManager.GetFamilyBudget(_member.id, out errorMessage);

             if (!string.IsNullOrEmpty(errorMessage))
             {
                 MessageBox.Show(errorMessage, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                 return;
             }

             // Dohvati ciljeve štednje člana porodice pomoću funkcije GetSavingGoals
             List<SavingGoal> savingGoals = DatabaseManager.GetSavingGoals(_member.id, out errorMessage);

             if (!string.IsNullOrEmpty(errorMessage))
             {
                 MessageBox.Show(errorMessage, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                 return;
             }

             foreach (var goal in savingGoals)
             {
                 string newStatus;
                 string statusMessage = DatabaseManager.CheckSavingsGoalStatus(goal, currentBudget, out newStatus);

                 if (newStatus != goal.status)
                 {
                     // Ažuriraj status u bazi podataka
                     DatabaseManager.UpdateSavingGoalStatus(goal.id, newStatus);
                 }

                 if (!string.IsNullOrEmpty(statusMessage))
                 {
                     MessageBox.Show(statusMessage, "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                 }
             }

             // Prikazivanje ciljeva štednje sa budžetom
             GoalsItemsControl.ItemsSource = savingGoals.Select(goal => new
             {
                 goal.goalAmount,
                 goal.description,
                 goal.targetDate,
                 budgetAmount = currentBudget, // Prikazuje budžet kao "budgetAmount"
                 goal.status 
             }).ToList();
         }*/
        private void LoadSavingsGoals()
        {
            string errorMessage;

            if (!_member.familyId.HasValue)
            {
                MessageBox.Show("Family ID nije dostupan.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int familyId = _member.familyId.Value;

            // Dohvati ciljeve štednje za sve članove porodice pomoću funkcije GetFamilySavingGoals
            List<SavingGoal> familySavingGoals = DatabaseManager.GetFamilySavingGoals(familyId, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Dohvati trenutni budžet porodice
            double currentBudget = DatabaseManager.GetFamilyBudget(familyId, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var goal in familySavingGoals)
            {
                string newStatus;
                string statusMessage = DatabaseManager.CheckSavingsGoalStatus(goal, currentBudget, out newStatus);

                if (newStatus != goal.status)
                {
                    // Ažuriraj status u bazi podataka
                    DatabaseManager.UpdateSavingGoalStatus(goal.id, newStatus);
                }

                if (!string.IsNullOrEmpty(statusMessage))
                {
                    MessageBox.Show(statusMessage, "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            // Prikazivanje ciljeva štednje sa budžetom
            GoalsItemsControl.ItemsSource = familySavingGoals.Select(goal => new
            {
                goal.goalAmount,
                goal.description,
                goal.targetDate,
                budgetAmount = currentBudget, // Prikazuje budžet kao "budgetAmount"
                goal.status
            }).ToList();
        }



        private void AddSavingGoalsButton_Click(object sender, RoutedEventArgs e)
        {
            AddSavingGoal addSavingGoal = new AddSavingGoal(_member);
            if (addSavingGoal.ShowDialog() == true)
            {
                string errorMessage;

                if (DatabaseManager.AddSavingGoal(_member.id, addSavingGoal.GoalAmount, addSavingGoal.TargetDate, addSavingGoal.Description, out errorMessage))
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
