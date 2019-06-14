using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Web
{
    public class CustomDateRangeAttribute: RangeAttribute
    {
        public CustomDateRangeAttribute(int minRange =0,int maxRange = 0) : base(typeof(DateTime), DateTime.Now.AddYears(minRange).ToString("MM/dd/yyyy"), DateTime.Now.AddYears(maxRange).ToString("MM/dd/yyyy"))
        { }
    }
}