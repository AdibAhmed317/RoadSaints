using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RoadSaintsAPI.Models;
using RoadSaintsAPI.Repository;

namespace RoadSaintsAPI.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        [HttpPost]
        [Route("addcustomer")]
        public IHttpActionResult AddCustomer([FromBody] CustomersModel customer)
        {
            CustomersRepo customersRepo = new CustomersRepo();

            if (customer == null)
            {
                return BadRequest("Customer data is null.");
            }

            bool isAdded = customersRepo.AddCustomer(customer);

            if (isAdded)
            {
                return Ok("Customer added successfully.");
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("allcustomers")]
        public IHttpActionResult GetAllCustomers()
        {
            CustomersRepo customersRepo = new CustomersRepo();

            List<CustomersModel> customers = customersRepo.GetAllCustomers();
            if (customers == null || customers.Count == 0)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        [HttpGet]
        [Route("details/{customerId}")]
        public IHttpActionResult GetCustomerById(int customerId)
        {
            CustomersRepo customersRepo = new CustomersRepo();
            CustomersModel customer = customersRepo.GetCustomerById(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPut]
        [Route("update/{customerId}")]
        public IHttpActionResult UpdateCustomerById(int customerId, [FromBody] CustomersModel customer)
        {
            CustomersRepo customersRepo = new CustomersRepo();

            if (customer == null)
            {
                return BadRequest("Customer data is null.");
            }

            bool isUpdated = customersRepo.UpdateCustomerById(customerId, customer);

            if (isUpdated)
            {
                return Ok("Customer updated successfully.");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("deletecustomer/{customerId}")]
        public IHttpActionResult DeleteCustomerById(int customerId)
        {
            CustomersRepo customersRepo = new CustomersRepo();

            bool isDeleted = customersRepo.DeleteCustomerById(customerId);

            if (isDeleted)
            {
                return Ok("Customer deleted successfully.");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
