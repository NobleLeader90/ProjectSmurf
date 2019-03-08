using OperationSmurf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationSmurf.ViewModels
{
    public class Roster
    {
        public Section Section { get; set; }
        public virtual List<Student> Students { get; set; }
        public int targetStudent { get; set; }
    }
}
