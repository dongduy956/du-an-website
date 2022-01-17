using NONBAOHIEMVIETTIN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Controllers
{
    public class NewsController : Controller
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 10;
        void ViewBagNoti(List<news> temp, int page)
        {
            ViewBag.last = 1;
            if (temp.Count() > 0)
            {
                int last = int.Parse(Math.Ceiling((double)temp.Count() / pageSize).ToString());
                ViewBag.last = last;
                ViewBag.noti = "Showing " + page + "-" + last + " of " + temp.Count() + " results";
            }
        }
        // GET: Products
        public ActionResult Index(string alias, int page = 1)
        {
            var temp = db.news.Where(x => x.newstype.alias.Equals(alias)).OrderByDescending(x => x.id).ToList();
            var news = temp.ToPagedList(page, pageSize);
            ViewBag.newstype = db.newstype.SingleOrDefault(x => x.alias.Equals(alias));
            ViewBagNoti(temp, page);
            return View(news);
        }
        public ActionResult Newsdetail(string alias)
        {
            news _news = new news();
            _news = db.news.SingleOrDefault(x => x.alias.Equals(alias));            
            return View(_news);
        }
    }
}
