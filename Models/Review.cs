﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Coaches.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool Positive { get; set; }
    }
}
