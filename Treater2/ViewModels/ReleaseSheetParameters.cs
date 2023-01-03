using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Treater2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Treater2.ViewModels
{
    public class ReleaseSheetParameters
    {
        public string ItemID { get; set; }
        public string Job { get; set; }
        public string Board { get; set; }
        public int? Width { get; set; }
        public int? Length { get; set; }
        public int? lbs { get; set; }
        public int? Sheets { get; set; }
        public string Roll { get; set; }
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}