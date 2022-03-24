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
        Context db;
        public UserMain()
        {
            InitializeComponent();
            db = new Context();
            AdjustRowDefinitions((int)Math.Ceiling((decimal)db.Sports.Count()/5), db.Sports.OrderBy(s => s.Name));
            AddCities();
            AddLanguages();
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
            }
            citiesCB.SelectedItem = citiesCB.Items[0];
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
            int coma = 2;
            if ((sender as RangeSlider).LowerValue.ToString().Length < 2) coma = 1;
            switch ((sender as RangeSlider).Name.Substring(0, index))
            {
                case "age":
                    if (ageLowerTB != null)
                    {
                        ageLowerTB.Text = (sender as RangeSlider).LowerValue.ToString().Substring(0, coma);
                    }
                    break;
                case "experience":
                    if (experienceLowerTB != null)
                    {

                        experienceLowerTB.Text = (sender as RangeSlider).LowerValue.ToString().Substring(0, coma);
                    }
                    break;
                case "price":
                    if (priceLowerTB != null)
                    {
                        priceLowerTB.Text = (sender as RangeSlider).LowerValue.ToString().Substring(0, coma);
                    }
                    break;
            }
        }

        private void slider_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            int index = (sender as RangeSlider).Name.IndexOf('S');
            int coma = 2;
           //if ((sender as RangeSlider).LowerValue.ToString().Length < 2) coma = 1;
            switch ((sender as RangeSlider).Name.Substring(0, index))
            {
                case "age":
                    if (ageHigherTB != null)
                    {
                        ageHigherTB.Text = (sender as RangeSlider).HigherValue.ToString().Substring(0, 2);
                    }
                    break;
                case "experience":
                    if (experienceHigherTB != null)
                    {
                        experienceHigherTB.Text = (sender as RangeSlider).HigherValue.ToString().Substring(0, 2);
                    }
                    break;
                case "price":
                    if (priceHigherTB != null)
                    {
                        priceHigherTB.Text = (sender as RangeSlider).HigherValue.ToString().Substring(0, 2);
                    }
                    break;
            }
        }
    }
}
