using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using RoadSaintsAPI.DB_Config;
using RoadSaintsAPI.Models;
using RoadSaintsAPI.Repository;

namespace RoadSaintsAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    [RoutePrefix("api/shoppingcart")]
    public class ShoppingCartController : ApiController
    {
        [HttpPost]
        [Route("addcart")]
        public IHttpActionResult AddCart([FromBody] ShoppingCartModel shoppingCart)
        {
            ShoppingCartRepo shoppingCartRepo = new ShoppingCartRepo();

            if (shoppingCart == null)
            {
                return BadRequest("Shopping cart data is null.");
            }

            bool isAdded = shoppingCartRepo.AddCart(shoppingCart);

            if (isAdded)
            {
                return Ok("Shopping cart added successfully.");
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("allcarts")]
        public IHttpActionResult GetAllCarts()
        {
            ShoppingCartRepo shoppingCartRepo = new ShoppingCartRepo();

            List<ShoppingCartModel> shoppingCarts = shoppingCartRepo.GetAllCart();
            if (shoppingCarts == null || shoppingCarts.Count == 0)
            {
                return NotFound();
            }
            return Ok(shoppingCarts);
        }

        [HttpGet]
        [Route("details/{customerId}")]
        public IHttpActionResult GetCartById(int customerId)
        {
            var shoppingCartRepo = new ShoppingCartRepo();
            var cart = shoppingCartRepo.GetCartById(customerId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPut]
        [Route("update/{cartId}")]
        public IHttpActionResult UpdateCartById(int cartId, [FromBody] ShoppingCartModel shoppingCart)
        {
            ShoppingCartRepo shoppingCartRepo = new ShoppingCartRepo();

            if (shoppingCart == null)
            {
                return BadRequest("Shopping cart data is null.");
            }

            bool isUpdated = shoppingCartRepo.UpdateCartById(cartId, shoppingCart);

            if (isUpdated)
            {
                return Ok("Shopping cart updated successfully.");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("deletecart/{cartId}")]
        public IHttpActionResult DeleteCartById(int cartId)
        {
            ShoppingCartRepo shoppingCartRepo = new ShoppingCartRepo();

            bool isDeleted = shoppingCartRepo.DeleteCartById(cartId);

            if (isDeleted)
            {
                return Ok("Shopping cart deleted successfully.");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("deletecartsbycustomer/{customerId}")]
        public IHttpActionResult DeleteCartsByCustomerId(int customerId)
        {
            ShoppingCartRepo shoppingCartRepo = new ShoppingCartRepo();
            bool isDeleted = shoppingCartRepo.DeleteCartByCustomerId(customerId);

            if (isDeleted)
            {
                return Ok("Shopping carts deleted successfully.");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("itemcount/{customerId}")]
        public IHttpActionResult GetCartItemCount(int customerId)
        {
            var shoppingCartRepo = new ShoppingCartRepo();
            int itemCount = shoppingCartRepo.GetCartItemCount(customerId);
            return Ok(itemCount);
        }

    }
}
