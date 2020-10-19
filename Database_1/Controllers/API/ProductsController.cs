using Database_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Database_1.Dao;

namespace Database_1.Controllers.API
{
    public class ProductsController : ApiController
    {
		private ApplicationDbContext _context;
		public ProductsController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpGet]
		public IHttpActionResult GetAllProducts()
		{
			var products = _context.Products.ToList();

			return Ok(products);
		}

		[HttpGet]

		public IHttpActionResult GetProductById(int id)
		{
			var product = _context.Products
				.Include(p => p.Category)
				.SingleOrDefault(p => p.Id == id);

			if (product == null)
				return NotFound();

			var productDao = new ProductDao
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				CategoryName = product.Category.Name
			};

			return Ok(productDao);
		}

		[HttpDelete]
		public IHttpActionResult DeleteById(int id)
		{
			var product = _context.Products
				.Include(p => p.Category)
				.SingleOrDefault(p => p.Id == id);

			if (product == null)
				return NotFound();

			_context.Products.Remove(product);
			_context.SaveChanges();

			return Ok("Product with Id " + product.Id + " deleted...");
		}
		[HttpPost]
		public IHttpActionResult Create(Product product)
		{
			var doesProductExist = _context.Products.Any(p => p.Name.Contains(product.Name));

			if (doesProductExist)
				return BadRequest();

			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == product.CategoryId);

			if (categoryInDb == null)
				return BadRequest();

			var newProduct = new Product
			{
				Name = product.Name,
				Price = product.Price,
				CategoryId = product.CategoryId
			};

			_context.Products.Add(newProduct);
			_context.SaveChanges();

			return StatusCode(HttpStatusCode.Created);
		}

		[HttpPut]
		public IHttpActionResult EditById(int id, Product product)
		{
			// find product by Product ID
			var productInDb = _context.Products.SingleOrDefault(p => p.Id == id);

			if (productInDb == null)
				return NotFound();

			// find category by category ID
			var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == product.CategoryId);

			if (categoryInDb == null)
				return BadRequest();



			var isProductNameExsists = _context.Products.Any(p => p.Name.Contains(product.Name));


			if (isProductNameExsists) 
				return BadRequest();

			productInDb.Name = product.Name;
			productInDb.Price = product.Price;
			productInDb.CategoryId = product.CategoryId;

			_context.SaveChanges();

			return StatusCode(HttpStatusCode.NoContent);
		}


	}
}
