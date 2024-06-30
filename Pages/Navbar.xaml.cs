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
    /// Interaction logic for Navbar.xaml
    /// </summary>
    public partial class Navbar : Page
    {
        private Member _member;

        public Navbar(Member member)
        {
            InitializeComponent();
            _member = member;
            DisplayMemberDetails();
        }

        private void DisplayMemberDetails()
        {
            firstName_block.Text = _member.firstName;
            lastName_block.Text = _member.lastName;
        }
        private void Hyperlink_Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Navigate(new Home(_member));

        }
        private void Hyperlink_Expense_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new AddExpense(_member));
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Navigate(new AddExpense(_member));
        }
        private void Hyperlink_Income_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Navigate(new AddIncome(_member));
        }
        private void Hyperlink_Family_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Navigate(new FamilyMembers(_member));
        }
        private void Hyperlink_MyBudget_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Navigate(new MyBudget(_member));
        }
        private void Hyperlink_SavingsGoals_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Navigate(new SavingsGoals(_member));
        }
        private void Hyperlink_Logout_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Login());
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Navigate(new Login());
        }
    }
}
