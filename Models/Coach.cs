using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Coaches.Models
{
    class Coach
    {
        public int Id { get; set; }

        [Required, Column("Full name")]
        public string FullName { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        public string PhotoUrl { get; set; }


        //public Person Person { get; set; }
        //public string Holiday { get; set; }

        

        //public Category Category { get; set; } 

        //public Status Status { get; set; }
        //public string WhereToBuy { get; set; }

        //public double ApproximatePrice { get; set; }
        //public bool GiftWrapping { get; set; }

        //public string Comment { get; set; }
    }
}
