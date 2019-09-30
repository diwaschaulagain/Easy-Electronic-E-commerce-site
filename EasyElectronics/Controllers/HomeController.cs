using EasyElectronics.Models;
using EasyElectronics.Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EasyElectronics.Controllers
{
    public class HomeController : Controller
    {
        EasyElecDBEntities db = new EasyElecDBEntities();
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult ViewItem(int id)
        {
            return PartialView("_ViewItem", db.tblProducts.Find(id));
        }
        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Login(LoginViewModel l, string ReturnUrl = "")
        {


            var users = db.tblUsers.Where(a => a.Username == l.Username && a.Password == l.Password).FirstOrDefault();
            if (users != null)
            {
                //saving value in session
                Session.Add("username", users.Username);
                FormsAuthentication.SetAuthCookie(l.Username, l.RememberMe);
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    Session.Add("userid", users.UserId);
                    if (users.tblUserRoles.Where(r => r.RoleId == 1).FirstOrDefault() != null)
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid User");
            }

            return View();

        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult ProductList(string search, int? page, int id = 0)
        {

            if (id != 0)
            {

                return View(db.tblProducts.Where(p => p.CategoryId == id).ToList().ToPagedList(page ?? 1, 4));
            }
            else
            {
                if (search != "")
                {
                    return View(db.tblProducts.Where(x => x.Description.Contains(search) || x.ProductName.Contains(search) || search == null).ToList().ToPagedList(page ?? 1, 4));
                }
                else
                {
                    return View(db.tblProducts.ToList().ToPagedList(page ?? 1, 4));
                }

            }

        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(UserViewModel uv)
        {
            tblUser tbl = db.tblUsers.Where(u => u.Username == uv.Username).FirstOrDefault();
            if (tbl != null)
            {
                return Json(new { success = false, message = "User Already Registerd" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                tblUser tb = new tblUser();
                tb.Username = uv.Username;
                tb.Password = uv.Password;
                tb.Fullname = uv.Fullname;
                tb.Email = uv.Email;
                db.tblUsers.Add(tb);
                db.SaveChanges();

                tblUserRole ud = new tblUserRole();
                ud.UserId = tb.UserId;
                ud.RoleId = 2;
                db.tblUserRoles.Add(ud);
                db.SaveChanges();
                return Json(new { success = true, message = "User Registered Successfully" }, JsonRequestBehavior.AllowGet);
            }



        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}