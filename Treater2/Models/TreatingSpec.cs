using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Treater2.Models
{
    public class TreatingSpec
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TreatingSpecID { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double FlowTarget { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double FlowMax { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double FlowMin { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double RCTarget { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double RCMax { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double RCMin { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double VolTarget { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double VolMax { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double VolMin { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double NetRCTarget { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double NetRCMin { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double NetRCMax { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double GurleyPorosityTarget { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double GurleyPorosityMin { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double GurleyPorosityMax { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double WetOutTarget { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double WetOutMin { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double WetOutMax { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double CaliperTarget { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double CaliperMin { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double CaliperMax { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double BasisWeightTarget { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double BasisWeightMin { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double BasisWeightMax { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}