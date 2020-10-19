using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Database_1.Models
{
	public class Product
	{
		public int Id { get; set; }

		[Required]
		[DisplayName("Product Price")]
		public string Name { get; set; }

		[Required]
		[DisplayName("Product Price")]
		public double Price { get; set; }
		[Required]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}