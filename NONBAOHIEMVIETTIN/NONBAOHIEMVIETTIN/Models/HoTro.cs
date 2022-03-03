using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
        public string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            string unsigned = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            unsigned = Regex.Replace(unsigned, "[^a-zA-Z0-9 ]+", "");
            return unsigned.Replace(" ", "-");
        }


        public int validateYear(int year)
        {
            if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
                return 1;
            return 0;
        }
        public int dayOfMonth(int month, int year)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 2:
                    return 28 + validateYear(year);
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
            }
            return -1;
        }

        public DateTime getYesterday(DateTime now)
        {
            int day, month, year;

            if (now.Day == 1)
            {
                if (now.Month == 1)
                {
                    month = 12;
                    year = now.Year - 1;
                }
                else
                {
                    month = now.Month - 1;
                    year = now.Year;
                }
                day = dayOfMonth(month, year);
            }
            else
            {
                month = now.Month;
                year = now.Year;
                day = now.Day - 1;
            }


            return new DateTime(year, month, day);
        }

     
    }
}