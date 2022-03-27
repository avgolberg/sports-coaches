using Microsoft.Win32;
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
    /// Логика взаимодействия для AddCoach.xaml
    /// </summary>
    public partial class AddCoach : Window
    {
        private Context db;
        private OpenFileDialog openFileDialog;
        private Coach coach;
        public AddCoach()
        {
            InitializeComponent();
            db = new Context();
            coach = new Coach();
            coach.Id = db.Coaches.Max(c => c.Id) + 1;

            AddValues();
        }

        private void AddValues()
        {
            foreach (Language language in db.Languages.OrderBy(l => l.Name))
            {
                languagesLB.Items.Add(language.Name);
            }

            foreach (City city in db.Cities.OrderBy(c => c.Name))
            {
                cityCB.Items.Add(city.Name);
            }

            foreach (Sport sport in db.Sports.OrderBy(s => s.Name))
            {
                sportCB.Items.Add(sport.Name);
            }

            foreach (Rank rank in db.Ranks.OrderBy(r => r.Name))
            {
                rankCB.Items.Add(rank.Name);
            }

        }

        private void coachImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    coachImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    string filename = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //System.IO.File.Copy(openFileDialog.FileName, "..//..//Images//coach_" + coach.Id + "_" + filename.Substring(filename.LastIndexOf('.')));

            }
        }
    }
}
