using Database_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Database_1.ViewModels
{
	public class CartViewModel
	{
		public Cart Cart { get; set; }
		public Product Product { get; set; }
		public IEnumerable<Product> Products { get; set; }
	}
}