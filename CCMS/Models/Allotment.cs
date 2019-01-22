using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class Allotment
    {
        public long Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public int Allowed { get; set; }
        public int Taken { get; set; }
    }
}
