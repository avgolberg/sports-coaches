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
    /// Логика взаимодействия для AddSport.xaml
    /// </summary>
    public partial class AddSport : Window
    {
        private OpenFileDialog openFileDialog;
        private Context db;
        public AddSport()
        {
            InitializeComponent();
            db = new Context();
        }

        private void sportImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    sportImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            if (sportTB.Text.Length != 0)
            {
                Sport sport = new Sport();
                sport.Name = sportTB.Text;

                if (openFileDialog != null)
                {
                    string filename = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1);
                    string sportImage = sportTB.Text.ToLower() + filename.Substring(filename.LastIndexOf('.'));
                    System.IO.File.Copy(openFileDialog.FileName, "..//..//Images//" + sportImage);
                    sport.ImageUrl = "Images/" + sportImage;
                }
                else
                {
                    sport.ImageUrl = "Images/no-image.png";
                }
                
                db.Sports.Add(sport);
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
