using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Treater2.Models
{
    public class TreatingLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TreatingLineID { get; set; }
        public string TreaterName { get; set; }
    }
}