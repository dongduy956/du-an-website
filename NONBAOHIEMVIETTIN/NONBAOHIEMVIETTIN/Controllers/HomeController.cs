﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
namespace NONBAOHIEMVIETTIN.Controllers
{
    public class HomeController : Controller
    {
        EDO db = new EDO();
        public ActionResult Index()
        {           
            return View(db.accounts.ToList());
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}