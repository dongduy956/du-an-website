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

        [HttpPost]
        public JsonResult LoginGoogle(string username, string email, string fullname, string image)
        {
            var acc = new accounts()
            {
                username = username,
                email = email,
                fullname = fullname,
                image = image,
                issocial = 1,
                status = true,
                password = "",
                idrole = 1,
                alias = "tai-khoan-" + (db.accounts.OrderByDescending(x => x.id).FirstOrDefault().id + 1)

            };
            bool check = false;
            var accTemp = db.accounts.SingleOrDefault(x => x.email.Equals(acc.email) && x.issocial == 1);
            if (accTemp == null)
                check = true;
            if (check)
            {
                db.accounts.Add(acc);
                db.SaveChanges();
                Session["account"] = db.accounts.SingleOrDefault(x => x.email.Equals(acc.email) && x.issocial == 1);
            }
            else
            {
                if (accTemp.status == false)
                    return Json(new
                    {
                        status = 0,
                        message = "Tài khoản bị khoá."
                    });
                Session["account"] = accTemp;
            }
            return Json(new
            {
                status = 1
            });
        }
        [HandleError]
        [HttpPost]
        public JsonResult LoginFacebook(accounts acc)
        {
            if(string.IsNullOrEmpty(acc.email))
            {
                acc.email = acc.username = "";
            }
            acc.fullname = acc.fullname.Replace(" undefined ", " ");
            acc.status = true;
            acc.issocial = 2;
            acc.password = "";
            acc.idrole = 1;
            acc.status = true;
            acc.alias = "tai-khoan-" + (db.accounts.OrderByDescending(x => x.id).FirstOrDefault().id + 1);
            bool check = false;
            var accTemp = db.accounts.SingleOrDefault(x => x.email.Equals(acc.email) && x.issocial == 2);
            if (accTemp == null)
                check = true;
            if (check)
            {
                db.accounts.Add(acc);
                db.SaveChanges();
                Session["account"] = db.accounts.SingleOrDefault(x => x.email.Equals(acc.email) && x.issocial == 2);
            }
            else
            {
                if (!string.IsNullOrEmpty(acc.email) && !acc.email.Equals(accTemp.email))
                {
                    accTemp.email = accTemp.username = acc.email;
                    db.Entry(accTemp).State = EntityState.Modified;
                    db.SaveChanges();
                }
                if (accTemp.status == false)
                    return Json(new
                    {
                        status = false,
                        message = "Tài khoản của bạn bị khoá"
                    });
                Session["account"] = accTemp;
            }

            return Json(new
            {
                status = true
            });
        }
        [HandleError]

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
            Session["account"] = Session["wishSession"] = Session["cartSession"] = null;

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
                        var ServerSavePath = Path.Combine(Server.MapPath("~/assets/images/users/") + InputFileName);
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
            accounts acc = db.accounts.SingleOrDefault(x => x.username.Equals(usernamelogin) && x.password.Equals(passwordlogin) && x.issocial == 0);
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
        bool sendMail(string mailTo, string body)
        {
            // //đăng nhập mail để gửi
            string email = ConfigurationManager.AppSettings["mail"].ToString();
            string pass = ConfigurationManager.AppSettings["pass"].ToString();

            //gán thông tin
            var mess = new MailMessage(email, mailTo);
            mess.Subject = "Kích hoạt tài khoản";
            mess.Body = body;
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
                if (db.accounts.SingleOrDefault(x => x.username.Equals(acc.username) && x.issocial == 0) != null)
                    return Json(0);
                if (db.accounts.SingleOrDefault(x => x.email.Equals(acc.email) && x.issocial == 0) != null)
                    return Json(2);
                #region mã xác nhận email
                Session["code"] = randCode();
                string body = @"<!DOCTYPE html>
<html lang='en'>

<head>
    <meta charset='UTF-8'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Document</title>
    <style>
        * {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }

        body {
            padding: 16px;
        }

        .body {
            border: 1px solid #ccc;
            width: 100%;
            margin: auto;
        }

        .code {
            text-align: center;
            margin: 0 auto;
            background: rgb(35, 189, 137);
            border-radius: 50px;
            font-size: 1.5rem;
            line-height: 2rem;
            color: white;
            min-width: 150px;
            max-width: 200px;
        }

        .code_text {
            margin: auto;
            min-width: 150px;
            max-width: 200px;
            text-align: center;
            font-size: 1.5rem;
            line-height: 2rem;
        }

        .header {
            text-align: center;
            text-transform: uppercase;
            background: #00BBA6;
            color: white;
            padding: 4px;
        }

        .header h1 {
            line-height: 2.5rem;

        }

        .header h4 {

            line-height: 2rem;
        }

        footer {
            margin-top: 12px;
            text-align: center;
            text-transform: uppercase;
            background: #00BBA6;
            color: white;
            padding: 4px;

        }

        p {
            line-height: 2rem;
            padding: 0 8px;
        }
    </style>
</head>

<body>
    <div class='body'>
        <div class='header'>
            <h1>Công ty nón bảo hiểm việt tin</h1>
            <h4>Xác thực tài khoản " + acc.username + @"</h4>
        </div>
        <p>Chào bạn " + acc.fullname + @",</p>
        <p>Đây là mã xác nhận kích hoạt tài khoản của bạn. Vui lòng không cung cấp cho người khác.</p>
        <h4 class='code_text'>Mã xác nhận</h4>
        <div class='code'>
            <span>" + Session["code"] + @"</span>
        </div>

        <p>Nếu không phải bạn? Vui lòng bỏ qua email này.</p>
        <p>Đã có tài khoản?<a href='https://nonbaohiem.ml/dang-nhap'>Đăng nhập tại đây</a></p>

        <p>Xin cảm ơn,</p>
        <p>nonbaohiemviettin@gmail.com</p>
        <footer class='footer'>
            <p>Cảm ơn bạn đã đăng kí tài khoản trên <a href='https://nonbaohiem.ml'>nonbaohiem.ml</a></p>
        </footer>
    </div>
</body>

</html>";
                #endregion
                if (sendMail(acc.email, body))
                {
                    acc.image = "assets/images/users/" + acc.image;
                    acc.alias = "tai-khoan-" + (db.accounts.OrderByDescending(x => x.id).FirstOrDefault().id + 1);
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
                var acc = db.accounts.SingleOrDefault(x => x.email.Equals(email) && x.issocial == 0);
                if (acc == null)
                    return Json(0);
                #region mã xác nhận email
                Session["code"] = randCode();
                string body = @"<!DOCTYPE html>
<html lang='en'>

<head>
    <meta charset='UTF-8'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Document</title>
    <style>
        * {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }

        body {
            padding: 16px;
        }

        .body {
            border: 1px solid #ccc;
            width: 100%;
            margin: auto;
        }

        .code {
            text-align: center;
            margin: 0 auto;
            background: rgb(35, 189, 137);
            border-radius: 50px;
            font-size: 1.5rem;
            line-height: 2rem;
            color: white;
            min-width: 150px;
            max-width: 200px;
        }

        .code_text {
            margin: auto;
            min-width: 150px;
            max-width: 200px;
            text-align: center;
            font-size: 1.5rem;
            line-height: 2rem;
        }

        .header {
            text-align: center;
            text-transform: uppercase;
            background: #00BBA6;
            color: white;
            padding: 4px;
        }

        .header h1 {
            line-height: 2.5rem;

        }

        .header h4 {

            line-height: 2rem;
        }

        footer {
            margin-top: 12px;
            text-align: center;
            text-transform: uppercase;
            background: #00BBA6;
            color: white;
            padding: 4px;

        }

        p {
            line-height: 2rem;
            padding: 0 8px;
        }
    </style>
</head>

<body>
    <div class='body'>
        <div class='header'>
            <h1>Công ty nón bảo hiểm việt tin</h1>
            <h4>Xác thực tài khoản " + acc.username + @"</h4>
        </div>
        <p>Chào bạn " + acc.fullname + @",</p>
        <p>Đây là mã xác nhận kích hoạt tài khoản của bạn. Vui lòng không cung cấp cho người khác.</p>
        <h4 class='code_text'>Mã xác nhận</h4>
        <div class='code'>
            <span>" + Session["code"] + @"</span>
        </div>

        <p>Nếu không phải bạn? Vui lòng bỏ qua email này.</p>
        <p>Đã có tài khoản?<a href='https://nonbaohiem.ml/dang-nhap'>Đăng nhập tại đây</a></p>

        <p>Xin cảm ơn,</p>
        <p>nonbaohiemviettin@gmail.com</p>
        <footer class='footer'>
            <p>Cảm ơn bạn đã đăng kí tài khoản trên <a href='https://nonbaohiem.ml'>nonbaohiem.ml</a></p>
        </footer>
    </div>
</body>

</html>";
                #endregion
                if (sendMail(acc.email, body))
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
                var acc = (Session["acc"] as accounts);
                #region mã xác nhận email
                Session["code"] = randCode();
                string body = @"<!DOCTYPE html>
<html lang='en'>

<head>
    <meta charset='UTF-8'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Document</title>
    <style>
        * {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }

        body {
            padding: 16px;
        }

        .body {
            border: 1px solid #ccc;
            width: 100%;
            margin: auto;
        }

        .code {
            text-align: center;
            margin: 0 auto;
            background: rgb(35, 189, 137);
            border-radius: 50px;
            font-size: 1.5rem;
            line-height: 2rem;
            color: white;
            min-width: 150px;
            max-width: 200px;
        }

        .code_text {
            margin: auto;
            min-width: 150px;
            max-width: 200px;
            text-align: center;
            font-size: 1.5rem;
            line-height: 2rem;
        }

        .header {
            text-align: center;
            text-transform: uppercase;
            background: #00BBA6;
            color: white;
            padding: 4px;
        }

        .header h1 {
            line-height: 2.5rem;

        }

        .header h4 {

            line-height: 2rem;
        }

        footer {
            margin-top: 12px;
            text-align: center;
            text-transform: uppercase;
            background: #00BBA6;
            color: white;
            padding: 4px;

        }

        p {
            line-height: 2rem;
            padding: 0 8px;
        }
    </style>
</head>

<body>
    <div class='body'>
        <div class='header'>
            <h1>Công ty nón bảo hiểm việt tin</h1>
            <h4>Xác thực tài khoản " + acc.username + @"</h4>
        </div>
        <p>Chào bạn " + acc.fullname + @",</p>
        <p>Đây là mã xác nhận kích hoạt tài khoản của bạn. Vui lòng không cung cấp cho người khác.</p>
        <h4 class='code_text'>Mã xác nhận</h4>
        <div class='code'>
            <span>" + Session["code"] + @"</span>
        </div>

        <p>Nếu không phải bạn? Vui lòng bỏ qua email này.</p>
        <p>Đã có tài khoản?<a href='https://nonbaohiem.ml/dang-nhap'>Đăng nhập tại đây</a></p>

        <p>Xin cảm ơn,</p>
        <p>nonbaohiemviettin@gmail.com</p>
        <footer class='footer'>
            <p>Cảm ơn bạn đã đăng kí tài khoản trên <a href='https://nonbaohiem.ml'>nonbaohiem.ml</a></p>
        </footer>
    </div>
</body>

</html>";
                #endregion

                sendMail(acc.email, body);
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
                Session["acc"] = Session["code"] = null;
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

        [HandleError]
        public ActionResult AccountInfo(int page = 1)
        {

            var acc = Session["account"] as accounts;
            if (acc == null)
                return Redirect("/");
            var temp = db.order.Where(x => x.idaccount == acc.id).ToList();
            var lstOrder = temp.ToPagedList(page, pageSize);
            ViewBagNoti(temp, page);
            return View(lstOrder);
        }
        [HttpPost]
        public JsonResult update(accounts acc)
        {

            var accSession = Session["account"] as accounts;
            if (db.accounts.SingleOrDefault(x => !x.username.Equals(accSession.username) && x.email.Equals(acc.email) && x.issocial == 0 && accSession.issocial == 0) != null)
                return Json(new
                {
                    status = -1,
                    message = "Email này đã được sử dụng."
                });
            acc.id = accSession.id;
            acc.password = accSession.password;
            acc.issocial = accSession.issocial;
            acc.status = accSession.status;
            acc.idrole = accSession.idrole;
            acc.username = accSession.username;
            acc.alias = accSession.alias;
            if (!acc.image.Contains("assets/images/users/") && acc.issocial == 0)
                acc.image = "assets/images/users/" + acc.image;
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
            Session["account"] = db.accounts.SingleOrDefault(x => x.id == acc.id);
            return Json(new
            {
                status = 1,
                message = "Cập nhật tài khoản thành công!!!",
                fullname = acc.fullname,
                image = acc.image
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
