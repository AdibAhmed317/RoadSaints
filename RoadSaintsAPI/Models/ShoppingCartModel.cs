using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadSaintsAPI.Models
{
    public class ShoppingCartModel
    {
        public int CartId { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        // Navigation properties
        public CustomersModel Customer { get; set; }
        public ProductsModel Product { get; set; }
    }
}