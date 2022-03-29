using Sports_Coaches.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddCertificates.xaml
    /// </summary>
    public partial class AddCertificates : Window
    {
        private Context db;
        private List<Certificate> certificates;
        public List<Certificate> selectedCertificates;
        public AddCertificates(List<Certificate> selectedCertificates)
        {
            InitializeComponent();
            db = new Context();
            certificates = db.Certificates.ToList();
            this.selectedCertificates = selectedCertificates;
            if (selectedCertificates.Count > 0) SetSelected();
            else AddComboBox();
        }
        private void AddComboBox(string text = "")
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Margin = new Thickness(10);
            comboBox.FontSize = 14;
            comboBox.Text = text;
            comboBox.IsEditable = true;
            comboBox.ItemsSource = certificates;
            comboBox.DisplayMemberPath = "Name";
            MaterialDesignThemes.Wpf.HintAssist.SetHint(comboBox, "Назва сертифікату");

            certificatesSP.Children.Add(comboBox);
        }


        private void SetSelected()
        {
            foreach (Certificate certificate in selectedCertificates)
            {
                AddComboBox(certificate.Name);
            }
        }

        private void AddSelectedCertificates()
        {
            selectedCertificates.Clear();
            foreach (ComboBox cb in certificatesSP.Children)
            {
                if (selectedCertificates.Where(c => c.Name.Equals(cb.Text)).Any()) continue;
                else if (certificates.Where(c => c.Name.Equals(cb.Text)).Any())
                {
                    selectedCertificates.Add(certificates.Where(c => c.Name.Equals(cb.Text)).FirstOrDefault());
                }
                else
                {
                    if (cb.Text.Length != 0)
                    {
                        Certificate certificate = new Certificate() { Name = cb.Text };
                        selectedCertificates.Add(certificate);
                        db.Certificates.Add(certificate);
                    }
                }
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            AddSelectedCertificates();

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
