using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyElectronics.Models
{
    public class MyMenu
    {
        public static List<tblCategory> GetMenus()
        {
            using (var context = new EasyElecDBEntities())
            {
                return context.tblCategories.ToList();
            }
        }
        public static List<tblCategory> GetSubMenus(int menuid)
        {
            using (var context = new EasyElecDBEntities())
            {
                return context.tblCategories.Where(u => u.CategoryId == menuid).ToList();
            }
        }
    }
}