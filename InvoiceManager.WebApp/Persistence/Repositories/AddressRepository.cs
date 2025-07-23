using InvoiceManager.WebApp.Core;
using InvoiceManager.WebApp.Core.Models.Domains;

namespace InvoiceManager.WebApp.Persistence.Repositories;

public class AddressRepository(IApplicationDbContext context)
{
	internal int AddAddress(Address address)
	{
		Address? newAddress = context.Addresses.FirstOrDefault(x => 
			x.PostalCode == address.PostalCode && 
			x.City == address.City && 
			x.Street == address.Street && 
			x.Number == address.Number
		);

		if (newAddress is null)
		{
			context.Addresses.Add(address);
			context.SaveChanges();
			return address.Id;
		}

		return newAddress.Id;
	}
}