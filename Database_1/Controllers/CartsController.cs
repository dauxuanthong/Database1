using Database_1.Models;
using Database_1.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Database_1.Controllers
{
	public class CartsController : Controller
	{
		private readonly ApplicationDbContext _context;
		public CartsController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: Carts
		public ActionResult Index()
		{
			var carts = _context.Carts
				.Include(s => s.Customer)
				.Include(s => s.Product)
				.ToList();
			return View(carts);
		}

		[Authorize(Roles = "guest")]
		[HttpGet]
		public ActionResult Create()
		{
			var viewModel = new CartViewModel
			{
				Products = _context.Products.ToList()
			};


			return View(viewModel);
		}

		[Authorize(Roles = "guest")]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Create(Product product)
		{
			Cart cart = new Cart
			{
				CustomerId = User.Identity.GetUserId(),
				ProductId = product.Id
			};

			_context.Carts.Add(cart);
			_context.SaveChanges();
			return RedirectToAction("Mine");
		}

		[HttpGet]
		[Authorize(Roles = "guest")]
		public ActionResult Mine()
		{
			var userId = User.Identity.GetUserId();

			var carts = _context.Carts
				.Where(c => c.CustomerId == userId)
				.Include(c => c.Product)
				.ToList();

			return View(carts);
		}

		//DELETE for admin and customer
		[HttpGet]
		public ActionResult Delete(int id)
		{
			var cartInDb = _context.Carts.SingleOrDefault(p => p.Id == id);

			if (cartInDb == null)
			{
				return HttpNotFound();
			}

			_context.Carts.Remove(cartInDb);
			_context.SaveChanges();

			return RedirectToAction("Mine");
		}
		/*
		//Edit cart for user
		[HttpGet]
		[Authorize(Roles = "guest")]
		public  ActionResult Edit(int id)
		{
			var cartInDb = _context.Carts.SingleOrDefault(p => p.Id == id);
			if (cartInDb == null)
			{
				return HttpNotFound();
			}
			var viewmodel = new CartViewModel
			{
				Cart = cartInDb,
				Products = _context.Products.ToList()
			};
			return View(viewmodel);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		[Authorize(Roles = "guest")]

		public ActionResult Edit (Cart cart)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var cartInDb = _context.Carts.SingleOrDefault(p => p.Id == cart.Id);

			if (cartInDb == null)
			{
				return HttpNotFound();
			}
			
			cartInDb.Product.Name = cart.Product.Name;
			_context.SaveChanges();

			return RedirectToAction("Mine");
		}
		*/
	}
}