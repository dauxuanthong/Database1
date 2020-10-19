using Database_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_1.ViewModels
{
	public class ProductCategoryViewModel
	{
		public Product Product { get; set; }
		//Create list of Category
		public IEnumerable<Category> Categories { get; set;}
	}
} 