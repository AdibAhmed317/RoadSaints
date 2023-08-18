using RoadSaintsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadSaintsAPI
{
    public class CombinedOrderData
    {
        public OrdersModel Order { get; set; }
        public OrderDetailsModel OrderDetails { get; set; }
    }
}