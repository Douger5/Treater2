using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Treater2.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public string ItemID { get; set; }
        public string Description { get; set; }
        public string ItemIDDescription
        {
            get
            {
                return ItemID + ": " + Description;
            }
        }
    }
}