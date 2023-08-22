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

                context.SaveChanges(); 

                foreach (var orderDetails in orderDetailsList)
                {
                    var newDetails = new Order_Details
                    {
                        order_id = newOrder.order_id, 
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

        public List<OrdersModel> GetOrdersByCustomerId2(int customerId)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var orders = context.Orders
                    .Where(o => o.customer_id == customerId)
                    .Select(o => new OrdersModel
                    {
                        OrderId = o.order_id,
                        CustomerId = o.customer_id,
                        OrderDate = o.order_date,
                        TotalAmount = o.total_amount
                    })
                    .ToList();

                return orders;
            }
        }

        public List<OrdersModel> GetAllOrders()
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var orders = context.Orders
                    .Select(o => new OrdersModel
                    {
                        OrderId = o.order_id,
                        CustomerId = o.customer_id,
                        OrderDate = o.order_date,
                        TotalAmount = o.total_amount
                    })
                    .ToList();

                return orders;
            }
        }

        public List<OrderDetailsModel> GetOrderDetailsByOrderId(int orderId)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var orderDetails = context.Order_Details
                    .Where(od => od.order_id == orderId)
                    .Select(od => new OrderDetailsModel
                    {
                        OrderId = od.order_id,
                        ProductId = od.product_id,
                        Quantity = od.quantity,
                        Subtotal = od.subtotal
                    })
                    .ToList();

                return orderDetails;
            }
        }

    }
}