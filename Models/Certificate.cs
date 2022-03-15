using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Coaches.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Coach> Coaches { get; set; }
    }
}
