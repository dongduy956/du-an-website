
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

    public partial class history_recharge
    {

        public int id { get; set; }

        public Nullable<int> idaccount { get; set; }

        public Nullable<int> amount_money { get; set; }

        public Nullable<System.DateTime> create_date { get; set; }


        [JsonIgnore]
        public virtual accounts accounts { get; set; }

    }

}
