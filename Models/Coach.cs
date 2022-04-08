using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Coaches.Models
{
    public enum Gender
    {
        Мужчина,
        Женщина
    }
    public class Coach
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        public List<Phone> Phone { get; set; }

        public string Email { get; set; }

        public string PhotoUrl { get; set; }

        public Gender Gender { get; set; }

        public City City { get; set; }

        [Required]
        public Sport Sport { get; set; }

        public List<Language> Languages { get; set; }

        public List<Diploma> Diplomas { get; set; }

        public List<Course> Courses { get; set; }

        public List<Certificate> Certificates { get; set; }

        public List<WorkPlace> WorkPlaces { get; set; }

        [Required]
        public int Experience { get; set; }

        public Rank Rank { get; set; }

        public List<Training> Training { get; set; }
        public List<AwayTraining> AwayTraining { get; set; }

        public List<Schedule> Schedule { get; set; }

        public List<Achievement> Achievements { get; set; }

        public List<Review> Reviews { get; set; }

    }
}
