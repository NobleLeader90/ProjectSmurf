using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smurf.Models
{
    public class Event
    {
        public int ID { get; set; }
        public int EventNum { get; set; }
        public string EventName { get; set; }
        public bool IsQuiz { get; set; }
        public bool IsExam { get; set; }
        public double Grade { get; set; }
        public bool ReqRuncheck { get; set; }
    }
}
