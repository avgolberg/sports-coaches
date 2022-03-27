using MaterialDesignThemes.Wpf;
using Sports_Coaches.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using Xceed.Wpf.Toolkit;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для UserMain.xaml
    /// </summary>
    public partial class UserMain : Window
    {
        private Context db;
        private string sportName;
        public List<Schedule> sheduleResult = new List<Schedule>();
        public UserMain()
        {
            InitializeComponent();
            db = new Context();
            AddSports((int)Math.Ceiling((decimal)db.Sports.Count() / 5), db.Sports.OrderBy(s => s.Name));
            AddCities();
            AddLanguages();
            AddRanks();
            SetSlidersStartValues();
        }

        private void SetSlidersStartValues()
        {
            var today = DateTime.Today;
            var birthdate = db.Coaches.Min(c => c.DateOfBirth);
            var age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;

            ageSlider.Maximum = age;
            ageSlider.HigherValue = age;

            birthdate = db.Coaches.Max(c => c.DateOfBirth);
            age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;

            ageSlider.Minimum = age;
            ageSlider.LowerValue = age;

            var experience = db.Coaches.Min(c => c.Experience);
            experienceSlider.Minimum = experience;
            experienceSlider.LowerValue = experience;

            experience = db.Coaches.Max(c => c.Experience);
            experienceSlider.Maximum = experience;
            experienceSlider.HigherValue = experience;

            var price = db.Training.Min(t => t.Price);
            priceSlider.Minimum = price;
            priceSlider.LowerValue = price;

            price = db.Training.Max(t => t.Price);
            priceSlider.Maximum = price;
            priceSlider.HigherValue = price;
        }

        private void AddCoaches(IQueryable<Coach> coaches)
        {
            coachesGrid.RowDefinitions.Clear();
            coachesGrid.Children.Clear();
            for (int n = 0; n < db.Coaches.Count(); n++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(1, GridUnitType.Star);
                coachesGrid.RowDefinitions.Add(rowDef);
            }

            int i = 0;
            foreach (Coach coach in coaches)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                sp.VerticalAlignment = VerticalAlignment.Center;
                sp.Cursor = Cursors.Hand;
                sp.Margin = new Thickness(30, 20, 10, 0);

                Image img = new Image();
                img.Width = 100;
                img.Height = 100;
                if (coach.PhotoUrl!=null)
                    img.Source = new BitmapImage(new Uri(coach.PhotoUrl, UriKind.Relative));
                else img.Source = new BitmapImage(new Uri("Images/logo.jpg", UriKind.Relative));

                StackPanel infoSP = new StackPanel();
                infoSP.VerticalAlignment = VerticalAlignment.Center;
                infoSP.Width = 500;
                infoSP.Margin = new Thickness(10, 0, 10, 0);

                TextBlock nameTB = new TextBlock();
                nameTB.Text = coach.FullName;
                nameTB.FontSize = 20;

                //age
                var today = DateTime.Today;
                var birthdate = coach.DateOfBirth;
                var age = today.Year - birthdate.Year;
                if (birthdate.Date > today.AddYears(-age)) age--;

                TextBlock ageTB = new TextBlock();
                ageTB.Text = age + " " + DeclensionGenerator.Generate(age, "рік", "роки", "років");
                ageTB.FontSize = 16;

                TextBlock sportTB = new TextBlock();
                sportTB.Text = coach.Sport.Name + " - " + coach.Experience + " " + DeclensionGenerator.Generate(coach.Experience, "рік", "роки", "років");
                sportTB.FontSize = 16;

                TextBlock workplaceTB = new TextBlock();
                if (coach.WorkPlaces.Count != 0)
                    workplaceTB.Text = coach.WorkPlaces[0].Name + " (" + coach.WorkPlaces[0].Address + ")";
                workplaceTB.FontSize = 16;
                workplaceTB.TextWrapping = TextWrapping.Wrap;
                workplaceTB.Margin = new Thickness(0, 10, 0, 10);

                infoSP.Children.Add(nameTB);
                infoSP.Children.Add(ageTB);
                infoSP.Children.Add(sportTB);
                infoSP.Children.Add(workplaceTB);

                StackPanel infoSP2 = new StackPanel();
                infoSP2.VerticalAlignment = VerticalAlignment.Center;
                infoSP2.Margin = new Thickness(10, 0, 10, 0);

                TextBlock priceTB = new TextBlock();
                if(coach.Training.Count != 0)
                    priceTB.Text = "від " + coach.Training.Min(t => t.Price) + " грн.";
                else priceTB.Text = "від 1 грн.";
                priceTB.FontSize = 20;

                TextBlock textTB = new TextBlock();
                textTB.Text = "за тренування";
                textTB.FontSize = 16;

                RatingBar ratingBar = new RatingBar();
                ratingBar.Value = 0;
                ratingBar.Max = 1;
                ratingBar.HorizontalAlignment = HorizontalAlignment.Center;
                ratingBar.Margin = new Thickness(5);
                ratingBar.ValueChanged += new RoutedPropertyChangedEventHandler<int>(BasicRatingBar_ValueChanged);

                infoSP2.Children.Add(priceTB);
                infoSP2.Children.Add(textTB);
                infoSP2.Children.Add(ratingBar);

                sp.Children.Add(img);
                sp.Children.Add(infoSP);
                sp.Children.Add(infoSP2);

                coachesGrid.Children.Add(sp);
                sp.SetValue(Grid.RowProperty, i);

                i++;
            }
        }

        private void AddRanks()
        {
            foreach (Rank rank in db.Ranks)
            {
                ranksLB.Items.Add(rank.Name);
            }
        }

        private void AddLanguages()
        {
            foreach (Language language in db.Languages.OrderBy(l => l.Name))
            {
                languagesLB.Items.Add(language.Name);
            }
        }

        private void AddCities()
        {
            foreach (City city in db.Cities.OrderBy(c => c.Name))
            {
                citiesCB.Items.Add(city.Name);

                if (city.Name.Equals("Київ"))
                    citiesCB.SelectedItem = city.Name;
            }
        }

        private void AddSports(int rowNumber, IQueryable<Sport> sports)
        {
            sportsGrid.RowDefinitions.Clear();
            sportsGrid.Children.Clear();
            for (int n = 0; n < rowNumber; n++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(1, GridUnitType.Star);
                sportsGrid.RowDefinitions.Add(rowDef);
            }

            int i = 0, j = 0;
            foreach (Sport sport in sports)
            {
                StackPanel sp = new StackPanel();
                sp.Cursor = Cursors.Hand;
                sp.Margin = new Thickness(15);

                Image img = new Image();
                img.Stretch = Stretch.UniformToFill;
                img.HorizontalAlignment = HorizontalAlignment.Center;
                img.VerticalAlignment = VerticalAlignment.Center;
                img.Source = new BitmapImage(new Uri(sport.ImageUrl, UriKind.Relative));

                TextBlock tb = new TextBlock();
                tb.Text = sport.Name;
                tb.FontSize = 18;
                tb.Margin = new Thickness(0, 10, 0, 10);
                tb.TextWrapping = TextWrapping.Wrap;
                tb.TextAlignment = TextAlignment.Center;

                sp.Children.Add(img);
                sp.Children.Add(tb);

                sportsGrid.Children.Add(sp);
                Grid.SetColumn(sp, j);
                Grid.SetRow(sp, i);

                j++;
                if (j == 5)
                {
                    j = 0;
                    i++;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddSports((int)Math.Ceiling((decimal)db.Sports.Where(s => s.Name.Contains(searchTB.Text)).Count() / 5), db.Sports.Where(s => s.Name.Contains(searchTB.Text)).OrderBy(s => s.Name));
        }

        private void slider_LowerValueChanged(object sender, RoutedEventArgs e)
        {
            int index = (sender as RangeSlider).Name.IndexOf('S');
            string lowerValue = (sender as RangeSlider).LowerValue.ToString();
            if ((sender as RangeSlider).LowerValue.ToString().Contains(','))
            {
                int comaIndex = (sender as RangeSlider).LowerValue.ToString().IndexOf(',');
                lowerValue = (sender as RangeSlider).LowerValue.ToString().Substring(0, comaIndex);
            }

            switch ((sender as RangeSlider).Name.Substring(0, index))
            {
                case "age":
                    if (ageLowerTB != null)
                    {
                        ageLowerTB.Text = lowerValue;
                    }
                    break;
                case "experience":
                    if (experienceLowerTB != null)
                    {

                        experienceLowerTB.Text = lowerValue;
                    }
                    break;
                case "price":
                    if (priceLowerTB != null)
                    {
                        priceLowerTB.Text = lowerValue;
                    }
                    break;
            }
        }

        private void slider_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            int index = (sender as RangeSlider).Name.IndexOf('S');
            string higherValue = (sender as RangeSlider).HigherValue.ToString();
            if ((sender as RangeSlider).HigherValue.ToString().Contains(','))
            {
                int comaIndex = (sender as RangeSlider).HigherValue.ToString().IndexOf(',');
                higherValue = (sender as RangeSlider).HigherValue.ToString().Substring(0, comaIndex);
            }
            switch ((sender as RangeSlider).Name.Substring(0, index))
            {
                case "age":
                    if (ageHigherTB != null)
                    {
                        ageHigherTB.Text = higherValue;
                    }
                    break;
                case "experience":
                    if (experienceHigherTB != null)
                    {
                        experienceHigherTB.Text = higherValue;
                    }
                    break;
                case "price":
                    if (priceHigherTB != null)
                    {
                        priceHigherTB.Text = higherValue;
                    }
                    break;
            }
        }

        private void scheduleBTN_Click(object sender, RoutedEventArgs e)
        {
            ScheduleForm scheduleForm = new ScheduleForm(sheduleResult);
            scheduleForm.ShowDialog();
            if (scheduleForm.DialogResult != null)
            {
                this.sheduleResult = scheduleForm.sheduleResult;
                scheduleLB.Items.Clear();
                foreach (var schedule in sheduleResult)
                {
                    scheduleLB.Items.Add(schedule.Day.ToString() + ":" + schedule.StartHour + ":00");
                }
                scheduleLB.SelectAll();
            }
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = Mouse.GetPosition(sportsGrid);

            int row = 0;
            int col = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            // calc row mouse was over
            foreach (var rowDefinition in sportsGrid.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            // calc col mouse was over
            foreach (var columnDefinition in sportsGrid.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }

            StackPanel sp = (StackPanel)GetElementInGridPosition(col, row);
            TextBlock sportTB = (TextBlock)sp.Children[1];
            sportName = sportTB.Text;

            AddCoaches(db.Coaches.Include(c => c.Sport).Include(c => c.WorkPlaces).Include(c => c.Training).OrderBy(c => c.FullName).Where(c => c.Sport.Name.Equals(sportName)).Where(c => c.City.Name.Equals(citiesCB.SelectedItem.ToString())));

            findSportGrid.Visibility = Visibility.Collapsed;
            sportsGrid.Visibility = Visibility.Collapsed;
            filterGrid.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Visible;
        }

        private UIElement GetElementInGridPosition(int column, int row)
        {
            foreach (UIElement element in this.sportsGrid.Children)
            {
                if (Grid.GetColumn(element) == column && Grid.GetRow(element) == row)
                    return element;
            }

            return null;
        }

        private void backButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            findSportGrid.Visibility = Visibility.Visible;
            sportsGrid.Visibility = Visibility.Visible;
            filterGrid.Visibility = Visibility.Collapsed;
            backButton.Visibility = Visibility.Collapsed;
        }

        private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //if((sender as RatingBar).Value==0) (sender as RatingBar).Value = 1;
            //else if((sender as RatingBar).Value==1) (sender as RatingBar).Value = 0;

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //IQueryable<Coach> coaches = db.Coaches.Include(c => c.Sport).Include(c => c.WorkPlaces).Include(c => c.Training).OrderBy(c => c.FullName);
                
                //.Where(c => (int)c.Gender == 0 || (int)c.Gender == 1);

            //foreach (ListBoxItem item in (sender as ListBox).SelectedItems)
            //{
            //    if (item.Content.ToString().Equals("Чоловік"))
            //        coaches = coaches.Where(c => (int)c.Gender == 0);
            //    if (item.Content.ToString().Equals("Жінка"))
            //        coaches = coaches.Where(c => (int)c.Gender == 1);
            //}

            //AddCoaches(coaches);

            //int index = (sender as ListBox).Name.IndexOf('L');            
            //switch ((sender as RangeSlider).Name.Substring(0, index))
            //{
            //    case "gender":
            //        AddCoaches();
            //        break;
            //    case "schedule":

            //        break;
            //    case "ranks":

            //        break;
            //    case "languages":

            //        break;
            //}
        }
    }
}
public class DeclensionGenerator
{
    /// <summary>
    /// Возвращает слова в падеже, зависимом от заданного числа
    /// </summary>
    /// <param name="number">Число от которого зависит выбранное слово</param>
    /// <param name="nominativ">Именительный падеж слова. Например "день"</param>
    /// <param name="genetiv">Родительный падеж слова. Например "дня"</param>
    /// <param name="plural">Множественное число слова. Например "дней"</param>
    /// <returns></returns>
    public static string Generate(int number, string nominativ, string genetiv, string plural)
    {
        var titles = new[] { nominativ, genetiv, plural };
        var cases = new[] { 2, 0, 1, 1, 1, 2 };
        return titles[number % 100 > 4 && number % 100 < 20 ? 2 : cases[(number % 10 < 5) ? number % 10 : 5]];
    }
}