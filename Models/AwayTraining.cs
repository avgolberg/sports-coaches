using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Coaches.Models
{
    [Table("AwayTraining")]
    public class AwayTraining
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public int Price { get; set; }
    }
}
