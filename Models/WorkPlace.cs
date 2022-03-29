using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Coaches.Models
{
    public class WorkPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public City City { get; set; }

        public List<Coach> Coaches { get; set; }
    }
}
