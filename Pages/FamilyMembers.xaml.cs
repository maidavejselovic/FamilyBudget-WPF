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
    /// Interaction logic for FamilyMembers.xaml
    /// </summary>
    public partial class FamilyMembers : Page
    {
        private Member _member;
        public FamilyMembers(Member member)
        {
            InitializeComponent();
            _member = member;
            LoadNavbar();
            LoadFamilyMembers();
        }
        private void LoadNavbar()
        {
            Navbar navbar = new Navbar(_member);
            NavbarFrame.Content = navbar;
        }
        private void LoadFamilyMembers()
        {
            if (_member.familyId != null)
            {
                int familyId = _member.familyId.Value; // Eksplicitna konverzija nullable int u int
                List<Member> familyMembers = DatabaseManager.GetFamilyMembers(familyId, out string errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                {

                }
                else
                {
                    incomeDataGrid.ItemsSource = familyMembers;
                }
            }
        }
       
        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button?.CommandParameter is int memberId)
            {
                if (DatabaseManager.ApproveMember(memberId))
                {
                    LoadFamilyMembers();
                }
                else
                {
                    MessageBox.Show("Član porodice nije pronađen.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button?.CommandParameter is int memberId)
            {
                if (DatabaseManager.RejectMember(memberId))
                {
                    LoadFamilyMembers();
                }
                else
                {
                    MessageBox.Show("Član porodice nije pronađen.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

