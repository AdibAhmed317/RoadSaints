using RoadSaintsAPI.Models;
using RoadSaintsAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;

namespace RoadSaintsAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        [HttpPost]
        [Route("addorder")]
        public IHttpActionResult CreateOrderAndDetails([FromBody] JObject combinedData)
        {
            if (combinedData == null)
            {
                return BadRequest("Invalid input data");
            }

            dynamic dynamicData = combinedData.ToObject<dynamic>();

            OrdersModel order = dynamicData.order.ToObject<OrdersModel>();
            List<OrderDetailsModel> orderDetailsList = dynamicData.orderDetails.ToObject<List<OrderDetailsModel>>();

            OrderRepo orderRepo = new OrderRepo();
            bool success = orderRepo.AddOrderAndDetails(order, orderDetailsList);

            if (success)
            {
                return Ok("Order added successfully");
            }

            return InternalServerError();
        }

        [HttpGet]
        [Route("history/{customerId}")]
        public IHttpActionResult GetOrderByCustomerId(int customerId)
        {
            var orderRepo = new OrderRepo();
            var order = orderRepo.GetOrderByCustomerId(customerId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }


        [HttpGet]
        [Route("allorders/{customerId}")]
        public IHttpActionResult GetOrdersByCustomerId(int customerId)
        {
            OrderRepo orderRepo = new OrderRepo();

            List<OrdersModel> orders = orderRepo.GetOrdersByCustomerId2(customerId);

            return Ok(orders);
        }

        [HttpGet]
        [Route("allorders")]
        public IHttpActionResult GetAllOrders()
        {
            OrderRepo orderRepo = new OrderRepo();

            try
            {
                var orders = orderRepo.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
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
