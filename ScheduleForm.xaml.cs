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
    /// Логика взаимодействия для Schedule.xaml
    /// </summary>
    public partial class ScheduleForm : Window
    {
        private int rowNumber = 18;
        private int columnNumber = 8;
        public List<Schedule> sheduleResult;
        public ScheduleForm(List<Schedule> sheduleResult)
        {
            InitializeComponent();
            SetGridDefinition();
            this.sheduleResult = sheduleResult;
            SetSelected();
        }

        public void SetGridDefinition()
        {
            mainGrid.RowDefinitions.Clear();
            mainGrid.ColumnDefinitions.Clear();

            mainGrid.Children.Clear();
            for (int n = 0; n < rowNumber; n++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(1, GridUnitType.Star);
                mainGrid.RowDefinitions.Add(rowDef);
            }

            for (int n = 0; n < columnNumber; n++)
            {
                ColumnDefinition columnDef = new ColumnDefinition();
                columnDef.Width = new GridLength(1, GridUnitType.Star);
                mainGrid.ColumnDefinitions.Add(columnDef);
            }

            List<string> days = new List<string>() { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Нд" };
            for (int i = 0; i < columnNumber-1; i++)
            {
                TextBlock tb = new TextBlock();
                tb.Text = days[i];
                tb.TextAlignment = TextAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;

                mainGrid.Children.Add(tb);
                Grid.SetColumn(tb, i+1);
                Grid.SetRow(tb, 0);
            }

            List<string> time = new List<string>() { "6:00", "7:00", "8:00", "9:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00" };
            for (int i = 0; i < rowNumber - 1; i++)
            {
                TextBlock tb = new TextBlock();
                tb.Text = time[i];
                tb.TextAlignment = TextAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;

                mainGrid.Children.Add(tb);
                Grid.SetColumn(tb, 0);
                Grid.SetRow(tb, i+1);
            }

            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < columnNumber; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Stroke = new SolidColorBrush() { Color = Colors.LightGray};
                    rect.Fill = new SolidColorBrush() { Color = Colors.Transparent };

                    mainGrid.Children.Add(rect);
                    Grid.SetColumn(rect, j);
                    Grid.SetRow(rect, i);
                }
            }

        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                var point = Mouse.GetPosition(mainGrid);

                int row = 0;
                int col = 0;
                double accumulatedHeight = 0.0;
                double accumulatedWidth = 0.0;

                // calc row mouse was over
                foreach (var rowDefinition in mainGrid.RowDefinitions)
                {
                    accumulatedHeight += rowDefinition.ActualHeight;
                    if (accumulatedHeight >= point.Y)
                        break;
                    row++;
                }

                // calc col mouse was over
                foreach (var columnDefinition in mainGrid.ColumnDefinitions)
                {
                    accumulatedWidth += columnDefinition.ActualWidth;
                    if (accumulatedWidth >= point.X)
                        break;
                    col++;
                }

            Rectangle rect = (Rectangle)GetElementInGridPosition(col, row);
            TextBlock startTime = (TextBlock)GetElementInGridPosition(0, row);
            int startHour = int.Parse(startTime.Text.Substring(0, startTime.Text.IndexOf(':')));
            int endHour = startHour + 1;

            if (((SolidColorBrush)rect.Fill).Color == Colors.Orange)
            {
                rect.Fill = new SolidColorBrush() { Color = Colors.Transparent };
                Schedule scheduleToDel = sheduleResult.Where(s => (s.Day == (Day)(col - 1)) && (s.StartHour == startHour) && (s.EndHour == endHour)).FirstOrDefault();
                sheduleResult.Remove(scheduleToDel);
            }
            else
            {
                rect.Fill = new SolidColorBrush() { Color = Colors.Orange };
                Schedule schedule = new Schedule() { Day = (Day)(col - 1), StartHour = startHour, EndHour = endHour };
                sheduleResult.Add(schedule);
            }
        }

        private void SetSelected()
        {
            foreach (Schedule schedule in sheduleResult)
            {
                Rectangle rect = (Rectangle)GetElementInGridPosition((int)schedule.Day+1, schedule.StartHour-5);
                rect.Fill = new SolidColorBrush() { Color = Colors.Orange };
            }
        }

        private UIElement GetElementInGridPosition(int column, int row)
        {
            foreach (UIElement element in this.mainGrid.Children)
            {
                if (Grid.GetColumn(element) == column && Grid.GetRow(element) == row)
                    return element;
            }

            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }
    }
