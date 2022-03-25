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
        public List<Schedule> sheduleResult = new List<Schedule>();
        public UserMain()
        {
            InitializeComponent();
            db = new Context();
            AdjustRowDefinitions((int)Math.Ceiling((decimal)db.Sports.Count() / 5), db.Sports.OrderBy(s => s.Name));
            AddCities();
            AddLanguages();
            AddRanks();
            SetSlidersStartValues();
        }

        public void SetSlidersStartValues()
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

        public void AddRanks()
        {
            foreach (Rank rank in db.Ranks)
            {
                ranksLB.Items.Add(rank.Name);
            }
        }

        public void AddLanguages()
        {
            foreach (Language language in db.Languages.OrderBy(l => l.Name))
            {
                languagesLB.Items.Add(language.Name);
            }
        }

        public void AddCities()
        {
            foreach (City city in db.Cities.OrderBy(c => c.Name))
            {
                citiesCB.Items.Add(city.Name);

                if (city.Name.Equals("Київ"))
                    citiesCB.SelectedItem = city.Name;
            }
        }

        public void AdjustRowDefinitions(int rowNumber, IQueryable<Sport> sports)
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
            AdjustRowDefinitions((int)Math.Ceiling((decimal)db.Sports.Where(s => s.Name.Contains(searchTB.Text)).Count() / 5), db.Sports.Where(s => s.Name.Contains(searchTB.Text)).OrderBy(s => s.Name));
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
    }
}
