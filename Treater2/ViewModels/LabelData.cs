using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Treater2.Models;

namespace Treater2.ViewModels
{
    public class LabelData
    {
        public LabelParameters labelParameters { get; set; }
        public TreaterTest SelectedTest { get; set; }
        public IEnumerable<TreaterTest> TreaterTests { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public float AvgRCF { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double AvgNRC { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public float AvgVol { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double AvgFlow { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double AvgGurley { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double AvgWetOut { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double AvgCaliper { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double AvgBW { get; set; }
        public int NonConforming { get; set; }
    }
}