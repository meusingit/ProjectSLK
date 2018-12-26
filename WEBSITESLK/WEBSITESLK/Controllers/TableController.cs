using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using WEBSITESLK.Models;
using System.Web.Security;
using System.Data.Entity;

namespace WEBSITESLK.Controllers
{
    public class TableController : Controller
    {
        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        //Registration POST action 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] Table table)
        {
            bool Status = false;
            string Message = "";

            if (ModelState.IsValid)
            {
                //email check
                var isExist = IsEmailExist(table.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email Already Exists");
                    return View(table);

                }

                table.ActivationCode = Guid.NewGuid();
                table.Password = Crypto.Hash(table.Password);
                table.ConfirmPassword = Crypto.Hash(table.ConfirmPassword);
                table.IsEmailVerified = false;
                using (alphaShakeEntities2 dc = new alphaShakeEntities2())
                {
                    dc.Tables.Add(table);
                    dc.SaveChanges();
                    SendVerificationLinkEmail(table.EmailID, table.ActivationCode.ToString());
                    Message = "Registration successfullydone. Account activation link sent to your email:" + table.EmailID;
                    Status = true;
                }
            }
            else
            {
                Message = "Invalid Request";
            }
            ViewBag.Status = Status;
            ViewBag.Message = Message;
            return View(table);
        }
        //Verify Account

            [HttpGet]
            public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (alphaShakeEntities2 dc = new alphaShakeEntities2())
            {
                dc.Configuration.ValidateOnSaveEnabled = false; //confirm password doesnot match issue
                var v = dc.Tables.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid request for email verification.";
                }
            }
            ViewBag.Status = Status;
            return View();
        }


        //Login 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (alphaShakeEntities2 dc = new alphaShakeEntities2())
            {
                var v = dc.Tables.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (!v.IsEmailVerified)
                    {
                        ViewBag.Message = "Please verify your email first";
                        return View();
                    }

                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Table");
        }




        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (alphaShakeEntities2 dc = new alphaShakeEntities2())
            {
                var v = dc.Tables.Where(a => a.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Table/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("meusingit@gmail.com", "MEUsingIt");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "9008659029##manoj#15327242500100647701"; //becareful, youre selling pw.
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your account is" +
                " successfully created. Please click on the below link to verify your account" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
    }
}