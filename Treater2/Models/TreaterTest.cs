using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Treater2.Models
{
    public enum PanConfig
    {
        [Display(Name = "U")]
        U,
        [Display(Name = "W")]
        W,
        [Display(Name = "1-Pan Roller")]
        Pan1,
        [Display(Name = "Config A")]
        ConfigA,
        [Display(Name = "Config B")]
        ConfigB
    }

    public enum UOM
    {
        sheets, ft, lbs, LI
    }
    public class TreaterTest
    {
        public int ID { get; set; }
        public int TreatingLineID { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime TreatmentDateTime { get; set; }
        public string AshPartID { get; set; }
        public int OperatorA { get; set; }
        public int? OperatorB { get; set; }
        [Required]
        public string RollSkidNum { get; set; }
        public string JobNum { get; set; }
        public int? Qty { get; set; }
        public UOM? UOM { get; set; }
        public string DryRollNum { get; set; }
        public int? TestNum { get; set; }
        [Range(150,360)]
        public int? Zone1Temp { get; set; }
        [Range(150, 360)]
        public int? Zone2Temp { get; set; }
        [Range(150, 360)]
        public int? Zone3Temp { get; set; }
        [Range(10, 110)]
        public int? Speed { get; set; }
        public PanConfig? PanConfig { get; set; }
        [Range(9, 20)]
        public int? PanLevel { get; set; } 
        public string BarConfigID { get; set; }
        [Range(75, 100)]
        public int? BarAngle { get; set; }
        [Range(50, 145)]
        public int? ResinTemp { get; set; }
        [Range(0,1260)]
        public double? ResinSolids { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double ResinSolidsPct
        {
            get
            {
                double res = 0;
                if (ResinSolids.HasValue)
                {
                    double RS = ResinSolids.Value;
                    res = (RS - 1.3251f) / 0.34f;
                }
                return res;
            }
        }
        [Range(0, 5000)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public int RawPaperWeight { get; set; }
        [Range(0, 5000)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public int DryPaperWeight { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double RawPaperMC
        {
            get
            {
                double rpm = 0;
                if (RawPaperWeight != 0)
                {
                    rpm = 1.0f * (RawPaperWeight - DryPaperWeight) / RawPaperWeight;
                }
                else
                {
                    rpm = 0;
                }
                return rpm;
            }
        }
        [Range(0, 5000)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public int TreatedPaperWeight { get; set; }
        [Range(0, 5000)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public int VolatilesTreatedWeight { get; set; }
        [Range(0, 5000)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public int VolatilesOvenDryWeight { get; set; }
        [Range(0, 5000)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public int TreatedPressedWeight { get; set; }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        [DisplayName("RC")]
        public float ResinContentFiberesin { get {
                float rcf = 0;
                if (TreatedPaperWeight == 0) return rcf;
                switch (TreatingLineID)
                {
                    case 1:
                        rcf = 1.0f * ((VolatilesTreatedWeight * 282 / 125) - DryPaperWeight) / (VolatilesTreatedWeight * 282 / 125);
                        break;
                    case 4:
                        rcf = 1.0f * ((TreatedPaperWeight * 9 / 4) - DryPaperWeight) / (TreatedPaperWeight * 9 / 4);
                        break;
                    default:
                        rcf = 1.0f * (TreatedPaperWeight - DryPaperWeight) / TreatedPaperWeight;
                        break;
                }
                return rcf;
            }
        }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public float Vol
        {
            get
            {
                float rcf = 0;
                if (VolatilesTreatedWeight != 0)
                {
                    rcf = 1.0f*(VolatilesTreatedWeight - VolatilesOvenDryWeight) / VolatilesTreatedWeight;
                }
                else
                {
                    rcf = 0;
                }
                return rcf;
            }
        }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double Flow
        {
            get
            {
                double rcf = 0;
                if (TreatedPaperWeight != 0)
                {
                    rcf = 1.0f * (TreatedPaperWeight - TreatedPressedWeight) / TreatedPaperWeight;
                }
                else
                {
                    rcf = 0;
                }
                return rcf;
            }
        }
        [DisplayFormat(DataFormatString = "{0:p1}")]
        public double NetRC
        {
            get
            {
                double rcf = ResinContentFiberesin - Vol;
                return rcf;
            }
        }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double BasisWeight
        {
            get
            {
                double rcf = 0.06614f * (TreatedPaperWeight * 9 / 4);
                return rcf;
            }
        }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double GurleyPorosity { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double WetOut { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Caliper { get; set; }
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        public virtual TreatingLine TreatingLine { get; set; }
        public virtual AshPart AshPart { get; set; }
        public virtual BarConfig BarConfig { get; set; }
        public string Paper_COA { get; set; }
        [Required]
        public string Resin_COA { get; set; }
    }
}