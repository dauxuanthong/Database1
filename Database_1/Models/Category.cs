using System.ComponentModel.DataAnnotations;

namespace Database_1.Models
{
	public class Category
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}