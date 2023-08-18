using RoadSaintsAPI.Models;
using RoadSaintsAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoadSaintsAPI.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        [HttpPost]
        [Route("create-order")]
        public IHttpActionResult CreateOrderAndDetails([FromBody] CombinedOrderData combinedData)
        {
            if (combinedData == null || combinedData.Order == null || combinedData.OrderDetails == null)
            {
                return BadRequest("Invalid input data");
            }

            OrderRepo orderRepo = new OrderRepo();
            bool success = orderRepo.AddOrderAndDetails(combinedData.Order, combinedData.OrderDetails);

            if (success)
            {
                return Ok("Order added successfully");
            }

            return InternalServerError();
        }

        [HttpGet]
        [Route("allorders/{customerId}")]
        public IHttpActionResult GetOrdersByCustomerId(int customerId)
        {
            OrderRepo orderRepo = new OrderRepo();

            List<OrdersModel> orders = orderRepo.GetOrdersByCustomerId(customerId);

            return Ok(orders);
        }

        [HttpGet]
        [Route("order-details/{orderId}")]
        public IHttpActionResult GetOrderDetailsByOrderId(int orderId)
        {
            OrderRepo orderRepo = new OrderRepo();

            List<OrderDetailsModel> orderDetails = orderRepo.GetOrderDetailsByOrderId(orderId);

            return Ok(orderDetails);
        }
    }
}
