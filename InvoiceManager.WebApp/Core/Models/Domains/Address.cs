﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.WebApp.Core.Models.Domains;

public class Address
{
	public int Id { get; set; }

	[Required]
	[Display(Name = "Ulica")]
	public string Street { get; set; } = default!;

	[Required]
	[Display(Name = "Numer")]
	public string Number { get; set; } = default!;

	[Required]
	[Display(Name = "Miejscowość")]
	public string City { get; set; } = default!;

	[Required]
	[Display(Name = "Kod pocztowy")]
	public string PostalCode { get; set; } = default!;

	public ICollection<Client> Clients { get; set; } = new Collection<Client>();
	public ICollection<ApplicationUser> Users { get; set; } = new Collection<ApplicationUser>();
}