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
    
    public partial class accounts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public accounts()
        {
            this.order = new HashSet<order>();
        }
    
        public int id { get; set; }
        public Nullable<int> idrole { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string image { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<int> issocial { get; set; }
    
        public virtual role role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> order { get; set; }
    }
}
