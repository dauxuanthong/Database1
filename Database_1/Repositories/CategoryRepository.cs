using Database_1.Models;
using Database_1.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_1.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _context;
		public CategoryRepository()
		{
			_context = new ApplicationDbContext();
		}
		public IEnumerable<Category> GetCategories()
		{
			return _context.Categories.ToList();
		}
	}
}