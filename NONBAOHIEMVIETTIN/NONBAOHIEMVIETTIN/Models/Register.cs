using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NONBAOHIEMVIETTIN.Models
{
    public class Register
    {
        [Required(ErrorMessage ="Tài khoản không được rỗng")]
        [Display(Name ="Tài khoản")]
        public string username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được rỗng")]
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }       
        public string image { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được rỗng")]
        [Display(Name = "Họ tên")]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Email không được rỗng")]        
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được rỗng")]
        [Display(Name = "Số điện thoại")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được rỗng")]
        [Display(Name = "Địa chỉ")]
        public string address { get; set; }
    }
}