﻿using System;
using System.Collections.Generic;

namespace MyFinances.WebApi.Models.Domains;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
