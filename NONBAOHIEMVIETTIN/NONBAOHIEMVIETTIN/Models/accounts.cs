namespace NONBAOHIEMVIETTIN.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class accounts
    {
        public int id { get; set; }

        public int? idpermission { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        [StringLength(500)]
        public string image { get; set; }

        [StringLength(100)]
        public string fullname { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(11)]
        public string phone { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        public virtual permission permission { get; set; }
    }
}
