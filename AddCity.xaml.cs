using Sports_Coaches.Models;
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

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddCity.xaml
    /// </summary>
    public partial class AddCity : Window
    {
        private Context db;
        public AddCity()
        {
            InitializeComponent();
            db = new Context();
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            if (cityTB.Text.Length != 0)
            {
                City city = new City() { Name = cityTB.Text };
                db.Cities.Add(city);
                db.SaveChanges();
                this.DialogResult = true;
                Close();
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Заповніть обов'язкові поля", "Виникла помилка");
            }
        }
    }
}
