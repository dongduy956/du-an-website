//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NONBAOHIEMVIETTIN.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class orderdetail
    {
        public int idproduct { get; set; }
        public int idorder { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> subtotal { get; set; }
    
        public virtual order order { get; set; }
        public virtual products products { get; set; }
    }
}