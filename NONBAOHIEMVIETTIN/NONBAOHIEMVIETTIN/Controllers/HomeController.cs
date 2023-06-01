using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
using System.Xml.Linq;
using System.Globalization;
using System.Text;

namespace NONBAOHIEMVIETTIN.Controllers
{
    public enum SitemapFrequency
    {
        Never,
        Yearly,
        Monthly,
        Weekly,
        Daily,
        Hourly,
        Always
    }
    public class HomeController : Controller
    {
        nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        [HandleError]
        public ActionResult Index()
        {
            List<ItemActivityCustomer> lstAcCus = new List<ItemActivityCustomer>();
            foreach(var item in db.accounts)
            {
                lstAcCus.Add(new ItemActivityCustomer() {
                    Id_order=-1,
                    Id_account=item.id,
                    Create_date=(DateTime)item.create_date
                });
            }
            foreach (var item in db.order)
            {
                lstAcCus.Add(new ItemActivityCustomer()
                {
                    Id_order = item.id,
                    Id_account = (int)item.idaccount,
                    Create_date = (DateTime)item.createdate
                });
            }
            ViewData["ac_cus"] = lstAcCus;
            return View(db.products.Where(x=> x.isdelete == false && x.status == true).ToList());

        }
        public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");
            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                xmlns + "url",
                new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                sitemapNode.LastModified == null ? null : new XElement(
                xmlns + "lastmod",
                sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                sitemapNode.Frequency == null ? null : new XElement(
                xmlns + "changefreq",
                sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                sitemapNode.Priority == null ? null : new XElement(
                xmlns + "priority",
                sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }
            XDocument document = new XDocument(root);
            return document.ToString();
        }

        [Route("sitemap.xml")]
        public ActionResult SitemapXml()
        {
            var sitemapNodes = GetSitemapNodes();
            string xml = GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "text/xml", Encoding.UTF8);
        }

        public IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();
            string url = "https://"+ Request.Url.Authority;
            nodes.Add(
                new SitemapNode()
                {
                    Url = url,
                    Priority = 1
                });
            nodes.Add(
               new SitemapNode()
               {
                   Url = url + "/gioi-thieu",
                   Priority = 0.9
               });
            nodes.Add(
                new SitemapNode()
                {
                    Url = url + "/dang-nhap",
                    Priority = 0.9
                });
            nodes.Add(
               new SitemapNode()
               {
                   Url = url + "/gio-hang",
                   Priority = 0.9
               });            
            nodes.Add(
            new SitemapNode()
            {
                Url = url + "/lien-he",
                Priority = 0.9
            });

            foreach (var production in db.production.Where(x => x.isdelete == false).ToList())
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = url + "/" + production.alias+"",
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.8
                   });
            }

            foreach (var newstype in db.newstype.ToList())
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = url + "/tin-tuc/" + newstype.alias + "",
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.8
                   });
            }
            foreach (var category in db.category.Where(x => x.isdelete == false))
            {
                foreach (var groupproduct in db.groupproduct.Where(x => x.isdelete == false).ToList())
                { 
                    nodes.Add(
                   new SitemapNode()
                   {
                       Url = url + "/"+category.alias+"/" + groupproduct.alias + "",
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.8
                   });
                }
            }
            foreach (var produtct in db.products.Where(x => x.isdelete == false && x.status == true).ToList())
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = url + "/chi-tiet/" + produtct.alias + "",
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.8
                   });
            }
            foreach (var produtct in db.news.ToList())
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = url + "/tin-tuc/chi-tiet/" + produtct.alias + "",
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.7
                   });
            }
            return nodes;
        }
    }
}