using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NONBAOHIEMVIETTIN.Models
{
    public class AccountInfo
    {
        public accounts acc { get; set; }
        public List<order> lstOrder { get; set; }
    }
}