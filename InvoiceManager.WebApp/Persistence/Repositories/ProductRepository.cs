using InvoiceManager.WebApp.Core;
using InvoiceManager.WebApp.Core.Models.Domains;

namespace InvoiceManager.WebApp.Persistence.Repositories;

public class ProductRepository(IApplicationDbContext context)
{
	internal Product GetProduct(int productId)
		=> context.Products.Single(x => x.Id == productId);

	internal List<Product> GetProducts()
		=> context.Products.ToList();
}