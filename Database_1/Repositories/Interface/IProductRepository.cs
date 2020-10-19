using Database_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_1.Repositories.Interface
{
	// 1st: create interface of ProductRepository 
	public interface IProductRepository
	{
		IEnumerable<Product> GetAllProducts();//Get a list of Product 
		IEnumerable<Product> GetAllProductsWithSearchString(string searchString);//Get a list of product with search 
		void CreateProduct(Product product);//Boolean logic of Create product
		bool EditProduct(Product product);//Boolean logic of Edit product 
		bool CheckExistProductName(string name);
		bool DeleteProductById(int id);//Boolean logic of Delete product 
		Product GetProductById(int id);
	}
}
