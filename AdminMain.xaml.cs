using Microsoft.Win32;
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
using Sports_Coaches.Models;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Context db;

        public MainWindow()
        {
            InitializeComponent();

            db = new Context();
            //Coach coach = new Coach();
            //coach.FullName = "Віктор Коваленко";
            //coach.City = (City)db.Cities.Where(c => c.Name.Equals("Одеса"));
            //coach.Sport = (Sport)db.Sports.Where(s => s.Name.Equals("Шахи"));
            //coach.Languages = new List<Language>();
            //coach.Languages.Add((Language)db.Languages.Where(l => l.Name.Equals("Англійська")));
            //coach.Languages.Add((Language)db.Languages.Where(l => l.Name.Equals("Українська")));
            //coach.Email = "kovalenkov@gmail.com";
            //coach.AwayTrainingPrice = 400;
            //coach.Experience = 19;
            //coach.DateOfBirth = DateTime.Parse("22.01.1969");
            //coach.Phone = new List<Phone>();
            //coach.Phone.Add(new Phone { Number = "+38 (050) 368-38-15" });
            //coach.Phone.Add(new Phone { Number = "+38 (067) 248-38-15" });

           // db.Coaches.Load; // загружаем данные
           // coachesGrid.ItemsSource = db.Coaches.Local;
        }

        private void coachesGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
               // db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Возникла ошибка");
            }
        }

        private void coachesGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            //object item = peopleGrid.Items[e.Row.GetIndex()];
            //peopleGrid.SelectedItem = item;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               // db.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены", ":)");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Возникла ошибка");
            }
        }

        private void giftImage_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                //Gift gift = giftsGrid.SelectedItem as Gift;
                //Image image = sender as Image;
                //openFileDialog = new OpenFileDialog();
                //openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
                //if (openFileDialog.ShowDialog() == true)
                //{
                //    //Реализовать копирование картинки в папу, для того, чтобы можно было не зависеть от ее местонахождения
                //    //string destFile = System.IO.Path.Combine(targetPath, openFileDialog.FileName);
                //    //File.Copy(openFileDialog.FileName, destFile, true);
                //    try
                //    {
                //        image.Source = new BitmapImage(new Uri(OpenFileDialog.FileName));
                //        gift.ImageUrl = openFileDialog.FileName;
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }

                //    db.SaveChanges();
                }

            }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            //if (giftsGrid.SelectedItems.Count > 0)
            //{
            //    for (int i = 0; i < giftsGrid.SelectedItems.Count; i++)
            //    {
            //        Gift gift = giftsGrid.SelectedItems[i] as Gift;
            //        if (gift != null)
            //        {
            //            db.gifts.Remove(gift);
            //        }
            //    }
            //}
            //db.SaveChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
