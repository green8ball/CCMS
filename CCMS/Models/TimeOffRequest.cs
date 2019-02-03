using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class TimeOffRequest
    {
        public long Id { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public DateTime Submission { get; set; }
        public string Status { get; set; }

        public long RequesterId { get; set; }
        public Employee Requester { get; set; }
        
    }
}
