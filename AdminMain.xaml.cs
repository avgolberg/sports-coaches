using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
            AddCoaches(db.Coaches.Include("Sport").OrderBy(c => c.FullName));
        }
        public void AddCoaches(IQueryable<Coach> coaches)
        {
            coachesGrid.Children.Clear();
            coachesGrid.RowDefinitions.Clear();
            for (int n = 0; n < coaches.Count(); n++)
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
                sp.HorizontalAlignment = HorizontalAlignment.Left;
                sp.Cursor = Cursors.Hand;
                sp.Margin = new Thickness(15);

                Image img = new Image();
                img.Width = 100;
                img.Height = 100;
                img.Stretch = Stretch.Uniform;
                if (coach.PhotoUrl != null && !coach.PhotoUrl.StartsWith("pack"))
                    img.Source = new BitmapImage(new Uri(coach.PhotoUrl, UriKind.Relative));
                else if(coach.PhotoUrl != null && coach.PhotoUrl.StartsWith("pack")) img.Source = new BitmapImage(new Uri(coach.PhotoUrl));
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

                coachesGrid.Children.Add(sp);
                Grid.SetRow(sp, i);
                i++;
            }            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            AddCoaches(db.Coaches.Include("Sport").Where(c => c.FullName.Contains(searchTB.Text)).OrderBy(c => c.FullName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddCoach addCoachForm = new AddCoach();
            addCoachForm.ShowDialog();
            if (addCoachForm.DialogResult == true)
            {
                AddCoaches(db.Coaches.Include("Sport").OrderBy(c => c.FullName));
            }
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = Mouse.GetPosition(coachesGrid);

            int row = 0;
            int col = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            // calc row mouse was over
            foreach (var rowDefinition in coachesGrid.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            // calc col mouse was over
            foreach (var columnDefinition in coachesGrid.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }

            StackPanel sp = (StackPanel)GetElementInGridPosition(col, row);
            StackPanel innerSP = (StackPanel)sp.Children[1];
            TextBlock nameTB = (TextBlock)innerSP.Children[0];
            AddCoach addCoachForm = new AddCoach(db.Coaches.Include("Sport").Include("City").Include("WorkPlaces").Include("Training").Include("Phone").Include("Languages").Include("City").Include("Diplomas").Include("Courses").Include("Certificates").Include("Certificates").Include("Rank").Include("Training").Include("AwayTraining").Include("Schedule").Include("Achievements").Where(c => c.FullName.Equals(nameTB.Text)).First());
            addCoachForm.ShowDialog();
            if(addCoachForm.DialogResult==true)
                AddCoaches(db.Coaches.Include("Sport").OrderBy(c => c.FullName));
        }

        private UIElement GetElementInGridPosition(int column, int row)
        {
            foreach (UIElement element in this.coachesGrid.Children)
            {
                if (Grid.GetColumn(element) == column && Grid.GetRow(element) == row)
                    return element;
            }

            return null;
        }

    }
}
