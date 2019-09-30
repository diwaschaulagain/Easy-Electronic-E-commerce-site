using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyElectronics.Models.ViewModel
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}