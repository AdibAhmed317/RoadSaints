using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;
using RoadSaintsAPI.DB_Config;
using RoadSaintsAPI.Models;
using RoadSaintsAPI.Repository;

namespace RoadSaintsAPI.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*", SupportsCredentials = true)]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        [HttpPost]
        [Route("addcustomer")]
        public IHttpActionResult AddCustomer([FromBody] CustomersModel customer)
        {
            CustomersRepo customersRepo = new CustomersRepo();

            if(ModelState.IsValid)
            {
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
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(CustomersModel model)
        {
            CustomersRepo customersRepo = new CustomersRepo();

            using (var context = new Bike_AccessoriesEntities1())
            {
                bool isAuthentic = context.Customers.Any(u => u.email == model.Email && u.password == model.Password);
                if (isAuthentic)
                {
                    var find = context.Customers.FirstOrDefault(u => u.email == model.Email);
                    bool isAdmin = (bool)find.isAdmin;

                    HttpCookie authCookie = new HttpCookie("AuthCookie");
                    authCookie.Values["IsAdmin"] = isAdmin.ToString();

                    authCookie.Expires = DateTime.Now.AddDays(3);

                    HttpContext.Current.Response.Cookies.Add(authCookie);

                    CustomersModel customer = customersRepo.GetCustomerById(find.customer_id);

                    return Ok(customer);
                }
                return NotFound();
            }
        }

        [HttpPost]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            var authCookie = new HttpCookie("AuthCookie")
            {
                Expires = DateTime.Now.AddDays(-3)
            };

            HttpContext.Current.Response.Cookies.Add(authCookie);

            return Ok("Logged out successfully");
        }

        [AdminAuthorization]
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
