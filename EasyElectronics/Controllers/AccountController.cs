using EasyElectronics.Models;
using EasyElectronics.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EasyElectronics.Controllers
{
    public class AccountController : Controller
    {
        EasyElecDBEntities db = new EasyElecDBEntities();
       
        [Authorize]
        public ActionResult ChangePassword()
        {



            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel ch)
        {



            string username = Session["username"].ToString();

            tblUser us = db.tblUsers.Where(u => u.Username == username && u.Password == ch.OldPassword).FirstOrDefault();
            if (us != null)
            {
                us.Password = ch.NewPassword;
                db.SaveChanges();

            }
            else
            {
                return Json(new { success = false, message = "You Enter Wrong Password" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, message = "Password Changed Successfully" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ForgetPassword()
        {
            return View();


        }
        [ValidateOnlyIncomingValuesAttribute]
        [HttpPost]

        public ActionResult ForgetPassword(UserViewModel uv)
        {

            if (ModelState.IsValid)
            {
                //https://www.google.com/settings/security/lesssecureapps
                //Make Access for less secure apps=true

                string from = "seventythreeplusten@gmail.com";
                using (MailMessage mail = new MailMessage(from, uv.Email))
                {
                    try
                    {
                        tblUser tb = db.tblUsers.Where(u => u.Email == uv.Email).FirstOrDefault();
                        if (tb != null)
                        {
                            mail.Subject = "Password Recovery";
                            mail.Body = "Your Password is:" + tb.Password;

                            mail.IsBodyHtml = false;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential networkCredential = new NetworkCredential(from, "vbproject123");
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = networkCredential;
                            smtp.Port = 587;
                            smtp.Send(mail);
                            ViewBag.Message = "Your Password Is Sent to your email";
                        }
                        else
                        {
                            ViewBag.Message = "email Doesnot Exist in Database";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {

                    }

                }

            }
            return View();


            //return RedirectToAction("Index", "Home");
        }
    }
}