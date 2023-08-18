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
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        [HttpGet]
        [Route("all-categories")]
        public IHttpActionResult GetCategories()
        {
            CategoryRepo categoryRepo = new CategoryRepo();

            List<CategoriesModel> categories = categoryRepo.GetCategories();

            return Ok(categories);
        }

        [HttpPost]
        [Route("add-categories")]
        public IHttpActionResult AddCategory([FromBody] CategoriesModel category)
        {
            CategoryRepo categoryRepo = new CategoryRepo();

            if (category == null)
            {
                return BadRequest("Invalid input data");
            }

            bool success = categoryRepo.AddCategory(category);

            if (success)
            {
                return Ok("Category added successfully");
            }

            return InternalServerError();
        }
    }
}
