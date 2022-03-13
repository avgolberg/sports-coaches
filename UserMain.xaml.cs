using Sports_Coaches.Models;
using System;
using System.Collections.Generic;
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
            AdjustRowDefinitions((int)Math.Ceiling((decimal)db.Sports.Count()/5));
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
            //db.Coaches.Add(coach);
            //db.SaveChanges();
        }

        public void AdjustRowDefinitions(int rowNumber)
        {
            sportsGrid.RowDefinitions.Clear();
            for (int n = 0; n < rowNumber; n++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(1, GridUnitType.Star);
                sportsGrid.RowDefinitions.Add(rowDef);
            }

            int i = 0, j = 0;
            foreach (Sport sport in db.Sports)
            {
                StackPanel sp = new StackPanel();
                sp.Cursor = Cursors.Hand;
                sp.Margin = new Thickness(15);
                Image img = new Image();
                img.Stretch = Stretch.UniformToFill;
                img.HorizontalAlignment = HorizontalAlignment.Center;
                img.VerticalAlignment = VerticalAlignment.Center;
                img.Source = new BitmapImage(new Uri(sport.ImageUrl, UriKind.Relative));
                //Images/start_folder_icon.ico" sport.ImageUrl
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
       
    }
}
