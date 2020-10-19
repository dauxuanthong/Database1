using Database_1.Dao;
using Database_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Database_1.Controllers.API
{
    public class CategoriesController : ApiController
    {
        private ApplicationDbContext _context;
        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }

        // Get all infor in Categories 
        [HttpGet]
        public IHttpActionResult GetAllCategories()
        {
            var categories = _context.Categories.ToList();

            return Ok(categories);
        }

        // Get product by I
        [HttpGet]
        public IHttpActionResult GetCategoriById(int id)
        {
            var category = _context.Categories.SingleOrDefault(p => p.Id == id);

            if (category == null)
                return NotFound();

            // Controll variable that user can view
            var categoryDao = new CategoryDao
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(categoryDao);
        }


        // Delete by Id
        [HttpDelete]
        public IHttpActionResult DeleteById(int id)
		{
            var category = _context.Categories.SingleOrDefault(p => p.Id == id);

            if (category == null)
                return NotFound();

            //Delete and save status
            _context.Categories.Remove(category);
            _context.SaveChanges();

            //Return status 
            return Ok("Category with Id: " + category.Id + "/ Name: " + category.Name + " deleted");
        }

        // Create 
        [HttpPost]
        public IHttpActionResult Create(Category category)
		{
            var doesCategoriExist = _context.Categories.Any(p => p.Name.Contains(category.Name));

            if (doesCategoriExist)
                return BadRequest();

            var newCategory = new Category
            {
                Name = category.Name
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();

            return StatusCode(HttpStatusCode.Created);
        }

        [HttpPut]
        public IHttpActionResult EditById(int id, Category category)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == id);

            if (categoryInDb == null)
                return NotFound();

            if (categoryInDb == null)
                return BadRequest();

            var isProductNameExsists = _context.Categories.Any(p => p.Name.Contains(category.Name));

            if (isProductNameExsists)
                return BadRequest();

            categoryInDb.Name = category.Name;

            _context.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
