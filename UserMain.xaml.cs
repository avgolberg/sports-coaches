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
            //db = new Context();
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
    }
}
