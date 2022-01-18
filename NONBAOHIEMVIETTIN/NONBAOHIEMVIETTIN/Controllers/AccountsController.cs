using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NONBAOHIEMVIETTIN.Models;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using Facebook;
using PagedList;

namespace NONBAOHIEMVIETTIN.Controllers
{
    public class AccountsController : Controller
    {

        private nonbaohiemviettinEntities db = new nonbaohiemviettinEntities();
        int pageSize = 4;
        void ViewBagNoti(List<order> temp, int page)
        {
            if (temp.Count() > 0)
            {
                int last = int.Parse(Math.Ceiling((double)temp.Count() / pageSize).ToString());
                ViewBag.last = last;
                ViewBag.noti = "Showing " + page + "-" + last + " of " + temp.Count() + " results";
            }
        }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["idFaceBook"],
                client_secret = ConfigurationManager.AppSettings["passFaceBook"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }
        [HttpPost]
        public void LoginGoogle(string username, string email, string fullname, string image)
        {
            var acc = new accounts()
            {
                username = username,
                email = email,
                fullname = fullname,
                image = image,
                issocial = 1,
                password="",
                idrole=1
            };
            bool check = false;
            var accTemp = db.accounts.SingleOrDefault(x => x.email.Equals(acc.email)&&x.issocial==1);
            if (accTemp == null)           
                check = true;
            if (check)
            {
                db.accounts.Add(acc);
                db.SaveChanges();
                Session["account"] = db.accounts.SingleOrDefault(x => x.email.Equals(acc.email) && x.issocial == 1);
            }
            else
                Session["account"] = accTemp;
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["idFaceBook"],
                client_secret = ConfigurationManager.AppSettings["passFaceBook"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email,picture");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;
                string image = me.picture[0].url;
                var acc = new accounts();
                acc.email = email;
                acc.username = email;
                acc.status = true;
                acc.fullname = firstname + " " + middlename + " " + lastname;
                acc.image = image;
                acc.issocial = 2;
                acc.password = "";
                acc.idrole = 1;
                bool check = false;
                var accTemp = db.accounts.SingleOrDefault(x => x.email.Equals(acc.email)&&x.issocial==2);
                if (accTemp == null)               
                    check = true;
                if(check)
                {
                    db.accounts.Add(acc);
                    db.SaveChanges();
                    Session["account"] = db.accounts.SingleOrDefault(x => x.email.Equals(acc.email) && x.issocial == 2);
                }
                else
                    Session["account"] = accTemp;
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            var acc = Session["account"] as accounts;
            if (acc != null)
                return Redirect("/");
            return View();
        }
        [HttpPost]
        public JsonResult Logout()
        {
            Session.Clear();
            return Json(1);
        }
        [HttpPost]
        public JsonResult UploadImg()
        {
            if (Request.Files.Count > 0)
            {
                var files = Request.Files;

                //iterating through multiple file collection   
                foreach (string str in files)
                {
                    HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/assets/img/user/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);

                    }

                }
                return Json("File Uploaded Successfully!");
            }
            else
            {
                return Json("No files to upload");
            }
        }
        [HttpPost]
        public JsonResult Login(string usernamelogin, string passwordlogin)
        {
            passwordlogin = HoTro.Instances.EncodeMD5(passwordlogin);
            accounts acc = db.accounts.SingleOrDefault(x => x.username.Equals(usernamelogin) && x.password.Equals(passwordlogin)&&x.issocial==0);
            if (acc != null)
            {
                if (bool.Parse(acc.status.ToString()))
                {
                    Session["account"] = acc;
                    return Json(1);
                }
                else
                    return Json(0);
            }
            return Json(-1);

        }
        private string randCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[4];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new String(stringChars);
        }
        bool sendMail(string mailTo)
        {
            // //đăng nhập mail để gửi
            string email = ConfigurationManager.AppSettings["mail"].ToString();
            string pass = ConfigurationManager.AppSettings["pass"].ToString();

            //gán thông tin
            var mess = new MailMessage(email, mailTo);
            mess.Subject = "Đây là mail tự động.";
            Session["code"] = randCode();
            mess.Body = "Mã xác nhận của bạn là:" + Session["code"];
            //cho gửi định dạng html
            mess.IsBodyHtml = true;
            //cấu hình mail
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            //gửi mail đi
            NetworkCredential net = new NetworkCredential(email, pass);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = net;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smtp.Send(mess);
            }
            catch (SmtpException ex)
            {
                return false;
            }
            return true;

        }
        // POST: Accounts/Create       
        [HttpPost]
        public JsonResult Register(accounts acc)
        {
            try
            {               
                if (db.accounts.SingleOrDefault(x => x.username.Equals(acc.username)&&x.issocial==0) != null)
                    return Json(0);
                if (db.accounts.SingleOrDefault(x => x.email.Equals(acc.email)&&x.issocial==0) != null)
                    return Json(2);
                if (sendMail(acc.email))
                {
                    Session["acc"] = acc;
                    return Json(1);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(-1);

        }
        [HttpPost]
        public JsonResult ForgetPass(string email)
        {
            try
            {
                var acc = db.accounts.SingleOrDefault(x => x.email.Equals(email)&&x.issocial==0);
                if (acc == null)
                    return Json(0);
                if (sendMail(email))
                {
                    Session["acc"] = acc;
                    return Json(1);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(-1);

        }
        [HttpPost]
        public JsonResult SendAgain()
        {
            try
            {
                sendMail((Session["acc"] as accounts).email);
                return Json(1);
            }
            catch (Exception ex)
            {
                return Json(0);

            }
        }
        [HttpPost]
        public JsonResult CodeRegister(string code)
        {
            if (!Session["code"].ToString().Equals(code))
                return Json(0);
            try
            {
                var acc = Session["acc"] as accounts;
                acc.password = HoTro.Instances.EncodeMD5(acc.password);
                acc.idrole = 1;
                acc.status = true;
                acc.issocial = 0;
                db.accounts.Add(acc);
                db.SaveChanges();
                Session.Clear();
                Session.Abandon();

                return Json(1);
            }
            catch (Exception ex)
            {
                return Json(0);
            }

        }
        [HttpPost]
        public JsonResult CodeForget(string code)
        {
            if (!Session["code"].ToString().Equals(code))
                return Json(0);
            return Json(1);
        }

        [HttpPost]
        public JsonResult ChangePasswordEmail(string passNew)
        {
            var acc = Session["acc"] as accounts;
            try
            {
                acc.password = HoTro.Instances.EncodeMD5(passNew);
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
                Session.Clear();
                Session.Abandon();
                return Json(1);
            }
            catch (Exception ex)
            {

            }
            return Json(-1);
        }


        [HttpPost]
        public JsonResult ChangePassword(string passold, string passnew, string prepass)
        {
            try
            {
                var acc = Session["account"] as accounts;
                if (!acc.password.Equals(HoTro.Instances.EncodeMD5(passold)))
                    return Json(-1);
                else
                {
                    if (!passnew.Equals(prepass))
                        return Json(0);
                    acc.password = HoTro.Instances.EncodeMD5(passnew);
                    db.Entry(acc).State = EntityState.Modified;
                    db.SaveChanges();
                    Session.Clear();
                    Session.Abandon();
                    return Json(1);
                }
            }
            catch (Exception)
            {
                return Json(2);
            }

        }        
        public ActionResult AccountInfo(int page=1)
        {
                    
            var acc = Session["account"] as accounts;
            if (acc == null)
                return Redirect("/");            
            var temp =  db.order.Where(x => x.idaccount == acc.id).ToList();
           var lstOrder = temp.ToPagedList(page, pageSize);            
            ViewBagNoti(temp, page);
            return View(lstOrder);
        }
        [HttpPost]
        public JsonResult update(accounts acc)
        {
            var accSession = Session["account"] as accounts;
            acc.id = accSession.id;
            acc.password = accSession.password;
            acc.issocial = accSession.issocial;
            acc.status = accSession.status;
            acc.idrole = accSession.idrole;
            acc.username=accSession.username;

            try
            {
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = 0,
                    message = "Lỗi hệ thống.Cập nhật tài khoản thất bại!!!"
                });
            }
            Session["account"] = acc;
            return Json(new
            {
                status = 1,
                message = "Cập nhật tài khoản thành công!!!",
                fullname=acc.fullname,
                image=acc.image
            });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
