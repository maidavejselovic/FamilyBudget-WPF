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
using System.Windows.Shapes;

namespace FamilyBudgetApp.Windows
{
    /// <summary>
    /// Interaction logic for AddFamily.xaml
    /// </summary>
    public partial class AddFamily : Window
    {
        public string FamilyName { get; private set; }

        public AddFamily()
        {
            InitializeComponent();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            if (DatabaseManager.AddFamily(familyNameTextBox.Text, out string errorMessage))
            {
                MessageBox.Show("Porodica uspešno dodata!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Naziv porodice ne može biti prazan.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
