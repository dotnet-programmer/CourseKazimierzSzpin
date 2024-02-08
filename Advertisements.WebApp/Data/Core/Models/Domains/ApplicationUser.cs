using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Advertisements.WebApp.Data.Core.Models.Domains;

public class ApplicationUser : IdentityUser
{
	//public int? AddressId { get; set; }
	//public Address? Address { get; set; }

	public ICollection<Advertisement> Advertisements { get; set; } = new Collection<Advertisement>();
}