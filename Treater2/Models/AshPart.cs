using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Treater2.Models
{
    public class AshPart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AshPartID { get; set; }
        public string ResinMix { get; set; }
        public string TreatingSpecID { get; set; }
        public string Paper { get; set; }
        public string Size { get; set; }
        public virtual TreatingSpec TreatingSpec { get; set; }
    }
}