using NONBAOHIEMVIETTIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NONBAOHIEMVIETTIN.Controllers
{
    public class WishController : Controller
    {
        public const string wishSession = "wishSession";
        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        [HandleError]

        public ActionResult Index()
        {
            var wish = Session[wishSession];
            var list = new List<CartItem>();
            if (wish != null)
            {
                list = (List<CartItem>)wish;
            }
            return View(list);
        }
        [HttpPost]
        public JsonResult AddItem(int ProductId, int Quantity)
        {
            if (Session["account"] == null)
                return Json(new { status = -1 }, JsonRequestBehavior.AllowGet);
            else
                try
                {
                    var wish = Session[wishSession];//bien wish co ten la wishSession
                    var p = db.products.Find(ProductId);
                    if (Quantity > p.quantity)
                        return Json(new
                        {
                            status = -3,
                            message = "Không đủ hàng"
                        });
                    //tim kiem san pham trong db, Id=1
                    if (wish != null)
                    {//gio da co sp
                        var list = (List<CartItem>)wish;
                        if (list.Exists(x => x.Product.id == ProductId))
                        {
                            foreach (var item in list)
                            {
                               
                                if (item.Product.id == ProductId)
                                {
                                    if ((item.Quantity + Quantity) > p.quantity)
                                        return Json(new
                                        {
                                            status = -3,
                                            message = "Không đủ hàng"
                                        });
                                    item.Quantity += Quantity;
                                    return Json(new
                                    {
                                        status = 1,
                                        id = item.Product.id,
                                        image = item.Product.image,
                                        name = item.Product.name,
                                        price = HoTro.Instances.convertVND(item.Product.promationprice>0?item.Product.promationprice.ToString():item.Product.price.ToString()),
                                        quantity = item.Quantity,
                                        alias = item.Product.alias,
                                        sumQuantity=list.Sum(x=>x.Quantity)
                                    });
                                }

                            }
                        }
                        else
                        {
                            var item = new CartItem();
                            item.Product = p;
                            item.Quantity = Quantity;
                            list.Add(item);
                            Session[wishSession] = list; //save
                            return Json(new
                            {
                                status = 0,
                                id = item.Product.id,
                                image = item.Product.image,
                                name = item.Product.name,
                                price = HoTro.Instances.convertVND(item.Product.promationprice > 0 ? item.Product.promationprice.ToString() : item.Product.price.ToString()),
                                quantity = item.Quantity,
                                alias = item.Product.alias,
                                sumQuantity = list.Sum(x => x.Quantity)
                            });
                        }

                    }
                    else
                    { //gio hang moi
                        var item = new CartItem();
                        item.Product = p;
                        item.Quantity = Quantity;
                        var list = new List<CartItem>();
                        list.Add(item);
                        Session[wishSession] = list;//save 
                        return Json(new
                        {
                            status = 0,
                            id = item.Product.id,
                            image=item.Product.image,
                            name=item.Product.name,
                            price = HoTro.Instances.convertVND(item.Product.promationprice > 0 ? item.Product.promationprice.ToString() : item.Product.price.ToString()),
                            quantity = item.Quantity,
                            alias = item.Product.alias,
                            sumQuantity=Quantity
                        });
                    }
                }
                catch (Exception ex)
                {
                }
            return Json(new { status = -2 }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteItem(int ProductId)
        {
            var cart = Session[wishSession];
            var p = db.products.Find(ProductId);
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.id == ProductId))
                {
                    list.RemoveAll(r => r.Product.id == ProductId);
                    Session[wishSession] = list;
                    return Json(new { status = 1, sumQuantity = list.Sum(x => x.Quantity) });
                }
            }
            return Json(new { status = 0 });

        }
    }
}