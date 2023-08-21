using RoadSaintsAPI.DB_Config;
using RoadSaintsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadSaintsAPI.Repository
{
    public class OrderRepo
    {
        public bool AddOrderAndDetails(OrdersModel order, List<OrderDetailsModel> orderDetailsList)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var newOrder = new Orders
                {
                    customer_id = order.CustomerId,
                    order_date = order.OrderDate,
                    total_amount = order.TotalAmount,
                };

                context.Orders.Add(newOrder);

                context.SaveChanges(); // Save order first to get its generated ID

                foreach (var orderDetails in orderDetailsList)
                {
                    var newDetails = new Order_Details
                    {
                        order_id = newOrder.order_id, // Use the generated order ID
                        product_id = orderDetails.ProductId,
                        quantity = orderDetails.Quantity,
                        subtotal = orderDetails.Subtotal,
                    };

                    context.Order_Details.Add(newDetails);
                }

                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }

        public List<OrdersModel> GetOrderByCustomerId(int CustomerId)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var orderEntitys = context.Orders
                    .Where(p => p.customer_id == CustomerId)
                    .ToList();

                var orderModels = orderEntitys.Select(orderEntity => new OrdersModel
                {
                    OrderId = orderEntity.order_id,
                    CustomerId = orderEntity.customer_id,
                    OrderDate = orderEntity.order_date,
                    TotalAmount = orderEntity.total_amount,
                    OrderDetails = orderEntity.Order_Details.Select(detailsEntity => new OrderDetailsModel
                    {
                        OrderDetailId = detailsEntity.order_detail_id,
                        OrderId = detailsEntity.order_id,
                        ProductId = detailsEntity.product_id,
                        Quantity = detailsEntity.quantity,
                        Subtotal = detailsEntity.subtotal,

                        Product = new ProductsModel
                        {
                            ProductName = detailsEntity.Products.product_name,
                            Price = detailsEntity.Products.price,
                        }
                    }).ToList()
                }).ToList();

                return orderModels;
            }
        }

    }
}