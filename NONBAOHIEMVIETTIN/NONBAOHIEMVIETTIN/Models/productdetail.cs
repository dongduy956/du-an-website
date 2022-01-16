using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NONBAOHIEMVIETTIN.Models
{
    public class productdetail
    {
        public products product { get; set; }
        public List<products> lstProductCategory { get; set; }
        public List<products> lstProductProduction { get; set; }
        public List<products> lstProductGroup { get; set; }
        public List<rate> lstRate { get; set; }
    }
}