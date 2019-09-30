using EasyElectronics.Models;
using EasyElectronics.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLogin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult ManageCategory()
        {
            return View();
        }

        public JsonResult GetData()
        {
            using (EasyElecDBEntities db = new EasyElecDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<CategoryViewModel> lst = new List<CategoryViewModel>();
                var catList = db.tblCategories.ToList();
                foreach (var item in catList)
                {
                    lst.Add(new CategoryViewModel() { CategoryId = item.CategoryId, CategoryName = item.CategoryName });
                }
                return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                using (EasyElecDBEntities db = new EasyElecDBEntities())
                {
                    ViewBag.Action = "New Category";
                    return View(new CategoryViewModel());
                }
            }
            else
            {
                using (EasyElecDBEntities db = new EasyElecDBEntities())
                {
                    CategoryViewModel sub = new CategoryViewModel();
                    var menu = db.tblCategories.Where(x => x.CategoryId == id).FirstOrDefault();
                    sub.CategoryId = menu.CategoryId;
                    sub.CategoryName = menu.CategoryName;
                    ViewBag.Action = "Edit Category";
                    return View(sub);
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(CategoryViewModel sm)
        {
            using (EasyElecDBEntities db = new EasyElecDBEntities())
            {
                if (sm.CategoryId == 0)
                {
                    tblCategory tb = new tblCategory();
                    tb.CategoryName = sm.CategoryName;
                    db.tblCategories.Add(tb);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tblCategory tbm = db.tblCategories.Where(m => m.CategoryId == sm.CategoryId).FirstOrDefault();
                    tbm.CategoryName = sm.CategoryName;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (EasyElecDBEntities db = new EasyElecDBEntities())
            {
                tblCategory sm = db.tblCategories.Where(x => x.CategoryId == id).FirstOrDefault();
                db.tblCategories.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}