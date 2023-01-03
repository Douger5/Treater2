using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Treater2.Models;

namespace Treater2.ViewModels
{
    public class HomeIndex
    {
        public int TreatingLineID { get; set; }
        public virtual TreatingLine TreatingLine { get; set; }
    }
}