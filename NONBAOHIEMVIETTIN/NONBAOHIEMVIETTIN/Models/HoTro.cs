using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NONBAOHIEMVIETTIN
{
    public class HoTro
    {
        private static HoTro instances;

        public static HoTro Instances
        {
            get
            {
                if (instances == null)
                    instances =new HoTro();
                return instances;
            }
           
        }

        public string EncodeMD5(string pass)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pass));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public string convertVND(string money)
        {
            var format = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            string value = String.Format(format, "{0:c0}", Convert.ToUInt32(money));
            return value;
        }
    }
}