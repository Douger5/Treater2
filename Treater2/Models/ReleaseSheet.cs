using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Treater2.Models
{
    public class ReleaseSheet
    {
        public int ID { get; set; }
        public string ItemID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ReleaseDateTime { get; set; }
        public string Job { get; set; }
        public string Board { get; set; }
        public int? Width { get; set; }
        public int? Length { get; set; }
        public int? Lbs { get; set; }
        public int? Sheets { get; set; }
        public string Roll { get; set; }
        public int? Rejected { get; set; }
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        public virtual Item Item { get; set; }
    }
}