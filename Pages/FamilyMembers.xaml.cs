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
            CheckMemberStatusAndLoadContent();
            LoadFamilyMembers();
        }
        private void LoadNavbar()
        {
            Navbar navbar = new Navbar(_member);
            NavbarFrame.Content = navbar;
        }
        private void CheckMemberStatusAndLoadContent()
        {
            if (_member.status == "na čekanju")
            {
                // Ako je korisnik na čekanju, prikažite poruku i sakrijte DataGrid
                pendingMessageTextBlock.Text = "Prihvatanje u porodicu na čekanju.";
                pendingMessageTextBlock.Visibility = Visibility.Visible;
                incomeDataGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Ako je korisnik odobren, učitajte članove porodice
                pendingMessageTextBlock.Visibility = Visibility.Collapsed;
                incomeDataGrid.Visibility = Visibility.Visible;
                LoadFamilyMembers();
            }
        }
        private void LoadFamilyMembers()
        {
            if (_member.familyId != null)
            {
                int familyId = _member.familyId.Value; // Eksplicitna konverzija nullable int u int
                List<Member> familyMembers = DatabaseManager.GetFamilyMembers(familyId, out string errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    // Obradi grešku ako je potrebno
                }
                else
                {
                    // Ako nema greške, postavi članove porodice u izvor podataka za DataGrid
                    incomeDataGrid.ItemsSource = familyMembers;
                }
            }
        }
        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button?.CommandParameter is int memberId)
            {
                // Pozovite funkciju za ažuriranje statusa u bazi podataka na "Odobren"
                if (DatabaseManager.ApproveMember(memberId))
                {
                    // Osvežite prikaz u DataGrid-u
                    RefreshDataGrid();
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
                // Pozovite funkciju za brisanje člana porodice iz baze podataka
                if (DatabaseManager.RejectMember(memberId))
                {
                    // Osvežite prikaz u DataGrid-u
                    RefreshDataGrid();
                }
                else
                {
                    MessageBox.Show("Član porodice nije pronađen.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void RefreshDataGrid()
        {
            LoadFamilyMembers(); // Primer funkcije za osvežavanje podataka, prilagodite je vašem scenariju
        }


    }
}

