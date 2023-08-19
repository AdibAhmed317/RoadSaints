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
        public bool AddOrderAndDetails(OrdersModel order, OrderDetailsModel orderDetails)
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

                var newDetails = new Order_Details
                {
                    order_id = newOrder.order_id, // Use the generated order ID
                    product_id = orderDetails.ProductId,
                    quantity = orderDetails.Quantity,
                    subtotal = orderDetails.Subtotal,
                };

                context.Order_Details.Add(newDetails);

                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }

        public List<OrdersModel> GetOrdersByCustomerId(int customerId)
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