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
    /// Interaction logic for AddSavingGoal.xaml
    /// </summary>
    public partial class AddSavingGoal : Window
    {
        private Member _member;

        public double GoalAmount { get; private set; }
        public double CurrentAmount { get; private set; }
        public DateTime TargetDate { get; private set; }
        public string Description { get; private set; }

        public AddSavingGoal(Member member)
        {
            InitializeComponent();
            _member = member;
        }
        private void goalAmount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            goalAmount_box.Focus();
        }
        private void goalAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(goalAmount_box.Text) && goalAmount_box.Text.Length > 0)
            {
                goalAmount_block.Visibility = Visibility.Collapsed;
            }
            else
            {
                goalAmount_block.Visibility = Visibility.Visible;
            }
        }

        private void currentAmount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentAmount_box.Focus();
        }

        private void currentAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentAmount_box.Text) && currentAmount_box.Text.Length > 0)
            {
                currentAmount_block.Visibility = Visibility.Collapsed;
            }
            else
            {
                currentAmount_block.Visibility = Visibility.Visible;
            }
        }

        private void date_MouseDown(object sender, MouseButtonEventArgs e)
        {
            date_block.Visibility = Visibility.Collapsed;
            date_picker.Visibility = Visibility.Visible;
            date_picker.IsDropDownOpen = true; // Automatski otvori DatePicker kad se klikne na TextBlock
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (date_picker.SelectedDate.HasValue)
            {
                date_block.Text = date_picker.SelectedDate.Value.ToString("dd.MM.yyyy");
                date_block.Visibility = Visibility.Visible;
                date_picker.Visibility = Visibility.Collapsed;
            }
            else
            {
                date_block.Visibility = Visibility.Visible;
                date_picker.Visibility = Visibility.Collapsed;
            }
        }

        private void description_MouseDown(object sender, MouseButtonEventArgs e)
        {
            description_box.Focus();
        }
        private void description_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(description_box.Text) && description_box.Text.Length > 0)
            {
                description_block.Visibility = Visibility.Collapsed;
            }
            else
            {
                description_block.Visibility = Visibility.Visible;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(goalAmount_box.Text) || string.IsNullOrWhiteSpace(currentAmount_box.Text) ||
                string.IsNullOrWhiteSpace(date_picker.Text) || string.IsNullOrWhiteSpace(description_box.Text))
            {
                MessageBox.Show("Sva polja moraju biti popunjena.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(goalAmount_box.Text, out double goalAmount))
            {
                MessageBox.Show("Iznos cilja mora biti broj.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!double.TryParse(currentAmount_box.Text, out double currentAmount))
            {
                MessageBox.Show("Trenutni iznos mora biti broj.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            GoalAmount = goalAmount;
            CurrentAmount = currentAmount;
            TargetDate = date_picker.SelectedDate.Value;
            Description = description_box.Text;

            DialogResult = true;
            Close();
        }
    }
}