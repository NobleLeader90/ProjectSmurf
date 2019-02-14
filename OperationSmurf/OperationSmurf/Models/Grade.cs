﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationSmurf.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int EventId { get; set; }
        public int State { get; set; }
        public int SectionId { get; set; }
        public double GradeEarned { get; set; }
    }
}
