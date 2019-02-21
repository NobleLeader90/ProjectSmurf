using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationSmurf.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int EventNum { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public bool IsQuiz { get; set; }
        public bool IsExam { get; set; }
        public double GradePointsAvailable { get; set; }
        public bool ReqRunCheck { get; set; }
    }
}
