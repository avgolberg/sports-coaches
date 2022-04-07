using Sports_Coaches.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Coaches
{
    class Context : DbContext
    {
        private bool ruVersion = false; 
        public Context() : base("Context")
        {
            //ruVersion = true;
            //Fill(); //at first start to fill the database
        }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Diploma> Diplomas { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<AwayTraining> AwayTraining { get; set; }
        public DbSet<WorkPlace> WorkPlaces { get; set; }

        private void Fill()
        {
            //Sports
            string path;
            string name;

            path = "..//..//Data//Sports.txt";
            if (ruVersion) path = "..//..//Data//Sports_ru.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                while ((name = reader.ReadLine()) != null)
                {
                    string sport = name.Split('|')[0];
                    string image = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, name.Split('|')[1]);
                    Sports.Add(new Sport { Name = sport, ImageUrl=image });
                }
            }

            //Languages
            path = "..//..//Data//Languages.txt";
            if (ruVersion) path = "..//..//Data//Languages_ru.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                while ((name = reader.ReadLine()) != null)
                {
                    Languages.Add(new Language { Name = name });
                }
            }

            //Ranks
            path = "..//..//Data//Ranks.txt";
            if (ruVersion) path = "..//..//Data//Ranks_ru.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                while ((name = reader.ReadLine()) != null)
                {
                    Ranks.Add(new Rank { Name = name });
                }
            }

            //Cities
            path = "..//..//Data//Cities.txt";
            if (ruVersion) path = "..//..//Data//Cities_ru.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                while ((name = reader.ReadLine()) != null)
                {
                    Cities.Add(new City { Name = name });
                }
            }

            SaveChanges();
        }
    }
}
