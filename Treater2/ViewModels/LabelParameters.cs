using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Treater2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Treater2.ViewModels
{
    public class LabelParameters
    {
        public int? ID { get; set; }
        public int? Quantity { get; set; }
        public UOM? UOM { get; set; }
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

    }
}