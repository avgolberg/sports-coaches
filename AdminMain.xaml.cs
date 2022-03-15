﻿using Microsoft.Win32;
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
            //db.Coaches.Add(coach);
            //db.SaveChanges();

            foreach (Coach coach in db.Coaches.Include("Sport").OrderBy(c => c.FullName))
            {
                AddCoaches(coach);
            }
        }
        public void AddCoaches(Coach coach)
        {
            /*using (var context = new SchoolContext())
{
    var student = context.Students
                        .Where(s => s.FirstName == "Bill")
                        .FirstOrDefault<Student>();

    context.Entry(student).Reference(s => s.StudentAddress).Load(); // loads StudentAddress
    context.Entry(student).Collection(s => s.StudentCourses).Load(); // loads Courses collection 
}     
            
             using (var ctx = new SchoolDBEntities())
{
    var stud1 = ctx.Students.Include(s => s.Standard.Teachers)
                    .Where(s => s.StudentName == "Bill")
                    .FirstOrDefault<Student>();
}
            
             */
            
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
