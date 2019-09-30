using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyElectronics.Models
{
    public class ItemDB
    {
        public static List<tblProduct> GetAllSpecialItem()
        {
            using (var context = new EasyElecDBEntities())
            {
                return context.tblProducts.OrderByDescending(e => e.ProductId).Where(s => s.IsSpecial == true).Take(8).ToList();
            }
        }
        public static List<tblProduct> GetAllItems()
        {
            using (var context = new EasyElecDBEntities())
            {
                return context.tblProducts.Where(s => s.IsSpecial == false).Take(3).ToList();
            }
        }

        public static List<tblProduct> GetMobile()
        {
            using (var context = new EasyElecDBEntities())
            {
                return context.tblProducts.OrderByDescending(e => e.ProductId).Where(s => s.CategoryId==5).Take(3).ToList();
            }
        }
        public static List<tblProduct> GetAudio()
        {
            using (var context = new EasyElecDBEntities())
            {
                return context.tblProducts.OrderByDescending(e => e.ProductId).Where(s => s.CategoryId == 3).Take(4).ToList();
            }
        }

    }
}