using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadSaintsAPI.Models
{
    public class CustomersModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool? IsAdmin { get; set; }

        // Navigation properties
        public ICollection<OrdersModel> Orders { get; set; }
        public ICollection<ShoppingCartModel> ShoppingCart { get; set; }
        public ICollection<WishlistModel> Wishlist { get; set; }

    }
}