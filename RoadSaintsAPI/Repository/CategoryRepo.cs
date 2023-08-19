using RoadSaintsAPI.DB_Config;
using RoadSaintsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadSaintsAPI.Repository
{
    public class CategoryRepo
    {
        public List<CategoriesModel> GetCategories()
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var result = context.Categories.Select(x => new CategoriesModel()
                {
                    CategoryId = x.category_id,
                    CategoryName = x.category_name,

                }).ToList();

                return result;
            }
        }

        public bool AddCategory(CategoriesModel category)
        {
            using (var context = new Bike_AccessoriesEntities1())
            {
                var newCategory = new Categories
                {
                    category_name = category.CategoryName
                };

                context.Categories.Add(newCategory);

                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }
    }
}