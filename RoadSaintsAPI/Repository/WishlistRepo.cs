using RoadSaintsAPI.DB_Config;
using RoadSaintsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace RoadSaintsAPI.Repository
{
    public class WishlistRepo
    {
        public bool AddToWishlist(WishlistModel wishlistItem)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var newWishlistItem = new Wishlist
                {
                    wishlist_id = wishlistItem.WishlistId,
                    customer_id = wishlistItem.CustomerId,
                    product_id = wishlistItem.ProductId,
                };

                context.Wishlist.Add(newWishlistItem);
                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }

        public List<WishlistModel> GetAllWishlistItems()
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var result = context.Wishlist.Select(x => new WishlistModel()
                {
                    WishlistId = x.wishlist_id,
                    CustomerId = x.customer_id,
                    ProductId = x.product_id,
                }).ToList();

                return result;
            }
        }

        public List<WishlistModel> GetWishlistItemsByCustomerId(int customerId)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var wishlistItemEntities = context.Wishlist
                    .Where(w => w.customer_id == customerId)
                    .ToList();

                var wishlistItemModels = wishlistItemEntities.Select(wishlistItemEntity => new WishlistModel
                {
                    WishlistId = wishlistItemEntity.wishlist_id,
                    CustomerId = wishlistItemEntity.customer_id,
                    ProductId = wishlistItemEntity.product_id,
                    Product = new ProductsModel
                    {
                        ProductName = wishlistItemEntity.Products.product_name,
                        Price = wishlistItemEntity.Products.price,
                    }
                }).ToList();

                return wishlistItemModels;
            }
        }


        public bool DeleteWishlistItemById(int wishlistItemId)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var existingWishlistItem = context.Wishlist.FirstOrDefault(p => p.wishlist_id == wishlistItemId);
                if (existingWishlistItem == null)
                {
                    return false;
                }

                context.Wishlist.Remove(existingWishlistItem);
                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }

        public bool DeleteWishlistItemsByCustomerId(int customerId)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var existingWishlistItems = context.Wishlist.Where(p => p.customer_id == customerId).ToList();

                if (existingWishlistItems.Count == 0)
                {
                    return false;
                }

                context.Wishlist.RemoveRange(existingWishlistItems);
                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }
    }
}