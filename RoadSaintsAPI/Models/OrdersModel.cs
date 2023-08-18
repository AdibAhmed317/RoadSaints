using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadSaintsAPI.Models
{
    public class OrdersModel
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }

        // Navigation properties
        public CustomersModel Customer { get; set; }
        public ICollection<OrderDetailsModel> OrderDetails { get; set; }
    }
}