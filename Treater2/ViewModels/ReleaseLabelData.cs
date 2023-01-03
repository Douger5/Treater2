using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Treater2.Models;

namespace Treater2.ViewModels
{
    public class ReleaseLabelData
    {
        public ReleaseSheetParameters releaseSheetParameters { get; set; }
        public Item selectedItem { get; set; }
    }
}