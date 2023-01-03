using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace Treater2.Models
{
    public class BarConfig
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string BarConfigID { get; set; }
    }
}