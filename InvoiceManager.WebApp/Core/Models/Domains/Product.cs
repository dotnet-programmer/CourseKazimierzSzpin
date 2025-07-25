﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.WebApp.Core.Models.Domains;

public class Product
{
	public int Id { get; set; }

	[Required]
	public string Name { get; set; } = default!;

	public decimal Value { get; set; }

	public ICollection<InvoicePosition> InvoicePositions { get; set; } = new Collection<InvoicePosition>();
}