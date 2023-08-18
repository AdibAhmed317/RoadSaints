using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadSaintsAPI.Models
{
    public class ProductsModel
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public string ImageUrl { get; set; }

        // Navigation properties
        public CategoriesModel Category { get; set; }
        public ICollection<OrderDetailsModel> OrderDetails { get; set; }
        public ICollection<ShoppingCartModel> ShoppingCart { get; set; }
        public ICollection<WishlistModel> Wishlist { get; set; }
    }
}