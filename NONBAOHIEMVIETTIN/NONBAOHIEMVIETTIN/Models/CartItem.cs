using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NONBAOHIEMVIETTIN.Models
{
    public class CartItem
    {
        public products Product { get; set; }
        public int Quantity { get; set; }
    }
}