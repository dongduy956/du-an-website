using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NONBAOHIEMVIETTIN.Models
{
    public class ItemActivityCustomer
    {
        private int id_account, id_order;
        private DateTime create_date;

        public int Id_account
        {
            get
            {
                return id_account;
            }

            set
            {
                id_account = value;
            }
        }

        public int Id_order
        {
            get
            {
                return id_order;
            }

            set
            {
                id_order = value;
            }
        }

        public DateTime Create_date
        {
            get
            {
                return create_date;
            }

            set
            {
                create_date = value;
            }
        }
    }
}