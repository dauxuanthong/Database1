using Database_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_1.Controllers
{
    public class CategoriesController : Controller
    {
		private ApplicationDbContext _context;

		public CategoriesController()
		{
			_context = new ApplicationDbContext();
		}

		//Get: Product
		[HttpGet]
		public ActionResult Index()
		{
			var categories = _context.Categories.ToList();
			return View(categories);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		//Check is exists
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Category category)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (_context.Categories.Any(p => p.Name.Contains(category.Name)))
			{
				ModelState.AddModelError("Name", "Product Name Already Exists.");
				return View();
			}

			var newCategory = new Category
			{
				Name = category.Name
			};

			_context.Categories.Add(newCategory);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		//Delete
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}

			_context.Categories.Remove(categoryInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		//Edit
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}

			return View(categoryInDb);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Edit(Product category)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var categoryInDb = _context.Categories.SingleOrDefault(p => p.Id == category.Id);

			if (categoryInDb == null)
			{
				return HttpNotFound();
			}

			categoryInDb.Name = category.Name;
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}