using Sports_Coaches.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddDiplomas.xaml
    /// </summary>
    public partial class AddDiplomas : Window
    {
        private List<Diploma> diplomas;
        private Context db;
        public List<Diploma> selectedDimplomas;
        
        public AddDiplomas(List<Diploma> selectedDimplomas)
        {
            InitializeComponent();
            db = new Context();
            diplomas = db.Diplomas.ToList();
            this.selectedDimplomas = selectedDimplomas;
            if (selectedDimplomas.Count > 0) SetSelected();
            else AddComboBox();
        }

        private void AddComboBox(string text = "")
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Margin = new Thickness(10);
            comboBox.FontSize = 14;
            comboBox.Text = text;
            comboBox.IsEditable = true;
            comboBox.ItemsSource = diplomas;
            comboBox.DisplayMemberPath = "Name";
            MaterialDesignThemes.Wpf.HintAssist.SetHint(comboBox, "Навчальний заклад");

            diplomasSP.Children.Add(comboBox);
        }
        

        private void SetSelected()
        {
            foreach (Diploma diploma in selectedDimplomas)
            {
                AddComboBox(diploma.Name);
            }
        }

        private void AddSelectedDiplomas()
        {
            selectedDimplomas.Clear();
            foreach (ComboBox cb in diplomasSP.Children)
            {
                if (selectedDimplomas.Where(d => d.Name.Equals(cb.Text)).Any()) continue;
                else if (diplomas.Where(d => d.Name.Equals(cb.Text)).Any())
                {
                    selectedDimplomas.Add(diplomas.Where(d => d.Name.Equals(cb.Text)).FirstOrDefault());
                }
                else
                {
                    if (cb.Text.Length != 0)
                    {
                        Diploma diploma = new Diploma() { Name = cb.Text };
                        selectedDimplomas.Add(diploma);
                        db.Diplomas.Add(diploma);
                    }
                }
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            AddSelectedDiplomas();

            db.SaveChanges();
            this.DialogResult = true;
            Close();
        }

        private void addComboBox_Click(object sender, RoutedEventArgs e)
        {
            AddComboBox();
        }
    }
}
