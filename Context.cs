using Sports_Coaches.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Coaches
{
    class Context : DbContext
    {
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Language> Languages { get; set; }
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
        public DbSet<WorkPlace> WorkPlaces { get; set; }

    }
}
