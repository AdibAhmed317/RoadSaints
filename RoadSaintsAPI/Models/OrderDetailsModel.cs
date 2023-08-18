using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadSaintsAPI.Models
{
    public class OrderDetailsModel
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Subtotal { get; set; }

        // Navigation properties
        public OrdersModel Order { get; set; }
        public ProductsModel Product { get; set; }
    }
}