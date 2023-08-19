using System;
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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        [HttpPost]
        [Route("addproduct")]
        public IHttpActionResult AddProduct([FromBody] ProductsModel product)
        {
            ProductsRepo productRepo = new ProductsRepo();
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }

            bool isAdded = productRepo.AddProduct(product);

            if (isAdded)
            {
                return Ok("Product added successfully.");
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("allproducts")]
        public IHttpActionResult GetAllProducts()
        {
            ProductsRepo productRepo = new ProductsRepo();
            List<ProductsModel> products = productRepo.GetAllData();
            if (products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet]
        [Route("details/{productId}")]
        public IHttpActionResult GetProductById(int productId)
        {
            ProductsRepo productRepo = new ProductsRepo();
            ProductsModel product = productRepo.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut]
        [Route("update/{productId}")]
        public IHttpActionResult UpdateProductById(int productId, [FromBody] ProductsModel product)
        {
            ProductsRepo productRepo = new ProductsRepo();

            if (product == null)
            {
                return BadRequest("Product data is null.");
            }

            bool isUpdated = productRepo.UpdateProductById(productId, product);

            if (isUpdated)
            {
                return Ok("Product updated successfully.");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("deleteproduct/{productId}")]
        public IHttpActionResult DeleteProductById(int productId)
        {
            ProductsRepo productRepo = new ProductsRepo();
            bool isDeleted = productRepo.DeleteProductById(productId);

            if (isDeleted)
            {
                return Ok("Product deleted successfully.");
            }
            else
            {
                return NotFound();
            }
        }
    }
}