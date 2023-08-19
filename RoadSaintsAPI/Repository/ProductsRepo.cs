using RoadSaintsAPI.Models;
using RoadSaintsAPI.DB_Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RoadSaintsAPI.Repository
{
    public class ProductsRepo
    {
        public bool AddProduct(ProductsModel product)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var newProduct = new Products
                {
                    product_name = product.ProductName,
                    description = product.Description,
                    price = product.Price,
                    stock_quantity = product.StockQuantity,
                    image_url = product.ImageUrl,
                    category_id = product.Category?.CategoryId // Check if CategoryId is provided
                };

                context.Products.Add(newProduct);
                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }

        public List<ProductsModel> GetAllData()
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var result = context.Products.Select(x => new ProductsModel()
                {
                    ProductId = x.product_id,
                    ProductName = x.product_name,
                    Description = x.description,
                    Price = x.price,
                    ImageUrl = x.image_url,
                    StockQuantity = x.stock_quantity,
                    Category = new CategoriesModel
                    {
                        CategoryId = x.Categories.category_id,
                        CategoryName = x.Categories.category_name,
                    }
                }).ToList();

                return result;
            }
        }

        public ProductsModel GetProductById(int productId)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var productEntity = context.Products.FirstOrDefault(p => p.product_id == productId);
                if (productEntity == null)
                {
                    return null;
                }

                var productModel = new ProductsModel
                {
                    ProductId = productEntity.product_id,
                    ProductName = productEntity.product_name,
                    Description = productEntity.description,
                    Price = productEntity.price,
                    ImageUrl = productEntity.image_url,
                    StockQuantity = productEntity.stock_quantity,
                    Category = new CategoriesModel
                    {
                        CategoryId = productEntity.Categories.category_id,
                        CategoryName = productEntity.Categories?.category_name
                    }
                };

                return productModel;
            }
        }

        public bool UpdateProductById(int productId, ProductsModel product)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var existingProduct = context.Products.FirstOrDefault(p => p.product_id == productId);
                if (existingProduct != null)
                {
                    existingProduct.product_name = product.ProductName;
                    existingProduct.description = product.Description;
                    existingProduct.price = product.Price;
                    existingProduct.stock_quantity = product.StockQuantity;
                    existingProduct.image_url = product.ImageUrl;
                    existingProduct.category_id = product.Category.CategoryId;
                }
                else
                {
                    return false; 
                }

                context.SaveChanges();

                return true;
            }
        }

        public bool DeleteProductById(int productId)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var existingProduct = context.Products.FirstOrDefault(p => p.product_id == productId);
                if (existingProduct == null)
                {
                    return false; 
                }

                context.Products.Remove(existingProduct);
                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }
    }
}