using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Coaches.Models
{
    public enum Day
    {
        Пн, Вт, Ср, Чт, Пт, Сб, Нд
    }
    public class Schedule
    {
        public int Id { get; set; }
        public Day Day { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public List<Coach> Coaches { get; set; }
    }
}
