﻿using RoadSaintsAPI.DB_Config;
using RoadSaintsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RoadSaintsAPI.Repository
{
    public class ShoppingCartRepo
    {
        public bool AddCart(ShoppingCartModel shoppingCart)
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var newCart = new Shopping_Cart
                {
                    cart_id = shoppingCart.CartId,
                    customer_id = shoppingCart.CustomerId,
                    product_id = shoppingCart.ProductId,
                    quantity = shoppingCart.Quantity,
                };

                context.Shopping_Cart.Add(newCart);
                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }

        public List<ShoppingCartModel> GetAllCart()
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var result = context.Shopping_Cart.Select(x => new ShoppingCartModel()
                {
                    CartId = x.cart_id,
                    CustomerId = x.customer_id,
                    ProductId = x.product_id,
                    Quantity = x.quantity,
                }).ToList();

                return result;
            }
        }

        public ShoppingCartModel GetCartById(int shoppingCartId)
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var shoppingCartEntity = context.Shopping_Cart.FirstOrDefault(p => p.cart_id == shoppingCartId);
                if (shoppingCartEntity == null)
                {
                    return null;
                }

                var shoppingCartModel = new ShoppingCartModel
                {
                    CartId = shoppingCartEntity.cart_id,
                    CustomerId = shoppingCartEntity.customer_id,
                    ProductId = shoppingCartEntity.product_id,
                    Quantity = shoppingCartEntity.quantity,
                };

                return shoppingCartModel;
            }
        }

        public bool UpdateCartById(int shoppingCartId, ShoppingCartModel shoppingCart)
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var existingCart = context.Shopping_Cart.FirstOrDefault(p => p.cart_id == shoppingCartId);
                if (existingCart != null)
                {
                    existingCart.quantity = shoppingCart.Quantity;
                }
                else
                {
                    return false;
                }

                context.SaveChanges();

                return true;
            }
        }

        public bool DeleteCartById(int shoppingCartId)
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var existingCart = context.Shopping_Cart.FirstOrDefault(p => p.cart_id == shoppingCartId);
                if (existingCart == null)
                {
                    return false;
                }

                context.Shopping_Cart.Remove(existingCart);
                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }

        public bool DeleteCartByCustomerId(int customerId)
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var existingCart = context.Shopping_Cart.Where(p => p.customer_id == customerId).ToList();

                if (existingCart.Count == 0)
                {
                    return false;
                }

                context.Shopping_Cart.RemoveRange(existingCart);
                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }

    }
}