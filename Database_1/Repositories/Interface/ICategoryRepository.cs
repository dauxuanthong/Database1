using Database_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_1.Repositories.Interface
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> GetCategories();
	}
}
