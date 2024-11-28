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
    /// Interaction logic for MyBudget.xaml
    /// </summary>
    public partial class MyBudget : Page
    {
        private Member _member;
        public MyBudget(Member member)
        {
            InitializeComponent();
            _member = member;
            LoadNavbar();
            LoadAllData();
            LoadTotalBudget();
        }

        private void LoadNavbar()
        {
            Navbar navbar = new Navbar(_member);
            NavbarFrame.Content = navbar;
        }

        private void LoadTotalBudget()
        {
            string errorMessage;
            double budget = DatabaseManager.GetMemberBudget(_member.id, out errorMessage);

            TotalBudgetTextBlock.Text = $"Moj budžet: \n {budget} RSD";
        }

        private void LoadAllData()
        {
            // List<Income> incomes = DatabaseManager.GetIncomesForMember(_member.id);
            //List<Expense> expenses = DatabaseManager.GetExpensesForMember(_member.id);

            List<MemberIncome> memberIncomes = DatabaseManager.GetMemberIncomesForMember(_member.id);
            List<MemberExpense> memberExpenses = DatabaseManager.GetMemberExpensesForMember(_member.id);

            var combinedList = memberExpenses.Select(me => new
            {
                Date = me.Expense.date,
                Description = me.Expense.description,
                MemberAmount = -me.memberAmount,   //minusa ispred iznosa
                Category = me.Expense.category,
                Type = "Trošak"
            })
            .Concat(memberIncomes.Select(mi => new
            {
                Date = mi.Income.date,
                Description = mi.Income.description,
                MemberAmount = mi.memberAmount,
                Category = mi.Income.category,
                Type = "Prihod"
            }))
           .OrderByDescending(item => item.Date) // Sortiranje u opadajućem redosledu
           .ToList();


            transactionsListView.ItemsSource = combinedList;
        }
    }
}