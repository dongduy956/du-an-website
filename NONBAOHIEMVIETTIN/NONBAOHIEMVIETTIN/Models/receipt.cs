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
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public partial class receipt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public receipt()
        {
            this.receiptdetail = new HashSet<receiptdetail>();
        }
    
        public int id { get; set; }
        public Nullable<int> idaccount { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public Nullable<decimal> total { get; set; }
        [JsonIgnore]

        public virtual accounts accounts { get; set; }
        [JsonIgnore]

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receiptdetail> receiptdetail { get; set; }
    }
}