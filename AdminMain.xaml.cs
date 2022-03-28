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
            //coach.Gender = Gender.Man;
            //coach.City = db.Cities.Where(c => c.Name.Equals("Одеса")).FirstOrDefault();
            //coach.Sport = db.Sports.Where(s => s.Name.Equals("Шахи")).FirstOrDefault();
            //coach.Languages = new List<Language>();
            //coach.Languages.Add(db.Languages.Where(l => l.Name.Equals("Англійська")).FirstOrDefault());
            //coach.Languages.Add(db.Languages.Where(l => l.Name.Equals("Українська")).FirstOrDefault());
            //coach.Email = "kovalenkov@gmail.com";
            //coach.AwayTrainingPrice = 400;
            //coach.Experience = 19;
            //coach.DateOfBirth = DateTime.Parse("22.01.1969");
            //coach.Phone = new List<Phone>();
            //coach.Phone.Add(new Phone { Number = "+38 (050) 368-38-15" });
            //coach.Phone.Add(new Phone { Number = "+38 (067) 248-38-15" });
            //coach.PhotoUrl = "Images/Aikido.png";
            //coach.Rank = db.Ranks.Where(r => r.Name.Equals("Кандидат у майстри спорту")).FirstOrDefault();

            //Coach coach1 = db.Coaches.Where(c => c.FullName.Equals("Віктор Коваленко")).FirstOrDefault();
            //coach1.Training = new List<Training>();
            //Training training1 = new Training() { Name = "Персональне тренування (дитяче)", Price = 200, Time = "60 хв." };
            //Training training2 = new Training() { Name = "Групове тренування (дитяче)", Price = 800, Time = "1 міс." };
            //coach1.Training.Add(training1);
            //coach1.Training.Add(training2);

            //Coach coach1 = db.Coaches.Where(c => c.FullName.Equals("Марія")).FirstOrDefault();
            //coach1.WorkPlaces = new List<WorkPlace>();
            //WorkPlace workPlace = new WorkPlace() { Name = "Спортивний клуб бойових мистецтв «Fighter»", Address = "бульв. Верховної Ради, 22" };
            //coach1.WorkPlaces.Add(workPlace);


            //Coach coach1 = db.Coaches.Where(c => c.FullName.Equals("Марія")).FirstOrDefault();
            //coach1.Diplomas = new List<Diploma>();
            //Diploma diploma = new Diploma() { Name = "Київський національний університет технологій та дизайну (КНУТД)" };
            //coach1.Diplomas.Add(diploma);


            //db.Coaches.Add(coach);
            //db.SaveChanges();

            foreach (Coach coach in db.Coaches.Include("Sport").OrderBy(c => c.FullName))
            {
                AddCoaches(coach);
            }
        }
        public void AddCoaches(Coach coach)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.HorizontalAlignment = HorizontalAlignment.Left;
            sp.Cursor = Cursors.Hand;
            sp.Margin = new Thickness(15);

            Image img = new Image();
            img.Width = 100;
            img.Height = 100;
            img.Stretch = Stretch.Uniform;
            if (coach.PhotoUrl != null)
                img.Source = new BitmapImage(new Uri(coach.PhotoUrl, UriKind.Relative));
            else img.Source = new BitmapImage(new Uri("Images/logo.jpg", UriKind.Relative));

            StackPanel innerSP = new StackPanel();
            innerSP.VerticalAlignment = VerticalAlignment.Center;
            innerSP.Margin = new Thickness(10, 0, 10, 0);

            TextBlock nameTB = new TextBlock();
            nameTB.Text = coach.FullName;
            nameTB.FontSize = 20;
            nameTB.Margin = new Thickness(0, 0, 0, 10);

            TextBlock sportTB = new TextBlock();
            sportTB.Text = coach.Sport.Name;
            sportTB.FontSize = 16;

            innerSP.Children.Add(nameTB);
            innerSP.Children.Add(sportTB);

            sp.Children.Add(img);
            sp.Children.Add(innerSP);

            coachesSP.Children.Add(sp);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            coachesSP.Children.Clear();
            foreach (Coach coach in db.Coaches.Include("Sport").Where(c => c.FullName.Contains(searchTB.Text)).OrderBy(c => c.FullName))
            {
                AddCoaches(coach);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
    }
}
