using Sports_Coaches.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddPhones.xaml
    /// </summary>
    public partial class AddPhones : Window
    {
        private Context db;
        private List<Phone> phones;
        public List<Phone> addedPhones;
        public AddPhones(List<Phone> addedPhones)
        {
            InitializeComponent();
            db = new Context();
            this.addedPhones = addedPhones;
            phones = db.Phones.ToList();

            if (addedPhones.Count > 0) SetSelected();
            else AddTextBox();
        }

        private void AddTextBox(string text = "")
        {
            MaskedTextBox tb = new MaskedTextBox();
            tb.Margin = new Thickness(10);
            tb.FontSize = 16;
            tb.Text = text;
            tb.Mask = "+38 (000) 000-0000";
            phonesSP.Children.Add(tb);
        }

        private void SetSelected()
        {
            foreach (Phone phone in addedPhones)
            {
                AddTextBox(phone.Number);
            }
        }
        private void AddSelectedPhones()
        {
            addedPhones.Clear();
            foreach (MaskedTextBox tb in phonesSP.Children)
            {
                if (addedPhones.Where(p => p.Number.Equals(tb.Text)).Any()) continue;
                else if (phones.Where(p => p.Number.Equals(tb.Text)).Any())
                {
                    addedPhones.Add(phones.Where(p => p.Number.Equals(tb.Text)).FirstOrDefault());
                }
                else
                {
                    if (tb.Text.Length != 0)
                    {
                        Phone phone = new Phone() { Number = tb.Text };
                        addedPhones.Add(phone);
                        db.Phones.Add(phone);
                    }
                }
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            AddSelectedPhones();

            db.SaveChanges();
            this.DialogResult = true;
            Close();
        }

        private void addTextBox_Click(object sender, RoutedEventArgs e)
        {
            AddTextBox();
        }

    }
}
