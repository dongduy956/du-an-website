using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NONBAOHIEMVIETTIN.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Tài khoản không được rỗng!!")]        
        [Display(Name ="Tài khoản")]
        public string username { get; set; }
        [Required(ErrorMessage ="Mật khẩu không được rỗng!!")]
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }
        public string image { get; set; }
        public string fullname { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string address { get; set; }
    }
}